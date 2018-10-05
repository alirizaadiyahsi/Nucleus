import AuthStore from '../stores/auth-store';

export default class BaseAppService {
    // todo: get this from a config file
    public static baseApiUrl = 'https://localhost:44339';

    private static request<T>(method: string, url: string, data: object | string = ''): Promise<IRestResponse<T>> {
        let isBadRequest = false;
        let body = data === '' ? null : data;
        const headers: { [key: string]: string } = {
            Authorization: `Bearer ${AuthStore.getToken()}`,
        };

        if (data) {
            headers['Content-Type'] = 'application/json';
            body = JSON.stringify(data);
        }

        return fetch(this.baseApiUrl + url,
            ({
                method,
                headers,
                body,
            }) as any)
            .then((response) => {
                isBadRequest = !response.ok;
                if (response.status === 401) {
                    AuthStore.removeToken();
                    return { errorMessage: 'Unauthorized request' };
                }

                return response.json();
            })
            .then((responseContent: any) => {

                const response: IRestResponse<T> = {
                    isError: isBadRequest,
                    errors: isBadRequest ? responseContent.errors : null,
                    content: isBadRequest ? null : responseContent,
                };

                return response;
            });
    }

    public get<T>(url: string): Promise<IRestResponse<T>> {
        return BaseAppService.request<T>('GET', url);
    }

    public delete(url: string): Promise<IRestResponse<void>> {
        return BaseAppService.request<void>('DELETE', url);
    }

    public put<T>(url: string, data: object | string): Promise<IRestResponse<T>> {
        return BaseAppService.request<T>('PUT', url, data);
    }

    public post<T>(url: string, data: object | string): Promise<IRestResponse<T>> {
        return BaseAppService.request<T>('POST', url, data);
    }
}
