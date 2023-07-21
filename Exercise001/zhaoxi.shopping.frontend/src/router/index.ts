// 路由配置文件
import { createRouter, createWebHistory } from "vue-router";
const router = createRouter({
    history: createWebHistory(),
    routes: [
        // 整个画布显示
        {
            // 登录页
            name: "login",
            path: "/login",
            component: () => import("../views/index/LoginPage.vue")
        },
        {
            // 404 页面，通过正则匹配
            name: "notfound",
            path: "/:pathMatch(.*)",
            component: () => import("../views/index/NotFound.vue")
        },
        // 非整个画布显示
        {
            name: "admin",
            path: "/admin",
            component: () => import("../views/index/AdminPage.vue"),
            // 嵌入子页面
            children: [
                {
                    name: "desktop",
                    path: "/desktop",
                    component: () => import("../views/index/Desktop.vue")
                },
                {
                    name: "person",
                    path: "/person",
                    component: () => import("../views/index/PersonPage.vue")
                },
                {
                    name: "menu",
                    path: "/menu",
                    component: () => import("../views/admin/menu/Menu.vue")
                },
                {
                    name: "role",
                    path: "/role",
                    component: () => import("../views/admin/role/Role.vue")
                },
                {
                    name: "user",
                    path: "/user",
                    component: () => import("../views/admin/user/User.vue")
                }
            ]
        },
    ]
});
export default router;