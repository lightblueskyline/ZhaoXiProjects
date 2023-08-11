// 路由
import { createRouter, createWebHistory } from "vue-router";
import { SettingUserDynamicRouter, FormatToken, ValidTokenExpire } from "../tool/index";
import userStore from "../store/index";
import { ElMessage } from "element-plus";

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
                    path: "/",
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
router.beforeEach(async (to, _from, next) => {
    // 未登录时，重定向至登录页
    if (!userStore().token || userStore().token == "") {
        if (to.path != "/login") {
            next("/login");
        }
    } else {
        // 判断登录有效期，并且避免重定向次数过多
        let expire = FormatToken(userStore().token)?.exp as number;
        if (!ValidTokenExpire(expire) && to.path != "/login") {
            ElMessage.error("登录已过期，请重新登录！");
            next("/login");
        }
    }

    // 未登陆时，没有权限，无需读取路由
    if (to.path != "/login") {
        // 读取并设置动态路由
        await SettingUserDynamicRouter();
    }

    // 动态路由已经添加，但是刷新页面后 404
    // 由于在导航中动态添加的路由，刷新页面是无法读取(刷新页面时没有跳转所以没有触发导航机制)
    // 原因是动态添加的路由需要在下次导航时才生效
    if (to.name == "notfound" || to.name == "NotFound") {
        // 所以要进行手动跳转到动态添加的路由，但前提是跳转的 Path 在路由中已存在才行
        if (router.getRoutes().find(x => x.path == to.path)) {
            // 存在则跳转
            next(to.path);
        }
    }

    next();
});

// 导出变量
export default router;