// 全局状态管理
import { defineStore } from "pinia"
import TagModel from '../class/TagModel'
import TreeModel from '../class/TreeModel'

const userStore = defineStore("main", {
    state: () => {
        return {
            Token: "",
            IsCollapse: false,
            Tags: [] as TagModel[],
            // 所有这些属性都将自动推断其类型
            UserMenus: [] as TreeModel[],
            // Token 刷新次数
            RefreshTokenCount: 0,
        }
    },
    // 状态管理
    persist: {
        enabled: true,
        strategies: [{
            key: "site", // 指定 key 这个名称会在浏览器本地存储中生成对应的 name
            storage: localStorage, // 自定义存储方式，默认是 sessionStorage
            // paths: ["UserMenus"] // 要缓存的对象，默认是所有
        }]
    }
});

// 导出
export default userStore;