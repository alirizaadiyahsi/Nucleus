import { Component } from 'vue-property-decorator';
import AccountAppService from '@/services/account/account-app-service';
import AppComponentBase from '@/models/shared/app-component-base';

@Component
export default class TopMenuComponent extends AppComponentBase {
    public logOut() {
        const accountAppService = new AccountAppService();
        accountAppService.logOut();
        this.$router.push({ path: '/account/login' });
    }
}
