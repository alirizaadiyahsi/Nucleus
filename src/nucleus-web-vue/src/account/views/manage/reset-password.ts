import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';

@Component
export default class ResetPasswordComponent extends NucleusComponentBase {
    refs = this.$refs as any;
    resetPasswordInput = {} as IResetPasswordInput;
    errors: INameValueDto[] = [];
    isPasswordReset =false;
    resultMessage: string | undefined;

    onSubmit() {
        if (this.refs.form.validate()) {
            this.resetPasswordInput.token = this.$route.query.token.toString();
            this.nucleusService.post<IResetPasswordOutput>('/api/resetPassword', this.resetPasswordInput)
                .then((response) => {
                    if (!response.isError) {
                        this.resultMessage = this.$t('PasswordResetSuccessful').toString();
                        this.isPasswordReset = true;
                    } else {
                        this.errors = response.errors;
                    }
                });
        }
    }
}