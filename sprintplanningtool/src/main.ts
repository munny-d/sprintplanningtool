import { createApp } from 'vue';
import { BootstrapVue3 } from 'bootstrap-vue-3';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue-3/dist/bootstrap-vue-3.css';
import vfmPlugin from 'vue-final-modal';
import App from './App.vue';
import router from './router';

createApp(App).use(router).use(BootstrapVue3).use(vfmPlugin).mount('#app');
