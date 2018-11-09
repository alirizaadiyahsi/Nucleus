import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component, Prop } from 'vue-property-decorator';

@Component
export default class ChangePasswordComponent extends AppComponentBase {
    @Prop() public changePasswordDialog!: boolean;
    @Prop() public logOut: any;
    public refs = this.$refs as any;
    public errors: INameValueDto[] = [];
    public changePasswordInput = {} as IChangePasswordInput;
    public dialog = false;

    public mounted() {
        this.$root.$on('changePasswordDialogChanged',
            (dialog: boolean) => {
                this.dialog = dialog;
            });
    }

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
}