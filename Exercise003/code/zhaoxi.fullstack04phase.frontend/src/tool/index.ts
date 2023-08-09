// 公共方法
import TagModel from "../class/TagModel";
import TreeMenuModel from "../class/TreeMenuModel";
import UserInfo from "../class/UserInfo";
import { GetMenus } from "../http/index";
import router from "../router/index";
import userStore from "../store/index";
import jwtDecode from "jwt-decode";

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
    //     router.addRoute("RootPage", {
    //         name: "",
    //         path: "",
    //         component: x.component
    //     });
    // });
    const obj = {
        Name: "",
        Index: "",
        FilePath: "",
        ParentId: "",
        Description: ""
    };
    // 对接权限菜单数据
    const treeMenu: Array<TreeMenuModel> = (await GetMenus(obj)) as any as Array<TreeMenuModel>;
    // 递归路由，将 Tree 转换为 List
    const treeList: Array<TreeMenuModel> = RecursiveRoutes(treeMenu);
    // 添加动态路由
    treeList.forEach(x => {
        router.addRoute("RootPage", {
            name: x.Name,
            path: x.Index,
            component: localFiles.find(y => y.filepath.indexOf(x.FilePath) > -1).component
        });
    });
    // 更新全局状态
    userStore().$patch({
        UserMenus: treeList
    });
}

// 递归路由，输出 List
export const RecursiveRoutes = (tree: Array<TreeMenuModel>) => {
    let list: Array<TreeMenuModel> = [];
    for (let index = 0; index < tree.length; index++) {
        let node = tree[index];
        if (node.Children) {
            let childrenList = RecursiveRoutes(node.Children);
            list = list.concat(childrenList);
        }
        if (node.FilePath == "") {
            continue;
        }
        list.push({
            Id: node.Id,
            Index: node.Index,
            Name: node.Name,
            FilePath: node.FilePath,
            Children: node.Children
        });
    }
    return list;
}

/**
 * 格式化 Token
 * @param token 
 * @returns 
 */
export const FormatToken = (token: string) => {
    if (token) {
        return jwtDecode(token) as UserInfo;
    }
    return null;
}

/**
 * 格式化时间
 * @param param 
 * @returns 
 */
export const FormatDate = (param: number): string => {
    // PS: 注意此处需要 * 1000
    const date = new Date(param * 1000);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1 + "").padStart(2, "0");
    const day = (date.getDay() + "").padStart(2, "0");
    const hour = (date.getHours() + "").padStart(2, "0");
    const minute = (date.getMinutes() + "").padStart(2, "0");
    const second = (date.getSeconds() + "").padStart(2, "0")
    return `${year}-${month}-${day} ${hour}:${minute}:${second}`;
}

/**
 * 获取当前时间
 * @returns 
 */
export const GetDate = (): string => {
    const date = new Date();
    const year = date.getFullYear();
    const month = (date.getMonth() + 1 + "").padStart(2, "0");
    const day = (date.getDay() + "").padStart(2, "0");
    const hour = (date.getHours() + "").padStart(2, "0");
    const minute = (date.getMinutes() + "").padStart(2, "0");
    const second = (date.getSeconds() + "").padStart(2, "0")
    return `${year}-${month}-${day} ${hour}:${minute}:${second}`;
}

/**
 * 验证 Token 有效期
 * @param param 
 * @returns 
 */
export const ValidTokenExpire = (param: number): boolean => {
    // 验证参数的有效性
    if (param) {
        // 若 Token 的有效期大于当前时间
        if (FormatDate(param) >= GetDate()) {
            return true;
        }
    }
    return false;
}