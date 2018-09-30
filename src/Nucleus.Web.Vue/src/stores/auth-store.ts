export default class AuthStore {
    static storageKey: string = "token";

    static getToken() {
        return window.localStorage.getItem(AuthStore.storageKey);
    }

    static setToken(token: string) {
        window.localStorage.setItem(AuthStore.storageKey, token);
    }

    static removeToken(): void {
        window.localStorage.removeItem(AuthStore.storageKey);
    }

    static isSignedIn(): boolean {
        return !!AuthStore.getToken();
    }
}