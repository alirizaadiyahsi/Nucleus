export default class AuthStore {
    public static storageKey: string = 'token';

    public static getToken() {
        return window.localStorage.getItem(AuthStore.storageKey);
    }

    public static setToken(token: string) {
        window.localStorage.setItem(AuthStore.storageKey, token);
    }

    public static removeToken(): void {
        window.localStorage.removeItem(AuthStore.storageKey);
    }

    public static isSignedIn(): boolean {
        return !!AuthStore.getToken();
    }
}
