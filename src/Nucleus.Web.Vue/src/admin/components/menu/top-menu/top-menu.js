import { __decorate } from "tslib";
import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component } from 'vue-property-decorator';
import LanguageStore from '@/stores/language-store';
let TopMenuComponent = class TopMenuComponent extends NucleusComponentBase {
    constructor() {
        super(...arguments);
        this.drawer = true;
        this.selectedLanguage = {};
    }
    beforeMount() {
        this.selectedLanguage = LanguageStore.getLanguage();
    }
    changePasswordDialogChanged(dialog) {
        this.$root.$emit('changePasswordDialogChanged', dialog);
    }
    drawerChanged() {
        this.$root.$emit('drawerChanged');
    }
    changeLanguage(languageCode, languageName) {
        this.$i18n.locale = languageCode;
        this.selectedLanguage = { languageName, languageCode };
        this.$vuetify.lang.current = languageCode;
        LanguageStore.setLanguage({
            languageCode,
            languageName
        });
    }
    logOut() {
        this.authStore.removeToken();
        this.$router.push({ path: '/account/login' });
    }
};
TopMenuComponent = __decorate([
    Component({
        components: {
            ChangePassword: require('@/admin/components/profile/change-password/change-password.vue').default
        }
    })
], TopMenuComponent);
export default TopMenuComponent;
//# sourceMappingURL=top-menu.js.map