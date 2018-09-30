import BaseAppService from './base-app-service';
import AuthStore from "../stores/auth-store";
export default class AccountAppService extends BaseAppService {
    login(loginViewModel) {
        return this.post('/api/account/login', loginViewModel)
            .then((response) => {
            if (!response.isError) {
                AuthStore.setToken(response.content.token);
            }
            return response;
        });
    }
    logOut() {
        AuthStore.removeToken();
    }
    register(registerViewModel) {
        return this.post('/api/account/register', registerViewModel)
            .then((response) => {
            return response;
        });
    }
}
//# sourceMappingURL=account-app-service.js.map