export default class AuthStore {
    static getToken() {
        return window.localStorage.getItem(AuthStore.storageKey);
    }
    static setToken(token) {
        window.localStorage.setItem(AuthStore.storageKey, token);
    }
    static removeToken() {
        window.localStorage.removeItem(AuthStore.storageKey);
    }
    static isSignedIn() {
        return !!AuthStore.getToken();
    }
}
AuthStore.storageKey = "token";
//# sourceMappingURL=auth-store.js.map