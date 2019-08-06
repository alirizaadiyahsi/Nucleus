import AuthStore from '@/stores/auth-store';
import AppConsts from '@/shared/application/nucleus';
import Nucleus from '@/shared/application/nucleus';

export default class NucleusService {
    private static request<T>(method: string, url: string, data: object | string = '', loadingEnabled = true):
        Promise<IRestResponseDto<T>> {
        let isBadRequest = false;
        let body = data === '' ? null : data;
        const headers: { [key: string]: string } = {
            Authorization: `Bearer ${AuthStore.getToken()}`
        };

        if (data) {
            headers['Content-Type'] = 'application/json';
            body = JSON.stringify(data);
        }

        if (loadingEnabled) {
            Nucleus.isLoading = true;
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
            }).finally(() => {
                Nucleus.isLoading = false;
            });
    }

    public get<T>(url: string, loadingEnabled = true): Promise<IRestResponseDto<T>> {
        return NucleusService.request<T>('GET', url, '', loadingEnabled);
    }

    public delete(url: string): Promise<IRestResponseDto<void>> {
        return NucleusService.request<void>('DELETE', url);
    }

    public put<T>(url: string, data: object | string): Promise<IRestResponseDto<T>> {
        return NucleusService.request<T>('PUT', url, data);
    }

    public post<T>(url: string, data: object | string): Promise<IRestResponseDto<T>> {
        return NucleusService.request<T>('POST', url, data);
    }
}