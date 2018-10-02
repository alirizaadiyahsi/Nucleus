import AuthStore from '../stores/auth-store';
export default class BaseAppService {
    static request(method, url, data = '') {
        let isBadRequest = false;
        let body = data === '' ? null : data;
        const headers = {
            Authorization: `Bearer ${AuthStore.getToken()}`,
        };
        if (data) {
            headers['Content-Type'] = 'application/json';
            body = JSON.stringify(data);
        }
        return fetch(this.baseApiUrl + url, ({
            method,
            headers,
            body,
        }))
            .then((response) => {
            isBadRequest = !response.ok;
            if (response.status === 401) {
                AuthStore.removeToken();
                return { errorMessage: 'Unauthorized request' };
            }
            return response.json();
        })
            .then((responseContent) => {
            const response = {
                isError: isBadRequest,
                errors: isBadRequest ? responseContent.errors : null,
                content: isBadRequest ? null : responseContent,
            };
            return response;
        });
    }
    get(url) {
        return BaseAppService.request('GET', url);
    }
    delete(url) {
        return BaseAppService.request('DELETE', url);
    }
    put(url, data) {
        return BaseAppService.request('PUT', url, data);
    }
    post(url, data) {
        return BaseAppService.request('POST', url, data);
    }
}
// todo: get this from a config file
BaseAppService.baseApiUrl = 'https://localhost:44339';
//# sourceMappingURL=base-app-service.js.map