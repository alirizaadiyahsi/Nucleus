import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import VueI18N from 'vue-i18n';

Vue.use(Vuetify);
Vue.use(VueI18N);

const messages = {
    en: {
        $vuetify: {
            dataIterator: {
                rowsPerPageText: 'Items per page:',
                pageText: '{0}-{1} of {2}',
            },
        },
    },
    sv: {
        $vuetify: {
            dataIterator: {
                rowsPerPageText: 'Element per sida:',
                pageText: '{0}-{1} av {2}'
            }
        }
    }
}

// Create VueI18n instance with options
const i18N = new VueI18N({
    locale: 'sv', // set locale
    messages // set locale messages
});

export default new Vuetify({
    lang: {
        t: (key, ...params) => i18N.t(key, params)
    },
    icons: {
        iconfont: 'mdi'
    }
})
