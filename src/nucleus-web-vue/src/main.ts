import './assets/sass/site.css';

import Vue from 'vue';
import App from './App.vue';
import router from './router';
import vuetify from './plugins/vuetify';
import VueI18n from 'vue-i18n';

Vue.use(VueI18n);

const locales = {
    en: require('@/assets/localization/shared-locales/en.json'),
    tr: require('@/assets/localization/shared-locales/tr.json')
};

const i18n = new VueI18n({
    locale: 'en',//LanguageStore.getLanguage().languageCode,
    fallbackLocale: 'en',
    messages: locales
});

Vue.config.productionTip = false;

new Vue({
    i18n,
    router,
    vuetify,
    render: h => h(App)
}).$mount("#app");
