import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        AsideMenu: require('./components/aside-menu/aside-menu.vue').default,
        TopMenu: require('./components/top-menu/top-menu.vue').default
    }
})
export default class AdminLayoutComponent extends AppComponentBase {
}