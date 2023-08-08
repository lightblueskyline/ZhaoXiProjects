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
