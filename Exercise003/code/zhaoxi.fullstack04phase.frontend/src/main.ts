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