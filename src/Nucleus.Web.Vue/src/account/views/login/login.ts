import { Component } from 'vue-property-decorator';
import AccountAppService from '@/services/account/account-app-service';
import AppComponentBase from '@/models/shared/app-component-base';

@Component
export default class LoginComponent extends AppComponentBase {

    public usernameoremail = '';
    public password = '';
    public errors: IErrorResponse[] = [];

    public onSubmit() {
        const accountAppService = new AccountAppService();
        const loginViewModel: ILoginInput = { userNameOrEmail: this.usernameoremail, password: this.password };

        accountAppService.login(loginViewModel).then((response) => {
            if (!response.isError) {
                this.$router.push({ path: '/admin/home' });
            } else {
                this.errors = response.errors;
            }
        });
    }
}
