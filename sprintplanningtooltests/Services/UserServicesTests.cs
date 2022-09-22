using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using sprintplanningtoolbackend.Authorisation;
using sprintplanningtoolbackend.Helpers;
using sprintplanningtoolbackend.Models;
using sprintplanningtoolbackend.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace sprintplanningtooltests
{
    public class UserServicesTests
    {

        private UserService _subject;
        private readonly Mock<IJwtUtils> _jwtUtils;
        private readonly IMapper _mapper;
        private readonly Mock<DbSet<User>> _mockUsers;
        private readonly Mock<DataContext> _context;
        private int callCount = 0;
        private int save = 0;

        public UserServicesTests()
        {
            _jwtUtils = new Mock<IJwtUtils>();
            _mockUsers = new Mock<DbSet<User>>();
            _context = new Mock<DataContext>(It.IsAny<ConfigurationProvider>());

            // Set up mapper
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
            _mapper = mockMapper.CreateMapper();

            // Create UserService instance
            _subject = new UserService(_context.Object, _jwtUtils.Object, _mapper);

        }

        // Helper method for setting up a mock DB set for Moq DB Context
        private Mock<DbSet<User>> SetUpDBUsers(IQueryable<User> users)
        {
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            
            return _mockUsers;
        }

        #region Authenticate

        [Fact]
        public void Authenticate_WhenTestUserExists_ReturnsTestUserInResponse()
        {
            // Arrange
            var user = new AuthenticateRequest()
            {
                Username = "test",
                Password = "test",
            };

            var hash = BCrypt.Net.BCrypt.HashPassword("test");
            var users = new List<User>
            {
                new User {
                    Username = "test",
                    Id = 1,
                    IsAdmin = true,
                    PasswordHash = hash,
                },
            }.AsQueryable();

            SetUpDBUsers(users);
            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            _jwtUtils.Setup(t => t.GenerateToken(users.First())).Returns("token");

            // Act
            var response = _subject.Authenticate(user);

            // Assert
            Assert.Equal(user.Username, response.Username);
            Assert.True(response.IsAdmin);
            Assert.Equal(1, response.Id);
            Assert.NotEmpty(response.Token);
        }

        [Fact]
        public void Authenticate_WhenUserEntersAnIncorrectPassword_ThrowsEx()
        {
            // Arrange
            var user = new AuthenticateRequest()
            {
                Username = "test",
                Password = "incorrect",
            };

            var hash = BCrypt.Net.BCrypt.HashPassword("test");
            var users = new List<User>
            {
                new User {
                    Username = "test",
                    PasswordHash = hash
                },
            }.AsQueryable();

            SetUpDBUsers(users);
            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            _jwtUtils.Setup(t => t.GenerateToken(users.First())).Returns("token");

            var expected = "Username or password is incorrect";

            // Act + Assert
            var exception = Assert.Throws<AppException>(() => _subject.Authenticate(user));
            Assert.Equal(expected, exception.Message);
        }

        #endregion        

        #region GetAll

        [Fact]
        public void GetAll_WhenNoUserExists_ReturnsNothing()
        {
            // Arrange
            var users = new List<User>().AsQueryable();
            SetUpDBUsers(users);
            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            // Act
            var result = _subject.GetAll().ToList();

            // Assert
            Assert.Empty(result);
        }           
        
        [Fact]
        public void GetAll_WhenOneUserExists_ReturnsOneUser()
        {
            // Arrange
            var users = new List<User>
            {
                new User {
                    Username = "test",
                    Id = 1,
                    IsAdmin = true,
                },
            }.AsQueryable();

            SetUpDBUsers(users);

            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            // Act
            var result = _subject.GetAll().ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Count == 1);
            Assert.Equal(users, result);
        }        
        
        [Fact]
        public void GetAll_WhenThreeUsersExists_ReturnsThreeUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User {
                    Username = "test",
                    Id = 1,
                    IsAdmin = true,
                },                
                new User {
                    Username = "test2",
                    Id = 2,
                    IsAdmin = false,
                },
                new User {
                    Username = "test3",
                    Id = 3,
                    IsAdmin = true,
                },
            }.AsQueryable();

            SetUpDBUsers(users);
            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            // Act
            var result = _subject.GetAll().ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Count == 3);
            Assert.Equal(users, result);
        }


        #endregion

        #region GetById

        [Fact]
        public void GetById_WhenIdIsFive_ThrowsKeyNotFoundEx()
        {
            // Arrange
            var users = new List<User>
            {
                new User {
                    Username = "test",
                    Id = 1,
                    IsAdmin = true,
                },                
                new User {
                    Username = "test2",
                    Id = 2,
                    IsAdmin = true,
                },
            }.AsQueryable();

            SetUpDBUsers(users);
            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            var expected = "User not found";

            // Act + Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => _subject.GetById(1));
            Assert.Equal(expected, exception.Message);      
        }        
        
        [Fact]
        public void GetById_WhenIdIsOne_ReturnsTestUser()
        {
            // Arrange
            var user = new User
            {
                Username = "test",
                Id = 1,
                IsAdmin = true,
            };

            var users = new List<User> { user }.AsQueryable();

            SetUpDBUsers(users);
            _mockUsers.Setup(m => m.Find(user.Id)).Returns(user);
            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            var expected = users.Where(u => u.Id == 1).Single();

            // Act
            var result = _subject.GetById(1);

            // Assert
            Assert.True(expected == result);
        }


        #endregion

        #region Register

        [Fact]
        public void Register_WhenValidNewUser_AddAndSaveChangesAreInvoked()
        {
            // Arrange
            var user = new RegisterRequest()
            {
                Username = "new",
                Password = "test",
                Email = "test@testing.com",
            };

            var users = new List<User> { new User { Id = 1 } }.AsQueryable();

            // Counter to see if context.add method is called first
            int addUser = 0;

            SetUpDBUsers(users);
            _context.Setup(c => c.Users).Returns(_mockUsers.Object);
            _context.Setup(c => c.Users.Add(It.IsAny<User>())).Callback(() => addUser = callCount++);
            _context.Setup(c => c.SaveChanges()).Callback(() => save = callCount++);

            // Act
            _subject.Register(user);

            // Assert
            _context.Verify(c => c.Users.Add(It.IsAny<User>()), Times.Once());
            _context.Verify(c => c.SaveChanges(), Times.Once());

            // Check the counters to confirm the call order.
            Assert.Equal(0, addUser);
            Assert.Equal(1, save);
        }        
        
        [Fact]
        public void Register_WhenUserRegistersATakenName_ThrowsAppEx()
        {
            // Arrange
            var user = new RegisterRequest()
            {
                Username = "test",
                Password = "test",
                Email = "test@testing.com",
            };

            var mappedUser = _mapper.Map<User>(user);

            var users = new List<User> { new User { Username = "test" } }.AsQueryable();

            SetUpDBUsers(users);
            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            var expected = "Username 'test' is already taken";

            // Act + Assert
            var exception = Assert.Throws<AppException>(() => _subject.Register(user));
            Assert.Equal(expected, exception.Message);
        }

        #endregion

        #region Update
        [Fact]
        public void Update_WhenValidNameChange_UpdateAndSaveChangesInvoked()
        {
            // Arrange
            var updateUserReq = new UpdateRequest 
            { 
                Email = "cloud@test.com", 
                IsAdmin = true, 
                Password = "testing",
                Username = "cloud",
            };

            var testUser = new User
            {
                Username = "test",
                Id = 1
            };

            var users = new List<User> { testUser }.AsQueryable();

            int updateUser = 0;

            SetUpDBUsers(users);
            _mockUsers.Setup(m => m.Find(testUser.Id)).Returns(testUser);

            _context.Setup(c => c.Users).Returns(_mockUsers.Object);
            _context.Setup(c => c.Users.Update(It.IsAny<User>())).Callback(() => updateUser = callCount++);
            _context.Setup(c => c.SaveChanges()).Callback(() => save = callCount++);

            // Act
            _subject.Update(1, updateUserReq);

            // Assert
            _context.Verify(c => c.Users.Update(It.IsAny<User>()), Times.Once());
            _context.Verify(c => c.SaveChanges(), Times.Once());

            // Check the counters to confirm the call order.
            Assert.Equal(0, updateUser);
            Assert.Equal(1, save);
        }      
        
        [Fact]
        public void Update_WhenChangingUsernameToExistingName_ThrowsAppEx()
        {
            // Arrange
            var updateUserReq = new UpdateRequest 
            { 
                Email = "cloud@test.com", 
                IsAdmin = true, 
                Password = "testing",
                Username = "test2",
            };

            var user = new User
            {
                Username = "test",
                Id = 1,
            };            
            
            var user2 = new User
            {
                Username = "test2",
                Id = 2,
            };

            var users = new List<User> { user, user2 }.AsQueryable();

            SetUpDBUsers(users);
            _mockUsers.Setup(m => m.Find(user.Id)).Returns(user);

            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            var expected = "Username 'test2' is already taken";

            // Act + Assert
            var exception = Assert.Throws<AppException>(() => _subject.Update(1, updateUserReq));
            Assert.Equal(expected, exception.Message);
        }

        #endregion

        #region Delete
        [Fact]
        public void Delete_WhenUserExists_DeleteAndSaveChangesInvoked()
        {
            // Arrange
            var user = new User
            {
                Username = "test",
                Id = 1,
                IsAdmin = true,
            };

            var users = new List<User> { user }.AsQueryable();

            int deleteUser = 0;

            SetUpDBUsers(users);
            _mockUsers.Setup(m => m.Find(user.Id)).Returns(user);

            _context.Setup(c => c.Users).Returns(_mockUsers.Object);
            _context.Setup(c => c.Users.Remove(It.IsAny<User>())).Callback(() => deleteUser = callCount++);
            _context.Setup(c => c.SaveChanges()).Callback(() => save = callCount++);

            // Act
            _subject.Delete(1);

            // Assert
            _context.Verify(c => c.Users.Remove(It.IsAny<User>()), Times.Once());
            _context.Verify(c => c.SaveChanges(), Times.Once());

            // Check the counters to confirm the call order.
            Assert.Equal(0, deleteUser);
            Assert.Equal(1, save);
        }

        [Fact]
        public void Delete_WhenUserDoesNotExist_ThrowEx()
        {
            // Arrange
            var user = new User { Id = 1 };
            var users = new List<User> { user }.AsQueryable();
           
            _mockUsers.Setup(m => m.Find(user.Id)).Returns(user);
            SetUpDBUsers(users);

            _context.Setup(c => c.Users).Returns(_mockUsers.Object);

            var expected = "User not found";

            // Act + Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => _subject.Delete(2));
            Assert.Equal(expected, exception.Message);
        }

        #endregion
    }
}