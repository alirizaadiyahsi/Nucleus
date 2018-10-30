import './assets/sass/site.css';
import 'vuetify/dist/vuetify.min.css';
import 'material-design-icons-iconfont/dist/material-design-icons.css';

import router from '@/router';
import Vue from 'vue';
import Vuetify from 'vuetify';

Vue.use(Vuetify);

const vue = new Vue({
    el: '#app-root',
    router,
    render: (h: any) => h(require('./App.vue').default),
});
