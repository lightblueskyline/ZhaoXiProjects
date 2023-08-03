import { createApp } from 'vue'
import './assets/css/global.scss' // 导入自定义全局样式
import App from './App.vue'
import router from './router' // 导入路由
import ElementPlus from "element-plus" // 全局导入 Element-Plus
import zhCn from 'element-plus/dist/locale/zh-cn.mjs' // 国际化
import "element-plus/dist/index.css" // 导入 Element-Plus 样式
import * as ElementPlusIconsVue from '@element-plus/icons-vue' // 注册所有图标
import { createPinia } from "pinia"; // 全局导入 pinia
import piniaPersist from "pinia-plugin-persist"; // 导入 pinia 持久化插件

// createApp(App).mount('#app')
const app = createApp(App);

app.use(router);

app.use(ElementPlus, { locale: zhCn });

// const pinia = createPinia();
// pinia.use(piniaPersist);
// app.use(pinia);
app.use(createPinia().use(piniaPersist));

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}

app.mount('#app');