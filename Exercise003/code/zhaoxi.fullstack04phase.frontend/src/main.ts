import { createApp } from 'vue'
import './assets/css/global.scss' // 导入自定义全局样式
import App from './App.vue'
import router from './router' // 导入路由
import ElementPlus from "element-plus" // 全局导入 Element-Plus
import "element-plus/dist/index.css" // 导入 Element-Plus 样式
import * as ElementPlusIconsVue from '@element-plus/icons-vue' // 注册所有图标

// createApp(App).mount('#app')
const app = createApp(App);
app.use(router);
app.use(ElementPlus);
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}
app.mount('#app');