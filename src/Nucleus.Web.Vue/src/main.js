import './assets/sass/site.css';
import Vue from 'vue';
import App from './App.vue';
import router from './router';
import vuetify from './plugins/vuetify';
import VueI18n from 'vue-i18n';
import LanguageStore from '@/stores/language-store';
Vue.use(VueI18n);
const locales = {
    en: require('@/assets/localizations/en.json'),
    tr: require('@/assets/localizations/tr.json')
};
const i18n = new VueI18n({
    locale: LanguageStore.getLanguage().languageCode,
    fallbackLocale: 'en',
    messages: locales
});
Vue.config.productionTip = false;
new Vue({
    i18n,
    router,
    vuetify,
    render: h => h(App)
}).$mount('#app');
//# sourceMappingURL=main.js.map