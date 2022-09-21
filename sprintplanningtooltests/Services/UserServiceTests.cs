//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using sprintplanningtoolbackend.Authorisation;
//using sprintplanningtoolbackend.Helpers;
//using sprintplanningtoolbackend.Models;
//using sprintplanningtoolbackend.Services;
//using System.Collections.Generic;
//using Xunit;

//namespace sprintplanningtooltests
//{
//    public class UserServiceTests
//    {
//        UserService _subject;
//        readonly Mock<IJwtUtils> _jwtUtls;
//        readonly Mock<IMapper> _autoMapper;
//        readonly Mock<DbSet<User>> _data;
//        readonly TestContext _context;

//        public UserServiceTests(IDataContext context)
//        {
//            _jwtUtls = new Mock<IJwtUtils>();
//            _autoMapper = new Mock<IMapper>();
//            _data = new Mock<DbSet<User>>();
//            _context = context;

//            var user = new User { Id = 1, Username = "test" };
         
//            _context.Users.Add(user);
            
//            _subject = new UserService(_context, _jwtUtls.Object, _autoMapper.Object);
//        }
//        [Fact]
//        public void Authenticate_WhenUserExists_ReturnsResponse()
//        {
//            // Arrange
//            var userReq = new AuthenticateRequest()
//            {
//                Username = "test",
//                Password = It.IsAny<string>()
//            };

            

//            var response = _subject.Authenticate(userReq);
//            Assert.NotNull(response);
//        }
//    }
//}