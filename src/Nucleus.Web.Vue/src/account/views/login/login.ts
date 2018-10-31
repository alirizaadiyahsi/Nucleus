import { Component } from 'vue-property-decorator';
import AppComponentBase from '@/infrastructure/core/app-component-base';
import AuthStore from '@/stores/auth-store';

@Component
export default class LoginComponent extends AppComponentBase {

    public refs = this.$refs as any;
    public usernameoremail = '';
    public password = '';
    public errors: INameValueDto[] = [];

    // todo: handle when press 'enter'
    public onSubmit() {
        if (this.refs.form.validate()) {
            const loginInput: ILoginInput = { userNameOrEmail: this.usernameoremail, password: this.password };

            this.appService.post<ILoginOutput>('/api/account/login', loginInput)
                .then((response) => {
                    if (!response.isError) {
                        AuthStore.setToken(response.content.token);
                        this.$router.push({ path: '/admin/home' });
                    } else {
                        this.errors = response.errors;
                    }
                });
        }
    }
}