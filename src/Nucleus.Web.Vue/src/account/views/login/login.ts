import { Component } from 'vue-property-decorator';
import AppComponentBase from '@/infrastructure/core/app-component-base';
import AuthStore from '@/stores/auth-store';

@Component
export default class LoginComponent extends AppComponentBase {

    public refs = this.$refs as any;
    public loginInput = {} as ILoginInput;
    public errors: INameValueDto[] = [];

    public onSubmit() {
        if (this.refs.form.validate()) {
            this.appService.post<ILoginOutput>('/api/account/login', this.loginInput)
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