import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import VueTheMask from 'vue-the-mask';
import VueSweetAlert2 from 'vue-sweetalert2';

import 'sweetalert2/dist/sweetalert2.min.css';

const sweetAlertOptions = {
  confirmButtonColor: 'var(--v-success-base)',
  cancelButtonColor: 'var(--v-error-base)',
  background: 'var(--v-primary-base)',
  color: 'white',
  inputLabel: 'white'
};

Vue.use(VueTheMask);
Vue.use(VueSweetAlert2, sweetAlertOptions);

require('./components/');
import './assets/scripts/formatter'

Vue.config.productionTip = false

new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
