// 路由
import { createRouter, createWebHistory } from "vue-router";

// 创建路由对象
const router = createRouter({
    // 历史模式
    history: createWebHistory(),
    // 路由规则
    routes: [
        // 全局
        {
            name: "LoginPage",
            path: "/login",
            component: () => import("../views/index/LoginPage.vue")
        },
        {
            name: "NotFound",
            path: "/notfound",
            component: () => import("../views/index/NotFound.vue")
        },
        // 嵌入
        {
            name: "RootPage", // 作为首页
            path: "/",
            component: () => import("../views/index/RootPage.vue"),
            // 嵌套其中的页面 -> children
            children: [
                {
                    name: "TestPage",
                    path: "/testpage",
                    component: () => import("../views/index/TestPage.vue"),
                }
            ]
        }
    ]
});

// 导出变量
export default router;