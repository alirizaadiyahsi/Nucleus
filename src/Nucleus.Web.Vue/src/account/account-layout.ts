import { Component } from 'vue-property-decorator';
import AppComponentBase from '@/shared/application/app-component-base';
import LanguageStore from '@/stores/language-store';

@Component
export default class AccountLayoutComponent extends AppComponentBase {
    public selectedLanguage = {} as ILanguageDto;

    public beforeMount() {
        this.selectedLanguage = LanguageStore.getLanguage();
    }

    public changeLanguage(languageCode: string, languageName: string) {
        this.$i18n.locale = languageCode;
        this.selectedLanguage = { languageName, languageCode } as ILanguageDto;

        LanguageStore.setLanguage({
            languageCode,
            languageName
        } as any);
    }
}