import AppComponentBase from '@/shared/application/app-component-base';
import { Component } from 'vue-property-decorator';

@Component
export default class AsideMenuComponent extends AppComponentBase {
    public drawer = true;

    get mainMenuItems() {
        return [
            { icon: 'home', text: this.$t('Home'), link: '/admin/home' }
        ];
    }

    get adminMenuItems() {
        return [
            { icon: 'people', text: this.$t('Users'), link: '/admin/user-list' },
            { icon: 'work', text: this.$t('Roles'), link: '/admin/role-list' }
        ];
    }

    public mounted() {
        this.$root.$on('drawerChanged',
            () => {
                this.drawer = !this.drawer;
            });
    }
}