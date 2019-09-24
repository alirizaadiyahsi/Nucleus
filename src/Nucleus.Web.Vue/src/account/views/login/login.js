import { __decorate } from "tslib";
import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';
let LoginComponent = class LoginComponent extends NucleusComponentBase {
    constructor() {
        super(...arguments);
        this.refs = this.$refs;
        this.loginInput = {};
        this.errors = [];
    }
    onSubmit() {
        if (this.refs.form.validate()) {
            this.nucleusService.post('/api/login', this.loginInput)
                .then((response) => {
                if (!response.isError) {
                    this.authStore.setToken(response.content.token);
                    this.$router.push({ path: '/admin/home' });
                }
                else {
                    this.errors = response.errors;
                }
            });
        }
    }
};
LoginComponent = __decorate([
    Component
], LoginComponent);
export default LoginComponent;
//# sourceMappingURL=login.js.map