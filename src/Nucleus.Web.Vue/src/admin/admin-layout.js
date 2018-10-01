import * as tslib_1 from "tslib";
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
let AdminLayoutComponent = class AdminLayoutComponent extends Vue {
};
AdminLayoutComponent = tslib_1.__decorate([
    Component({
        components: {
            NavMenuComponent: require('./components/nav-menu/nav-menu.vue').default,
            TopMenuComponent: require('./components/top-menu/top-menu.vue').default,
        },
    })
], AdminLayoutComponent);
export default AdminLayoutComponent;
//# sourceMappingURL=admin-layout.js.map