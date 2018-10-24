import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AccountAppService from '../../../services/account/account-app-service';

@Component
export default class TopMenuComponent extends Vue {
    public logOut() {
        const accountAppService = new AccountAppService();
        accountAppService.logOut();
        this.$router.push({ path: '/account/login' });
    }
}
