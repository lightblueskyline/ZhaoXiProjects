// 路由
import { createRouter, createWebHistory } from "vue-router";

// 创建路由对象
const router = createRouter({
    // 历史模式
    history: createWebHistory(),
    // 路由规则
    routes: [
        {
            name: "LoginPage",
            path: "/login",
            component: () => import("../views/index/LoginPage.vue")
        },
        {
            name: "TestPage", // 作为首页
            path: "/",
            component: () => import("../views/index/TestPage.vue")
        }
    ]
});

// 导出变量
export default router;