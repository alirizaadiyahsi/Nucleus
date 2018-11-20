import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component } from 'vue-property-decorator';

@Component
export default class AsideMenuComponent extends NucleusComponentBase {
    public drawer = true;
    public isAdmin = false;

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