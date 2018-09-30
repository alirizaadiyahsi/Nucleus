import * as tslib_1 from "tslib";
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AccountAppService from '../../../services/account-app-service';
let LoginComponent = class LoginComponent extends Vue {
    constructor() {
        super(...arguments);
        this.username = "";
        this.password = "";
        this.errors = [];
    }
    onSubmit() {
        const accountAppService = new AccountAppService();
        const loginViewModel = { userName: this.username, password: this.password };
        accountAppService.login(loginViewModel).then(response => {
            if (!response.isError) {
                this.$router.push({ path: '/admin/home' });
            }
            else {
                this.errors = response.errors;
            }
        });
    }
};
LoginComponent = tslib_1.__decorate([
    Component
], LoginComponent);
export default LoginComponent;
//# sourceMappingURL=login.js.map