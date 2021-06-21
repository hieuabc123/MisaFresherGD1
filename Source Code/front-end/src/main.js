import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import EmployeeList from './components/modules/Employee/EmployeeList.vue'
import VueTippy, { TippyComponent } from "vue-tippy"

Vue.config.productionTip = false
Vue.use(VueRouter)
    /**
     * Use Tooltip
     */
Vue.use(VueTippy, {
    directive: "tippy", // => v-tippy
    flipDuration: 0,
    popperOptions: {
        modifiers: {
            preventOverflow: {
                enabled: false
            }
        }
    }
});
Vue.component("tippy", TippyComponent);

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