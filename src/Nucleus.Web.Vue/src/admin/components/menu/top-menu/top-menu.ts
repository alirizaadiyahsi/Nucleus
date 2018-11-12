import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';
import LanguageStore from '@/stores/language-store';

@Component({
    components: {
        ChangePassword: require('@/admin/components/profile/change-password/change-password.vue').default
    }
})
export default class TopMenuComponent extends AppComponentBase {
    public drawer = true;
    public selectedLanguage = {} as ILanguageDto;

    public mounted() {
        this.selectedLanguage = LanguageStore.getLanguage();
    }

    public changePasswordDialogChanged(dialog: boolean) {
        this.$root.$emit('changePasswordDialogChanged', dialog);
    }

    public drawerChanged() {
        this.$root.$emit('drawerChanged');
    }

    public changeLanguage(language: string, languageName: string) {
        LanguageStore.setLanguage({
            language: language,
            languageName: languageName
        } as any);
        window.location.reload();
    }

    public logOut() {
        this.authStore.removeToken();
        this.$router.push({ path: '/account/login' });
    }
}