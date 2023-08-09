# 朝夕教育.全栈班.第04期.Vue实战

## 开发环境、技术栈

- 操作系统： Windows
- 开发工具： Visual Studio Code, Visual Studio 2022, MySQL
- Node.js
- 包管理器： npm, pnpm (二选一即可)
- 前端框架： Vue3.2
- 脚本语言： TypeScript
- 构建工具： Vite
- 后端框架： .NET7 WebAPI
- 数据访问： MySQL

## 创建配置前端项目

### 使用 Vite 构建

```bash
pnpm create vite zhaoxi.fullstack04phase.frontend
#
cd ./zhaoxi.fullstack04phase.frontend
pnpm install
pnpm run dev
```

### 修改 Vite 配置

```ts
// vite.config.ts
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    open: false,
    host: "127.0.0.1",
    // port: 5001
  }
})
```

## 安装和配置路由

`pnpm install vue-router@4`  

### 新建路由文件夹和配置

```ts
// src\router\index.ts
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
```

### 全局引入路由

```ts
// src\main.ts
import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router' // 导入路由

// createApp(App).mount('#app')
const app = createApp(App);
app.use(router);
app.mount('#app');
```

### App.vue 添加路由标签

```html
<script setup lang="ts">
</script>

<template>
  <router-view></router-view>
</template>

<style scoped></style>
```

## 安装 sass (选装)

`pnpm install sass` 方便书写层叠样式

```html
<style scoped lang="scss">
div {
  p {
    color: red;
  }
}
</style>
```

## Element-Plus 组件库

### 安装以及导入 Element-Plus

`pnpm install element-plus` 建议使用全局导入

```ts
// src\main.ts
import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router' // 导入路由
import ElementPlus from "element-plus" // 全局导入 Element-Plus
import "element-plus/dist/index.css" // 导入 Element-Plus 样式

// createApp(App).mount('#app')
const app = createApp(App);
app.use(router);
app.use(ElementPlus);
app.mount('#app');
```

### [按需导入](https://element-plus.org/zh-CN/guide/quickstart.html#%E6%8C%89%E9%9C%80%E5%AF%BC%E5%85%A5)

PS: **零基础建议完整引入**

### 登录页面设计和表单验证

导入资源文件至： `.\ZhaoXiProjects\Exercise003\code\zhaoxi.fullstack04phase.frontend\public`

## 无限层级菜单和组件递归

### 递归组件

```html
<!-- 封装树状菜单 -->
<script setup lang="ts">
import TreeMenuModel from '../class/TreeMenuModel'; // 导入模型

defineProps({
    listTreeMenuModel: Array<TreeMenuModel>
});
</script>

<template>
    <!-- 单极菜单 -->
    <el-menu-item :index="item.Index" v-for="item in listTreeMenuModel?.filter(x => x.Children.length == 0)">
        <el-icon><icon-menu /></el-icon>
        <span>{{ item.Name }}</span>
    </el-menu-item>
    <!-- 多级菜单 -->
    <el-sub-menu :index="item.Index" v-for="item in listTreeMenuModel?.filter(x => x.Children.length > 0)">
        <template #title>
            <el-icon>
                <location />
            </el-icon>
            <span>{{ item.Name }}</span>
        </template>
        <!-- 递归无限层级菜单(自己调用自己) -->
        <!-- <el-menu-item :index="subitem.Index" v-for="subitem in item.Children.filter(x => x.Children.length == 0)">
            <el-icon><icon-menu /></el-icon>
            <span>{{ subitem.Name }}</span>
        </el-menu-item> -->
        <TreeMenu :listTreeMenuModel="item.Children"></TreeMenu>
    </el-sub-menu>
</template>
```

## 通过 DIV 处理排序

```html
<template>
    <!-- div 导致菜单折叠时变形 -->
    <div v-for="item in listTreeMenuModel">
        <!-- 单极菜单 -->
        <el-menu-item :index="item.Index" v-if="item.Children.length == 0">
            <el-icon><icon-menu /></el-icon>
            <span>{{ item.Name }}</span>
        </el-menu-item>
        <!-- 多级菜单 -->
        <el-sub-menu :index="item.Index" v-else>
            <template #title>
                <el-icon>
                    <location />
                </el-icon>
                <span>{{ item.Name }}</span>
            </template>
            <!-- 递归无限层级菜单(自己调用自己) -->
            <TreeMenu :listTreeMenuModel="item.Children"></TreeMenu>
        </el-sub-menu>
    </div>
</template>
```

## 安装图标

`pnpm install @element-plus/icons-vue`

