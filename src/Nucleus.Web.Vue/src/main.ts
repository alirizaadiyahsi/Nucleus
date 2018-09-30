import './assets/sass/site.css';
import router from './router';
import Vue from 'vue';

let vue = new Vue({
    el: '#app-root',
    router: router,
    render: h => h(require('./app.vue').default)
});