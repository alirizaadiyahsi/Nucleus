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
    public selectedLanguageName = '';

    public mounted() {
        this.selectedLanguageName = LanguageStore.getLanguage().languageName;
    }

    public changePasswordDialogChanged(dialog: boolean) {
        this.$root.$emit('changePasswordDialogChanged', dialog);
    }

    public drawerChanged() {
        this.$root.$emit('drawerChanged');
    }

    public changeLanguage(languageCode: string, languageName: string) {
        this.$i18n.locale = languageCode;
        this.selectedLanguageName = languageName;

        LanguageStore.setLanguage({
            languageCode,
            languageName
        } as any);
    }

    public logOut() {
        this.authStore.removeToken();
        this.$router.push({ path: '/account/login' });
    }
}