## 菜单中的 template & span

## 折叠图标

```html
<el-header>
    <!-- 折叠菜单 -->
    <!-- <el-radio-group v-model="isCollapse" style="margin-bottom: 20px">
        <el-radio-button :label="false">expand</el-radio-button>
        <el-radio-button :label="true">collapse</el-radio-button>
    </el-radio-group> -->
    <!-- 折叠菜单 使用图标 -->
    <!-- <el-icon>
        <Expand />
    </el-icon> -->
    <!-- 折叠菜单 使用自定义图标组件 -->
    <IconComp IconName="Expand"></IconComp>
</el-header>
```

## 全局状态管理(pinia)以及持久化方案

安装： `pnpm install pinia`  
使用：

```ts
// src\main.ts
import { createPinia } from "pinia"; // 全局导入 pinia

app.use(createPinia());
```

```ts
// src\store\index.ts
// pinia 全局状态管理
import { defineStore } from "pinia";

const userStore = defineStore("main", {
    state: () => {
        return {
            isCollapse: false // 菜单默认打开
        }
    }
});

// 导出
export default userStore;
```

pinia 持久化插件： `pnpm install pinia-plugin-persist`

```ts
import piniaPluginPersist from "pinia-plugin-persist"; // 导入 pinia 持久化插件

app.use(createPinia().use(piniaPluginPersist));
```

## 工作台

国际化引入报错，处理方式： `src\vite-env.d.ts` 添加  
`declare module 'element-plus/dist/locale/zh-cn.mjs';`

## 后端项目创建、配置、编码

后端解决方案解构

- ExecWebAPI (暴露的 API)
- Model
  - 数据库实体
  - DTO (数据传输对象)
  - 公共模型
- Interface (接口层)
- Service (实现层)

项目(ExecWebAPI, Model)安装包： `SqlSugarCore`  
项目(ExecWebAPI)安装包： `Autofac, Autofac.Extensions.DependencyInjection, AutoMapper, AutoMapper.Extensions.Microsoft.DependencyInjection, Microsoft.AspNetCore.Mvc.NewtonsoftJson`  
项目(Service)安装包： `Microsoft.AspNetCore.Authentication.JwtBearer, Microsoft.IdentityModel.Tokens, System.IdentityModel.Tokens.Jwt, AutoMapper`  
项目(Interface)安装包： `Microsoft.AspNetCore.Http.Features`

## 文件上传

## 头像图片上传策略模式

基本策略抽象类、两种策略实现类、文件上传服务接口、文件上传服务类、文件上传上下文类(通过此类调用具体策略，已上传头像图片)、上传类型枚举类、文件上传工厂类(创建策略实体)  
**PS:** 若要通过浏览器浏览图片，需要配置静态文件中间件

## Axios 请求 WebAPI

安装： `pnpm install axios`  
处理跨域问题：

```ts
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    open: false, // 打开浏览器
    host: "127.0.0.1", // 设置主机
    // port: 5001, // 设置端口
    proxy: {
      "/api": {
        // 转发地址
        target: "http://localhost:5271/api",
        // 启用跨域访问
        changeOrigin: true,
        // 修改请求路径
        rewrite: path => path.replace(/^\/api/, "")
      }
    }
  }
});
```

## Vite 动态路由

`const nodeFiles = import.meta.glob(["../views/*/*.vue", "../views/*/*/*.vue", "../views/*/*/*.vue"]);`

## 动态路由 404 问题

```ts
// 路由导航(到某页面之前的拦截)
// 当路由存在则跳转，不存在 404
router.beforeEach(async (to, from, next) => {
    // 未登陆时，没有权限，无需读取路由
    if (to.path != "/login") {
        console.log(from);
        // 读取并设置动态路由
        await SettingUserDynamicRouter();
    }

    // 未登录时，重定向至登录页
    if (!userStore().token || userStore().token == "") {
        if (to.path != "/login") {
            next("/login");
        }
    } else {
        // Todo 判断登录有效期，并且避免重定向次数过多
    }

    // 动态路由已经添加，但是刷新页面后 404
    // 由于在导航中动态添加的路由，刷新页面是无法读取(刷新页面时没有跳转所以没有触发导航机制)
    // 原因是动态添加的路由需要在下次导航时才生效
    console.log(router.getRoutes());
    if (to.name == "notfound") {
        // 所以要进行手动跳转到动态添加的路由，但前提是跳转的 Path 在路由中已存在才行
        if (router.getRoutes().find(x => x.path == to.path)) {
            // 存在则跳转
            next(to.path);
        }
    }

    next();
});
```
