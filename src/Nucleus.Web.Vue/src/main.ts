import './assets/sass/site.css';
import 'bootstrap/dist/css/bootstrap.css';

import router from './router';
import Vue from 'vue';

const vue = new Vue({
    el: '#app-root',
    router,
    render: (h: any) => h(require('./App.vue').default),
});
