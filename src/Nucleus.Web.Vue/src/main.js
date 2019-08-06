import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import VueI18N from 'vue-i18n';
import './assets/sass/site.css';
import router from '@/router';

Vue.use(VueI18N);
Vue.config.productionTip = false;

// Ready translated locale messages
const messages = {
    en: {
        message: {
            hello: 'hello world'
        }
    },
    ja: {
        message: {
            hello: 'こんにちは、世界'
        }
    }
}

// Create VueI18n instance with options
const i18N = new VueI18N({
    locale: 'ja', // set locale
    messages // set locale messages
});

new Vue({
    i18n: i18N,
    vuetify,
    router,
    render: h => h(App)
}).$mount('#app');
