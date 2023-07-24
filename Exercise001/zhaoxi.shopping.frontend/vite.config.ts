import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    open: false, // true 启动项目后，是否自动打开浏览器
    host: "127.0.0.1", // 设置主机
    port: 5149, // 设置端口
    proxy: {
      "/api": {
        target: 'http://localhost:5148/api', // 本地代理地址
        changeOrigin: true, // 启用跨域访问
        rewrite: (path) => path.replace(/^\/api/, ''), // 修改请求路径
      }
    }
  }
});
