import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import LanguageStore from '@/stores/language-store';

@Component
export default class AccountLayoutComponent extends NucleusComponentBase {
    public selectedLanguage = {} as ILanguageDto;

    public created() {
        this.nucleus.auth.removeProps();
    }

    public beforeMount() {
        this.selectedLanguage = LanguageStore.getLanguage();
    }

    public changeLanguage(languageCode: string, languageName: string) {
        this.$i18n.locale = languageCode;
        this.selectedLanguage = { languageName, languageCode } as ILanguageDto;
        this.$vuetify.lang.current = languageCode;

        LanguageStore.setLanguage({
            languageCode,
            languageName
        } as any);
    }
}