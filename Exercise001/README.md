# 朝夕教育

R19-2023年4月录制Vue3+.NET7电商管理后台---VIP--完整53P

## 开发环境

- 操作系统： Windows
- 开发工具： Visual Studio 2022 & Visual Studio Code
- Node.js 版本： 18+
- 包管理器： npm 9.3.1 & pnpm 7.16.1(选装)
- 前端框架： Vue3.2
- 后端框架： .NET7
- 数据服务： MySQL

## 技术栈

- 前端体系： Vue3, Vite, TypeScript, Sass, Router, Element Plus, Axios, Pinia
- 后端体系： .NET7, WebAPI, SqlSugar, Autofac, JWT

## 后端项目

### 项目分层、数据库设计

- Class Library (Model, Interface, Service)
- 数据库表 (Log4Net, Menu, MenuRoleRelation, Role, UserRoleRelation, Users)

### Autofac 的使用

目的： 批量注册  
用以替换内置 IOC  
安装 NuGet 包： Autofac, Autofac.Extensions.DependencyInjection

### Automapper 的安装和配置

作用： DTO 与实体之间的映射，替代手工赋值  
安装 NuGet 包： AutoMapper, AutoMapper.Extensions.Microsoft.DependencyInjection

### SqlSugar 的介绍和安装

安装 NuGet 包： SqlSugarCore

### CodeFirst 的实现

1. 根据字符串中的配置信息动态生成数据库
2. 通过反射生成表结构
3. 添加测试数据

### 创建 MySQL 数据库

```sql
-- sudo mysql -u root -p
-- 创建用户
CREATE USER 'ZhaoXiProjects'@'%' IDENTIFIED WITH mysql_native_password BY 'P@ssw0rd';
-- 创建数据库
CREATE DATABASE ZhaoXiExercise001;
-- 授予权限
GRANT ALL PRIVILEGES ON ZhaoXiExercise001.* TO 'ZhaoXiProjects'@'%';
```

### 使用 JWT 生成 Token

安装 NuGet 包： Microsoft.AspNetCore.Authentication.JwtBearer

```json
// C:\Z_Documents\ZhaoXiProjects\Exercise001\Zhaoxi.Shopping.Backend\ExeWebApi\appsettings.json
"JWTTokenOptions": {
  "Audience": "http://localhost:5148",
  "Issuer": "http://localhost:5148",
  "SecurityKey": "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB"
}
```

### JSON 格式化

安装 NuGet 包： Microsoft.AspNetCore.Mvc.NewtonsoftJson

```csharp
// C:\Z_Documents\ZhaoXiProjects\Exercise001\Zhaoxi.Shopping.Backend\ExeWebApi\Program.cs
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // 忽略循环引用
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    // 统一设置 API 日期格式
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // 设置 JSON 返回格式同 Model 一致 (默认 JSON 中的首字母为小写，这里修改为同后端 Model 一致)
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
```

### Swagger 配置

1. Authorization
2. SchemaFilter

### 文件上传服务设计

1. 多种上传场景分析
2. 策略 + 工厂模式的讲解和应用

### 本地上传功能实现

### 七牛云上传功能实现

注册云账号： `https://portal.qiniu.com/signup`  
个人中心获取密钥信息  
安装 SDK 编写上传逻辑

### 个人信息编辑接口

### 菜单模块 CRUD 接口

### 角色模块 CRUD 接口

### 用户模块 CRUD 接口

### Controller 层补充

## 前端项目

### 使用 Vite 构建 Vue3 + TypeScript 项目

`npm install -g npm@9.8.1`  
`npm install -g pnpm`  
创建项目命令(文件名小写)： `pnpm create vite@latest web`  
进入选择界面之后选择 Vue 回车后再选择 TypeScript 最后回车继续  
cd web 进入目录  
`pnpm install` 初始化项目，安装依赖包  
`pnpm run dev` `pnpm dev` 启动项目

### 修改项目配置

