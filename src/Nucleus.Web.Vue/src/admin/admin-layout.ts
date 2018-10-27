import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        SideMenuComponent: require('./components/side-menu/side-menu.vue').default,
        TopMenuComponent: require('./components/top-menu/top-menu.vue').default,
    },
})
export default class AdminLayoutComponent extends AppComponentBase {
}
