// 公共方法
import TagModel from "../class/TagModel";
import TreeMenuModel from "../class/TreeMenuModel";
import { GetToken } from "../http";
import router from "../router/index";
import userStore from "../store/index";

// 选择菜单时添加 Tag
export const handleSelect = (index: string) => {
    if (index == "/") {
        return;
    }
    let name = router.getRoutes().filter(x => x.path == index)[0].name as string;
    let model: TagModel = {
        Name: name,
        Index: index,
        Checked: false,
    }
    let tags: Array<TagModel> = userStore().tags;
    if (tags.find(x => x.Index == index) == undefined) {
        tags.push(model);
        userStore().$patch({
            tags: tags
        });
    }
    tagClick(index);
}

// 点击 Tag 设置选中，更新全局状态，跳转路由
export const tagClick = (index: string) => {
    if (index == "/") {
        return;
    }
    let curr = userStore().tags;
    curr.forEach(x => {
        if (x.Index == index) {
            x.Checked = true;
        } else {
            x.Checked = false;
        }
    });
    userStore().$patch({
        tags: curr
    });
    router.push({
        path: index
    });
}

// 设置用户动态路由，更新全局状态
export const SettingUserDynamicRouter = async () => {
    // 读取所有节点下的文件
    const nodeFiles = import.meta.glob(["../views/*/*.vue", "../views/*/*/*.vue", "../views/*/*/*.vue"]);
    console.log(nodeFiles);
    let localFiles: any[] = [];
    for (var item in nodeFiles) {
        localFiles.push({ filepath: item, component: nodeFiles[item] });
    }
    console.log(localFiles);
    // // 动态路由添加
    // localFiles.forEach(x => {
    //     router.addRoute("admin", {
    //         name: "",
    //         path: "",
    //         component: x.component
    //     });
    // });
    const obj={
        Name:"",
        Index:"",
        FilePath:"",
        ParentId:"",
        Description:""
    };
}