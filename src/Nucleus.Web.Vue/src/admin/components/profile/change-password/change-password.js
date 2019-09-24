import { __decorate } from "tslib";
import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Prop } from 'vue-property-decorator';
let ChangePasswordComponent = class ChangePasswordComponent extends NucleusComponentBase {
    constructor() {
        super(...arguments);
        this.refs = this.$refs;
        this.errors = [];
        this.changePasswordInput = {};
        this.dialog = false;
    }
    mounted() {
        this.$root.$on('changePasswordDialogChanged', (dialog) => {
            this.dialog = dialog;
        });
    }
    save() {
        if (this.refs.form.validate()) {
            this.nucleusService.post('/api/changePassword', this.changePasswordInput)
                .then((response) => {
                if (!response.isError) {
                    this.dialog = false;
                    this.swalToast(2000, 'success', this.$t('Successful').toString());
                    this.logOut();
                }
                else {
                    this.errors = response.errors;
                }
            });
        }
    }
};
__decorate([
    Prop()
], ChangePasswordComponent.prototype, "changePasswordDialog", void 0);
__decorate([
    Prop()
], ChangePasswordComponent.prototype, "logOut", void 0);
ChangePasswordComponent = __decorate([
    Component
], ChangePasswordComponent);
export default ChangePasswordComponent;
//# sourceMappingURL=change-password.js.map