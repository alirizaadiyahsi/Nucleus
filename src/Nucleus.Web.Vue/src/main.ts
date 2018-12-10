import 'material-design-icons-iconfont/dist/material-design-icons.css';
import 'vuetify/dist/vuetify.min.css';
import './assets/sass/site.css';

import LanguageStore from '@/stores/language-store';
import router from '@/router';
import Vue from 'vue';
import Vuetify from 'vuetify';
import VueI18n from 'vue-i18n';

Vue.use(VueI18n);

const locales = {
    en: require('@/assets/js/locales/en.json'),
    tr: require('@/assets/js/locales/tr.json')
};

const i18n = new VueI18n({
    locale: LanguageStore.getLanguage().languageCode,
    fallbackLocale: 'en',
    messages: locales
});

Vue.use(Vuetify, {
    lang: {
        t: (key: any, ...params: any[]) => i18n.t(key, params)
    }
});

const vue = new Vue({
    i18n,
    el: '#app-root',
    router,
    render: (h: any) => h(require('./App.vue').default)
});