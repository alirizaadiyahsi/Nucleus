import ServiceBase from "@/core/services/service-base";
import {ILoginOutput} from "@/core/services/identity/models/login-output";
import {IRegisterInput} from "@/core/services/identity/models/register-input";
import {IRegisterOutput} from "@/core/services/identity/models/register-output";
import AuthStore from "@/core/stores/auth-store";

class IdentityService extends ServiceBase {
    login(loginModel: ILoginInput): Promise<ILoginOutput> {
        return this._nucleusAxios.post('identity/login', loginModel).then(response => {
            if (response.data.token) {
                AuthStore.setToken(response.data.token);
            }
            return response.data;
        });
    }

    logout() {
        AuthStore.removeToken();
    }

    register(registerModel: IRegisterInput): Promise<IRegisterOutput> {
        return this._nucleusAxios.post('identity/register', registerModel).then(response => {
            return response.data;
        });
    }
}

export default new IdentityService();