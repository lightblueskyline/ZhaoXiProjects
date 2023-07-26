# 朝夕教育.全栈班.第04期

## Vue 学习

### 环境准备

安装 Node.js

```bash
# 查看版本
node --version

# 查看(npm)包管理器版本
npm --version

# 解决 npm 下载慢的问题
# 查看镜像源
npm get registry
# 修改镜像源
npm config set registry http://registry.npm.taobao.org/
```

### 包管理器

1. npm
2. yarn
3. pnpm (推荐。速度快，节省磁盘空间)

**pnpm** 安装命令 `npm install -g pnpm`  
`npm install -> pnpm install`  
`npm i <pkg> -> pnpm add <pkg>`  
`npm run <cmd> -> pnpm <cmd>`

### 创建 Vue 应用

`npm init vue@latest` 之后出现的选项默认全部回车  
`cd <目录名>`  
`npm install` 安装对应包  
`npm run dev` 启动运行  
推荐的 IDE 配置是： Visual Studio Code + Volar 扩展

### 使用 pnpm 创建 Vue 应用

```bash
# 安装 pnpm
npm install -g pnpm
#
pnpm create vite vue-project-vite-ts
#
cd ./vue-project-vite-ts
pnpm install
# 简化版命令
pnpm i
pnpm run dev
pnpm dev
# 生成命令
pnpm build
```

|目录名称|作用|
|----|----|
|.vscode|vscode 配置|
|public|公共资源，该目录不会被打包|
|src|项目目录|
|src\assets|静态资源目录，会被编译和打包|
|src\components|组件|
|src\App.vue|根组件|
|src\main.ts|主函数|
|src\style.css|样式表|
|src\vite-env.d.ts|Vite 声明文件|
|index.html|起始页|
|package.json|项目配置 Node.js 初始化后生成(npm init)|
|tsconfig.json|TS 配置文件(浏览器环境，该配置引用了下面的 tsconfig.node.json)|
|tsconfig.node.json|TS 配置文件(Node 环境)|
|vite.config.ts|Vite 配置文件|

#### package.json

|节点名称|作用|
|----|----|
|scripts|项目启动、编译和预览的脚本，可以自定义命令实现个性化|
|dependencies|生产环境依赖包|
|devDependencies|开发环境依赖包|

```json
{
  "name": "vue-project-vite-ts",
  "private": true,
  "version": "0.0.0",
  "type": "module",
  "scripts": {
    "startApp": "vite", // 修改启动命令 pnpm startApp = pnpm run dev = pnpm dev
    "dev": "vite",
    "build": "vue-tsc && vite build",
    "preview": "vite preview"
  },
  "dependencies": {
    "vue": "^3.3.4"
  },
  "devDependencies": {
    "@vitejs/plugin-vue": "^4.2.3",
    "typescript": "^5.0.2",
    "vite": "^4.4.5",
    "vue-tsc": "^1.8.5"
  }
}
```

#### vite.config.ts

```ts
// vue-project-vite-ts\vite.config.ts
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    host: "127.0.0.1",
    // port:3000,
    open: false // 是否自动打开浏览器 True->Auto Open Browser
  }
})
```
