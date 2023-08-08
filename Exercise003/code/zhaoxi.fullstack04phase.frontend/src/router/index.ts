// 路由
import { createRouter, createWebHistory } from "vue-router";
import { SettingUserDynamicRouter } from "../tool/index";

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
            path: "/:pathMatch(.*)",
            component: () => import("../views/index/NotFound.vue")
        },
        // 嵌入
        {
            name: "RootPage", // 登入后母页面
            path: "/",
            component: () => import("../views/index/RootPage.vue"),
            // 嵌套其中的页面 -> children
            children: [
                {
                    name: "Desktop",
                    path: "/desktop",
                    component: () => import("../views/index/Desktop.vue"),
                },
                {
                    name: "Person",
                    path: "/personpage",
                    component: () => import("../views/index/PersonPage.vue"),
                },
                // {
                //     name: "Menu",
                //     path: "/menu",
                //     component: () => import("../views/admin/menu/Menu.vue"),
                // },
                // {
                //     name: "Role",
                //     path: "/role",
                //     component: () => import("../views/admin/role/Role.vue"),
                // },
                // {
                //     name: "User",
                //     path: "/user",
                //     component: () => import("../views/admin/user/User.vue"),
                // }
            ]
        }
    ]
});

// 路由导航(到某页面之前的拦截)
// 当路由存在则跳转，不存在 404
router.beforeEach(async (to, from, next) => {
    // 未登陆时，没有权限，无需读取路由
    if (to.path != "/login") {
        console.log(from);
        // 读取并设置动态路由
        await SettingUserDynamicRouter();
    }
    next();
});

// 导出变量
export default router;