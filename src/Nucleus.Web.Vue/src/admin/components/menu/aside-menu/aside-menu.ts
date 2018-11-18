import AppComponentBase from '@/shared/application/app-component-base';
import { Component } from 'vue-property-decorator';

@Component
export default class AsideMenuComponent extends AppComponentBase {
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
        const input: IIsUserInRoleInput = {
            userNameOrEmail: this.authStore.getTokenData().sub,
            roleName: 'Admin'
        };
        const query = '?' + this.queryString.stringify(input);
        this.appService.get<boolean>('/api/account/isUserInRole' + query)
            .then((response) => {
                this.isAdmin = response.content;
            });

        this.$root.$on('drawerChanged',
            () => {
                this.drawer = !this.drawer;
            });
    }
}