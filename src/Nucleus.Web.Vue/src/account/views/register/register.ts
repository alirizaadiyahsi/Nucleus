import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AccountAppService from '../../../services/account/account-app-service';

@Component
export default class RegisterComponent extends Vue {

    public username = '';
    public email = '';
    public password = '';
    public errors: IErrorResponse[] = [];
    public resultMessage: string | undefined;
    public registerComplete = false;

    public onSubmit() {
        const accountAppService = new AccountAppService();
        const registerViewModel: IRegisterViewModel = {
            userName: this.username,
            email: this.email,
            password: this.password,
        };

        accountAppService.register(registerViewModel).then((response) => {
            if (!response.isError) {
                this.resultMessage = response.content.resultMessage;
                this.registerComplete = true;
            } else {
                this.errors = response.errors;
            }
        });
    }
}
