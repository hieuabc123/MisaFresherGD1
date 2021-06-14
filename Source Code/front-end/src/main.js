import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import EmployeeList from './components/modules/Employee/EmployeeList.vue'

Vue.config.productionTip = false
Vue.use(VueRouter)

const routes = [
    { path: "/employee", component: EmployeeList }
]

const router = new VueRouter({
    mode: "history",
    routes
})

new Vue({
    render: h => h(App),
    router
}).$mount('#app')