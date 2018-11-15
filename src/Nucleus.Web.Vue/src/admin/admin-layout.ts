import AppComponentBase from '@/shared/application/app-component-base';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        AsideMenu: require('@/admin/components/menu/aside-menu/aside-menu.vue').default,
        TopMenu: require('@/admin/components/menu/top-menu/top-menu.vue').default
    }
})
export default class AdminLayoutComponent extends AppComponentBase {
}