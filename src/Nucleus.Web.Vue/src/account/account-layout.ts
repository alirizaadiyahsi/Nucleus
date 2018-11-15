import { Component } from 'vue-property-decorator';
import AppComponentBase from '@/shared/application/app-component-base';
import LanguageStore from '@/stores/language-store';

@Component
export default class AccountLayoutComponent extends AppComponentBase {
    public selectedLanguageName = '';

    public mounted() {
        this.selectedLanguageName = LanguageStore.getLanguage().languageName;
    }

    public changeLanguage(languageCode: string, languageName: string) {
        this.$i18n.locale = languageCode;
        this.selectedLanguageName = languageName;

        LanguageStore.setLanguage({
            languageCode,
            languageName
        } as any);
    }
}