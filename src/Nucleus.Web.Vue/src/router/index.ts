import {createRouter, createWebHashHistory, RouteRecordRaw} from "vue-router";
import AuthStore from "@/core/stores/auth-store";

const routes: Array<RouteRecordRaw> = [
    {path: '/', redirect: '/admin/home'},
    {
        path: '/admin',
        component: () => import("@/admin/admin-layout.vue"),
        meta: { requiresAuth: true },
        children: [
            {path: 'home', component: () => import('@/admin/views/home/home.vue')},
            {path: 'roles', component: () => import('@/admin/views/roles/role-list.vue')},
            {path: 'users', component: () => import('@/admin/views/users/user-list.vue')}
        ]
    },
    {
        path: "/identity",
        component: () => import("@/identity/identity-layout.vue"),
        children: [
            {
                path: "login",
                component: () => import("@/identity/views/login/login.vue"),
            },
            {
                path: "register",
                component: () => import("@/identity/views/register/register.vue"),
            }
        ],
    }
];

const router = createRouter({
    history: createWebHashHistory(),
    routes,
});

router.beforeEach((to: any, from: any, next: any) => {
    if (to.matched.some((record: any) => record.meta.requiresAuth)) {
        if (!AuthStore.isSignedIn()) {
            next({
                path: '/identity/login',
                query: { redirect: to.fullPath }
            });
        }
    }
    next();
});

export default router;
