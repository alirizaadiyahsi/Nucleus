import { Component } from 'vue-property-decorator';
import AccountAppService from '@/services/account/account-app-service';
import AppComponentBase from '@/models/shared/app-component-base';

@Component
export default class RegisterComponent extends AppComponentBase {

    public username = '';
    public email = '';
    public password = '';
    public errors: IErrorResponse[] = [];
    public resultMessage: string | undefined;
    public registerComplete = false;

    public onSubmit() {
        const accountAppService = new AccountAppService();
        const registerViewModel: IRegisterInput = {
            userName: this.username,
            email: this.email,
            password: this.password,
        };

        accountAppService.register(registerViewModel).then((response) => {
            if (!response.isError) {
                this.resultMessage = 'Your account has been successfully created.';
                this.registerComplete = true;
            } else {
                this.errors = response.errors;
            }
        });
    }
}
