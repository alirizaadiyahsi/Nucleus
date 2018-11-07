import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';
import AuthStore from '@/stores/auth-store';

@Component
export default class TopMenuComponent extends AppComponentBase {
    // todo: add profile dropdown menu to toolbar and show user name as menu name
    // todo: add components for each profile menu item

    public drawer = true;

    public drawerChanged() {
        this.$root.$emit('drawerChanged');
    }

    public logOut() {
        AuthStore.removeToken();
        this.$router.push({ path: '/account/login' });
    }
}