import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';

@Component
export default class RegisterComponent extends NucleusComponentBase {
    refs = this.$refs as any;
    registerInput = {} as IRegisterInput;
    errors: INameValueDto[] = [];
    resultMessage: string | undefined;
    registerComplete = false;

    onSubmit() {
        if (this.refs.form.validate()) {
            this.nucleusService.post<IRegisterOutput>('/api/register', this.registerInput)
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