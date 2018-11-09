import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        ChangePassword: require('@/admin/components/profile/change-password/change-password.vue').default
    }
})
export default class TopMenuComponent extends AppComponentBase {
    public drawer = true;

    public changePasswordDialogChanged(dialog: boolean) {
        this.$root.$emit('changePasswordDialogChanged', dialog);
    }

    public drawerChanged() {
        this.$root.$emit('drawerChanged');
    }

    public logOut() {
        this.authStore.removeToken();
        this.$router.push({ path: '/account/login' });
    }
}