```ts
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    open: true, // 启动项目后，自动打开浏览器
    host: "127.0.0.1", // 设置主机
    // port: 5001, // 设置端口
  }
})
```

### 安装和配置路由

1. 安装命令： `pnpm install vue-router@4`
2. 新建相关页面
3. src 下新增 router 文件夹与其中建立 index.ts 路由配置文件
4. 编写路由配置到 index.ts
5. main.ts 中引入路由
6. App.vue 中使用 router-view 标签显示路由页面

'admin' 文件夹放置动态路由相关页面  
'index' 文件夹放置非动态路由相关，所有人可见的页面

### 安装和配置 Element Plus

安装： `pnpm install element-plus`  
导入方式： 全局导入、单独导入、自动导入  
图标需要单独安装： `pnpm install @element-plus/icons-vue`

### 安装 Sass 添加和引用全局样式

安装： `pnpm install sass`  
assets 文件夹下新建 global.scss 文件，添加全局 CSS 并到 main.ts 中引入

### 设计并完成登录页静态效果

### 登录页表单验证逻辑

模型绑定，模板引用，设置表单验证规则  
:model="form" ref="loginForm" :rules="rules"  
导入类型：  
`import {FormInstance, FormRules} from 'element-plus'`  
`const rules = reactive<FormRules>({})`  
`const onSubmit = async (loginForm: FormInstance | undefined) => {}`

### 后台界面设计以及嵌套路由

### 图标组件的封装

图标的多种使用方式：

1. 通过标签直接使用
2. 通过 Button 按钮+图标组合使用
3. 通过 component 标签实现自定义

### 左侧菜单模块的实现

Element-Plus 菜单组件的应用  
菜单组件的封装，递归调用实现无限层级

### 全局状态管理 pinia 的安装

安装： `pnpm install pinia`  
优点： 跨组件/页面共享状态、热模块更换、服务端渲染...  
持久化： `pnpm install pinia-plugin-persist`

### Header 组件的封装

面包屑组件  
下拉菜单 (昵称、退出)  
Tags 标签导航

### 菜单折叠效果的实现

定义全局变量  
处理切换逻辑

### Tags 标签导航功能实现

从全局状态中读取 Tags  
设置 Tags `handleSelect(index)`  
点击 Tags `clickTag(index)`

### 工作台界面设计，以及逻辑交互

日历组件  
设置代办项  
多语言设置，以及声明文件问题

### Element-Plus 国际化

### 个人信息界面设计

`pnpm install jwt-decode`

### 菜单列表静态页

### 角色列表静态页

### 用户列表静态页

### Axios 的介绍和安装

Axios 是一个基于 promise 的网络请求库，可用于浏览器和 Node.js  
Axios 使用简单，包尺寸小且提供了易于扩展的接口  
安装命令： `pnpm install axios`

```ts
import axios from 'axios';

axios.get('/users')
    .then(result => {
      console.log(result.data);
    });
```

### Axios 拦截器的使用

创建一个拦截器对象 `const instance = axios.create({});`  
请求之前做拦截 `instance.interceptors.request.use()`  
得到响应结果之后做拦截 `instance.interceptors.resonse.use()`

### HTTP 请求的封装

```ts
// vite.config.ts
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    open: false, // true 启动项目后，是否自动打开浏览器
    host: "127.0.0.1", // 设置主机
    port: 5148, // 设置端口
    proxy: {
      "/api": {
        target: "http://localhost:5148/api", // 本地代理地址
        changeOrigin: true, // 启用跨域访问
        rewrite: path => path.replace(/^\/api/, ""), // 修改请求路径
      }
    }
  }
});
```

### 对接登录页

### 路由守卫与动态路由 (动态路由)

router.beforeEach  
SettingUserRouter

### 菜单列表页数据对接

### 菜单编辑页数据对接

### 角色列表页数据对接

### 角色编辑以及设置页面数据对接

### 用户板块数据对接

### 个人信息模块数据对接

### 登录信息显示以及退出功能

### Token 无感刷新

### 发布部署

前端打包： `pnpm builder`
