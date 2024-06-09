import 'default-passive-events'
import Vue from 'vue'
import App from './App.vue'

import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import router from './router'
import store from './store'

Vue.config.productionTip = true
Vue.use(ElementUI)

new Vue({
    router,
    store,
    render: h => h(App),
}).$mount('#app')

if (window.Cypress) {
    window.Vue = Vue
}
