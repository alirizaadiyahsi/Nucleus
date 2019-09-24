import Vue from "vue";
import Vuetify from "vuetify/lib";
import LanguageStore from '@/stores/language-store';
Vue.use(Vuetify);
import en from 'vuetify/src/locale/en';
import tr from 'vuetify/src/locale/tr';
export default new Vuetify({
    lang: {
        locales: { en, tr },
        current: LanguageStore.getLanguage().languageCode,
    },
    icons: {
        iconfont: "mdi"
    }
});
//# sourceMappingURL=vuetify.js.map