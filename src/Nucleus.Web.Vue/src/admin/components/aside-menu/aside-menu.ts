import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';

@Component
export default class AsideMenuComponent extends AppComponentBase {
    public drawer = true;

    public mainMenuItems = [
        { icon: 'home', text: 'Home', link: '/admin/home' },
        { icon: 'apps', text: 'Counter', link: '/admin/counter' }
    ];

    public adminMenuItems = [
        { icon: 'people', text: 'Users', link: '/admin/user-list' },
        { icon: 'work', text: 'Roles', link: '/admin/role-list' }
    ];

    public mounted() {
        this.$root.$on('drawerChanged',
            () => {
                this.drawer = !this.drawer;
            });
    }
}