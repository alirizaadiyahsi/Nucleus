import { Component } from 'vue-property-decorator';
import AppComponentBase from '@/infrastructure/core/app-component-base';

@Component
export default class RegisterComponent extends AppComponentBase {
    public refs = this.$refs as any;
    public registerInput = {} as IRegisterInput;
    public errors: INameValueDto[] = [];
    public resultMessage: string | undefined;
    public registerComplete = false;

    public onSubmit() {
        if (this.refs.form.validate()) {
            this.appService.post<IRegisterOutput>('/api/account/register', this.registerInput)
                .then((response) => {
                    if (!response.isError) {
                        this.resultMessage = this.$t('AccountCreationSuccessful').toString();
                        this.registerComplete = true;
                    } else {
                        this.errors = response.errors;
                    }
                });
        }
    }
}