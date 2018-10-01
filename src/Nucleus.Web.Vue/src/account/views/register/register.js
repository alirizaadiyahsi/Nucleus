import * as tslib_1 from "tslib";
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AccountAppService from '../../../services/account-app-service';
let RegisterComponent = class RegisterComponent extends Vue {
    constructor() {
        super(...arguments);
        this.username = '';
        this.email = '';
        this.password = '';
        this.errors = [];
        this.registerComplete = false;
    }
    onSubmit() {
        const accountAppService = new AccountAppService();
        const registerViewModel = {
            userName: this.username,
            email: this.email,
            password: this.password,
        };
        accountAppService.register(registerViewModel).then((response) => {
            if (!response.isError) {
                this.resultMessage = response.content.resultMessage;
                this.registerComplete = true;
            }
            else {
                this.errors = response.errors;
            }
        });
    }
};
RegisterComponent = tslib_1.__decorate([
    Component
], RegisterComponent);
export default RegisterComponent;
//# sourceMappingURL=register.js.map