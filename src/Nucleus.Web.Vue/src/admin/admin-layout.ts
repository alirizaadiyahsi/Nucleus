import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        AsideMenu: require('@/admin/components/aside-menu/aside-menu.vue').default,
        TopMenu: require('@/admin/components/top-menu/top-menu.vue').default
    }
})
export default class AdminLayoutComponent extends AppComponentBase {
}