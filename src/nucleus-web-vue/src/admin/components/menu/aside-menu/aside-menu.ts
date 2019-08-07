import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component } from 'vue-property-decorator';

@Component
export default class AsideMenuComponent extends NucleusComponentBase {
    public drawer = true;
    public isAdmin = false;

    get mainMenuItems() {
        return [
            { icon: 'mdi-home', text: this.$t('Home'), link: '/admin/home' }
        ];
    }

    get adminMenuItems() {
        return [
            { icon: 'mdi-account-multiple', text: this.$t('Users'), link: '/admin/user-list' },
            { icon: 'mdi-briefcase-account', text: this.$t('Roles'), link: '/admin/role-list' }
        ];
    }

    public mounted() {
        this.$root.$on('drawerChanged',
            () => {
                this.drawer = !this.drawer;
            });
    }
}