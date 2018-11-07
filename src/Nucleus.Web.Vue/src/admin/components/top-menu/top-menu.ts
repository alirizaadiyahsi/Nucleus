import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';
import AuthStore from '@/stores/auth-store';

@Component
export default class TopMenuComponent extends AppComponentBase {
    // todo: add profile dropdown menu to toolbar and show user name as menu name
    // todo: add components for each profile menu item
    public refs = this.$refs as any;
    public drawer = true;
    public dialog = false;
    public errors: INameValueDto[] = [];
    public changePasswordInput = {} as IChangePasswordInput;

    public passwordMatchError() {
        return (this.changePasswordInput.newPassword === this.changePasswordInput.passwordRepeat)
            ? ''
            : 'Passwords must match';
    }

    public save() {
        if (this.refs.form.validate()) {
            this.appService.post<ILoginOutput>('/api/account/changePassword', this.changePasswordInput)
                .then((response) => {
                    if (!response.isError) {
                        this.dialog = false;
                        this.swalToast(2000, 'success', 'Successful!');
                        this.logOut();
                    } else {
                        this.errors = response.errors;
                    }
                });
        }
    }

    public drawerChanged() {
        this.$root.$emit('drawerChanged');
    }

    public logOut() {
        AuthStore.removeToken();
        this.$router.push({ path: '/account/login' });
    }
}