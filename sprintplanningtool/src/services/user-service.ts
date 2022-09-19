import router from '@/router';
import { authHeader } from '../helpers/auth-header';
import { User } from 'interfaces/User';

export const userService = {
    login,
    logout,
    register,
    getAll,
    getById,
    update,
    delete: _delete,
    navigateToAdminPage,
};

async function login(username: string, password: string) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password }),
    };

    const response = await fetch(`/api/Users/authenticate`, requestOptions);

    const user = await handleResponse(response);
    // login successful if there's a jwt token in the response
    if (user.token) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(user));
    }

    return user;
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

async function register(user: any) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user),
    };

    const response = await fetch(`/api/Users/register`, requestOptions);
    return handleResponse(response);
}

async function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader() as HeadersInit,
    };

    const response = await fetch(`/api/Users`, requestOptions);
    return handleResponse(response);
}

async function getById(id: number) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader() as HeadersInit,
    };

    const response = await fetch(`/api/Users/${id}`, requestOptions);
    return handleResponse(response);
}

async function update(user: any) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            ...(authHeader() as HeadersInit),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user),
    };

    const response = await fetch(`/api/Users/${user.id}`, requestOptions);
    return handleResponse(response);
}

// prefixed function name with underscore because delete is a reserved word in javascript
async function _delete(id: number) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader() as HeadersInit,
    };

    const response = await fetch(`/api/Users/${id}`, requestOptions);
    return handleResponse(response);
}

async function navigateToAdminPage(id: string) {
    const currentUser: User = JSON.parse(localStorage.getItem('user') as any);
    const error = alert('You do not have permissions to access this page.');

    currentUser.isAdmin ? router.push('/admin') : error;
}

function handleResponse(response: any) {
    return response.text().then((text: string) => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api
                logout();
                location.reload();
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}
