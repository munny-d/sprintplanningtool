import { authHeader } from '@/helpers/auth-header';

export const reportService = {
    create,
    getAll,
    getById,
    getLatest,
    getTeamMembers,
    delete: _delete,
};

async function create(report: any) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(report),
    };

    const response = await fetch(`api/Reports/create`, requestOptions);
    return handleResponse(response);
}

async function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader() as HeadersInit,
    };

    const response = await fetch(`api/Reports`, requestOptions);
    return handleResponse(response);
}

async function getById(id: string) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader() as HeadersInit,
    };

    const response = await fetch(`api/Reports/${id}`, requestOptions);
    return handleResponse(response);
}

async function getTeamMembers(id: string) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader() as HeadersInit,
    };

    const response = await fetch(`api/Reports/team/${id}`, requestOptions);
    return handleResponse(response);
}

async function getLatest() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader() as HeadersInit,
    };

    const response = await fetch(`api/Reports/latest`, requestOptions);
    return handleResponse(response);
}

// prefixed function name with underscore because delete is a reserved word in javascript
async function _delete(id: number) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader() as HeadersInit,
    };

    const response = await fetch(`api/Reports/${id}`, requestOptions);
    return handleResponse(response);
}

function handleResponse(response: any) {
    return response.text().then((text: string) => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            const error = (data && data.message) || response.statusText;
            console.log('ERROR: ', error);

            return Promise.reject(error);
        }
        return data;
    });
}
