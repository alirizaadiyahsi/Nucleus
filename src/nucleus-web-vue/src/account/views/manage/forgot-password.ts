import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';

@Component
export default class ForgotPasswordComponent extends NucleusComponentBase {
    refs = this.$refs as any;
    forgotPasswordInput = {} as IForgotPasswordInput;
    errors: INameValueDto[] = [];
    isEmailSent = false;
    resultMessage: string | undefined;

    onSubmit() {
        if (this.refs.form.validate()) {
            this.nucleusService.post<IForgotPasswordOutput>('/api/forgotPassword', this.forgotPasswordInput)
                .then((response) => {
                    if (!response.isError) {
                        this.resultMessage = this.$t('EMailSentSuccessfully').toString();
                        this.isEmailSent = true;
                    } else {
                        this.errors = response.errors;
                    }
                });
        }
    }
}