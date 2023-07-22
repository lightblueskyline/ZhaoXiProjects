import { createApp } from 'vue'
import App from './App.vue'
import router from './router' // 导入路由配置
import ElementPlus from 'element-plus' // 导入组件
import 'element-plus/dist/index.css' // 导入样式
import './assets/css/global.scss' // 导入自定义的全局样式
import * as ElementPlusIconsVue from '@element-plus/icons-vue' // 导入图标
import { createPinia } from "pinia" // 全局状态管理
import piniaPluginPersist from 'pinia-plugin-persist' // 全局状态管理
import zhCn from 'element-plus/dist/locale/zh-cn.mjs' // Element-Plus 国际化

// createApp(App).mount('#app')
// createApp(App).use(router).mount('#app')
const app = createApp(App);
app.use(createPinia().use(piniaPluginPersist)); // 全局状态管理
// app.use(ElementPlus);
app.use(ElementPlus, {
    locale: zhCn // Element-Plus 国际化
});
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component);
} // 使用图标
app.use(router);
app.mount('#app');
