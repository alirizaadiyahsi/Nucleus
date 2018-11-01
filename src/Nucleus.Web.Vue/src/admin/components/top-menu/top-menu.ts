import { Component } from 'vue-property-decorator';
import AppComponentBase from '@/infrastructure/core/app-component-base';
import AuthStore from '@/stores/auth-store';

@Component
export default class TopMenuComponent extends AppComponentBase {
    // todo: use drawer in parent
    public drawer = true;
    public logOut() {
        AuthStore.removeToken();
        this.$router.push({ path: '/account/login' });
    }
}