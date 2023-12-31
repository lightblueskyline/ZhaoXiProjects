import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    host: "127.0.0.1", // IP
    // port:3000, // 端口
    open: false // 是否自动打开浏览器 True->Auto Open Browser
  }
})
