import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AccountAppService from '../../../services/account/account-app-service';

@Component
export default class LoginComponent extends Vue {

    public usernameoremail = '';
    public password = '';
    public errors: IErrorResponse[] = [];

    public onSubmit() {
        const accountAppService = new AccountAppService();
        const loginViewModel: ILoginViewModel = { userNameOrEmail: this.usernameoremail, password: this.password };

        accountAppService.login(loginViewModel).then((response) => {
            if (!response.isError) {
                this.$router.push({ path: '/admin/home' });
            } else {
                this.errors = response.errors;
            }
        });
    }
}
