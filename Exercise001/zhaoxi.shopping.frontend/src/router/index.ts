// 路由配置文件
import { createRouter, createWebHistory } from "vue-router";
import store from "../store/index"; // 全局状态管理
import { FormatToken, Valid, SettingUserRouter } from "../tools/index";
import { ElMessage } from "element-plus";

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
                // {
                //     name: "menu",
                //     path: "/menu",
                //     component: () => import("../views/admin/menu/Menu.vue")
                // },
                // {
                //     name: "role",
                //     path: "/role",
                //     component: () => import("../views/admin/role/Role.vue")
                // },
                // {
                //     name: "user",
                //     path: "/user",
                //     component: () => import("../views/admin/user/User.vue")
                // }
            ]
        },
    ]
});
// 需要在 beforeEach 中使用 pinia 否则无法成功初始化
router.beforeEach(async (to, from, next) => {
    console.log(from);
    // 判断是否存在 Token 不存在跳转至登录页
    if (store().Token == "" || !store().Token) {
        // 跳转至登录页
        if (to.path != "/login" && to.path != "/") {
            next("/login");
        }
    } else {
        // 已经登录，存在 Token
        // 判断有效期，并且避免无限重定向
        if (!Valid(FormatToken(store().Token)?.exp as number) && to.path != "/login") {
            ElMessage.warning("登录已过有效期，请重新登录！");
            next("/login");
        }
    }

    if (to.path != "/login") {
        await SettingUserRouter();
    }

    // 由于统一配置了 404 页面的原因，这里要判断一下当前是否已经进入 404 页面
    if (to.path == "notfound") {
        // 由于是在导航里动态添加路由，刷新页面时无法读取 (刷新页面时没有跳转所以没有触发导航机制)
        // 所以要进行手动跳转到动态添加的路由，但是前提是跳转的 path 在路由中存在才行
        if (router.getRoutes().find(x => x.path == to.path)) {
            // 经过一系列判断后没有问题则跳转
            next(to.path);
            return;
        }
    }
    next();
});
export default router;