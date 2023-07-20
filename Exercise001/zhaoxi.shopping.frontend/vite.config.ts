import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    open: true, // 启动项目后，自动打开浏览器
    host: "127.0.0.1", // 设置主机
    // port: 5001, // 设置端口
  }
})
