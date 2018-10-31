import AuthStore from '@/stores/auth-store';
import AppConsts from '@/infrastructure/core/app-consts';

export default class AppService {
    private static request<T>(method: string, url: string, data: object | string = ''): Promise<IRestResponseDto<T>> {
        let isBadRequest = false;
        let body = data === '' ? null : data;
        const headers: { [key: string]: string } = {
            Authorization: `Bearer ${AuthStore.getToken()}`
        };

        if (data) {
            headers['Content-Type'] = 'application/json';
            body = JSON.stringify(data);
        }

        return fetch(AppConsts.baseApiUrl + url,
                ({
                    method,
                    headers,
                    body
                }) as any)
            .then((response: any) => {
                isBadRequest = !response.ok;
                if (response.status === 401) {
                    AuthStore.removeToken();
                    return { errorMessage: 'Unauthorized request' };
                }

                return response.text();
            })
            .then((responseContent: any) => {
                let content: any;

                try {
                    content = JSON.parse(responseContent);
                } catch (err) {
                    content = responseContent;
                }

                const response = {
                    isError: isBadRequest,
                    errors: isBadRequest ? content : null,
                    content: isBadRequest ? null : content
                } as IRestResponseDto<T>;

                return response;
            });
    }

    public get<T>(url: string): Promise<IRestResponseDto<T>> {
        return AppService.request<T>('GET', url);
    }

    public delete(url: string): Promise<IRestResponseDto<void>> {
        return AppService.request<void>('DELETE', url);
    }

    public put<T>(url: string, data: object | string): Promise<IRestResponseDto<T>> {
        return AppService.request<T>('PUT', url, data);
    }

    public post<T>(url: string, data: object | string): Promise<IRestResponseDto<T>> {
        return AppService.request<T>('POST', url, data);
    }
}