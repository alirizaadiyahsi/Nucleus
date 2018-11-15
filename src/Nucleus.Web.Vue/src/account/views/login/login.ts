import { Component } from 'vue-property-decorator';
import AppComponentBase from '@/shared/application/app-component-base';

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
                        this.authStore.setToken(response.content.token);
                        this.$router.push({ path: '/admin/home' });
                    } else {
                        this.errors = response.errors;
                    }
                });
        }
    }
}