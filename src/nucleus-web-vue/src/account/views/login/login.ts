import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';

@Component
export default class LoginComponent extends NucleusComponentBase {
    public refs = this.$refs as any;
    public loginInput = {} as ILoginInput;
    public errors: INameValueDto[] = [];

    public onSubmit() {
        if (this.refs.form.validate()) {
            this.nucleusService.post<ILoginOutput>('/api/login', this.loginInput)
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