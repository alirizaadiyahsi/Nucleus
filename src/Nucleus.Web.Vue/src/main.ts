import './assets/styles/styles.scss';
import 'primevue/resources/themes/tailwind-light/theme.css';
import 'primevue/resources/primevue.min.css';    
import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';

import {createApp} from "vue";
import App from "./App.vue";
import router from "./router";
import PrimeVue from 'primevue/config';
import { createI18n } from 'vue-i18n'
import LanguageStore from "@/core/stores/language-store";

import Button from "primevue/button";
import Card from 'primevue/card';
import Divider from "primevue/divider";
import Dropdown from "primevue/dropdown";
import InputText from "primevue/inputtext";
import Message from 'primevue/message';

const i18n = createI18n({
    locale: LanguageStore.getLanguage().code,
    fallbackLocale: 'en',
    messages: {
        en: require('@/assets/localizations/en.json'),
        tr: require('@/assets/localizations/tr.json')
    }
})

const app = createApp(App);
app.use(router).use(PrimeVue).use(i18n).mount("#app");

app.component('Button', Button);
app.component('Card', Card);
app.component('Divider', Divider);
app.component('Dropdown', Dropdown);
app.component('InputText', InputText);
app.component('Message', Message);