import { __decorate } from "tslib";
import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component } from 'vue-property-decorator';
let AdminLayoutComponent = class AdminLayoutComponent extends NucleusComponentBase {
    created() {
        this.nucleus.auth.fillProps();
    }
};
AdminLayoutComponent = __decorate([
    Component({
        components: {
            AsideMenu: require('@/admin/components/menu/aside-menu/aside-menu.vue').default,
            TopMenu: require('@/admin/components/menu/top-menu/top-menu.vue').default
        }
    })
], AdminLayoutComponent);
export default AdminLayoutComponent;
//# sourceMappingURL=admin-layout.js.map