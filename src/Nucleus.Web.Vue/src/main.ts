import './assets/sass/site.css';

import router from './router';
import Vue from 'vue';

import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';

Vue.use(ElementUI);

const vue = new Vue({
    el: '#app-root',
    router,
    render: (h: any) => h(require('./App.vue').default),
});

