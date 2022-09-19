import { createApp } from 'vue';
import { createVuetify } from 'vuetify';
import vfmPlugin from 'vue-final-modal';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import App from './App.vue';
import router from './router';

const vuetify = createVuetify({
    components,
    directives,
});

createApp(App).use(router).use(vuetify).use(vfmPlugin).mount('#app');
