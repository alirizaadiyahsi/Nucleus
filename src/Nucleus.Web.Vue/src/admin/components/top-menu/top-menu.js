import * as tslib_1 from "tslib";
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AccountAppService from '../../../services/account-app-service';
let TopMenuComponent = class TopMenuComponent extends Vue {
    logOut() {
        const accountAppService = new AccountAppService();
        accountAppService.logOut();
        this.$router.push({ path: '/account/login' });
    }
};
TopMenuComponent = tslib_1.__decorate([
    Component
], TopMenuComponent);
export default TopMenuComponent;
//# sourceMappingURL=top-menu.js.map