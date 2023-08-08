// pinia 全局状态管理
import { defineStore } from "pinia";
import TagModel from "../class/TagModel";
import TreeMenuModel from "../class/TreeMenuModel";

const userStore = defineStore("storeUser", {
    state: () => {
        return {
            isCollapse: false, // 菜单默认打开
            tags: [] as TagModel[],
            token: "",
            UserMenus: [] as TreeMenuModel[]
        }
    },
    persist: {
        // 状态管理 & 持久化
        enabled: true, // 开启
        strategies: [
            {
                key: "ZhaoXi.Exercise003", // 指定 Key 此名称会在浏览器本地存储中生成对应的 Name
                storage: localStorage, // 自定义存储方式，默认是 sessionStorage
                // paths:["UserMenus"] // 要缓存的对象，默认是所有
            }
        ]
    }
});

// 导出
export default userStore;