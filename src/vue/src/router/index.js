import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Main from '../views/Main.vue'

import RightManagement from '../views/User/RightManagement.vue'
import RoleManagement from '../views/User/RoleManagement.vue'
import UserManagement from '../views/User/UserManagement.vue'
Vue.use(VueRouter)

const routes = [
    {
        path: '/home',
        name: 'Home',
        component: Home,
        children: [
            {
                // main 会被渲染在 home 的 <router-view> 中
                path: 'main',
                component: Main,
                name: '主页',
            },
            {
                path: 'UserManagement',
                component: UserManagement,
                name: '用户管理',
            },
            {
                path: 'RightManagement',
                component: RightManagement,
                name: '功能管理',
            },
            {
                path: 'RoleManagement',
                component: RoleManagement,
                name: '角色管理',
            },
        ],
    },
    {
        path: '/about',
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () =>
            import(/* webpackChunkName: "about" */ '../views/About.vue'),
    },
    {
        path: '/login',
        name: 'Login',
        component: Login,
    },
    {
        path: '/',
        redirect: '/login',
    },
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes,
})

export default router
