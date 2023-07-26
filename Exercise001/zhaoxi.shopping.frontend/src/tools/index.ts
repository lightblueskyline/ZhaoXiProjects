// 公共方法
import router from "../router/index";
import TagModel from "../class/TagModel";
import TreeModel from "../class/TreeModel";
import store from "../store/index";
import jwtDecode from "jwt-decode";
import UserInfo from "../class/UserInfo";
import { GetMenus } from "../http/index";

// 入参为路由配置
export const handleSelect = (index: string) => {
    if (index == "/") return;
    let name = router.getRoutes().filter(item => item.path == index)[0].name as string;
    let tagModel: TagModel = {
        Name: name,
        Index: index,
        Checked: false
    };
    let tags: Array<TagModel> = store().Tags; // 全局状态
    if (tags.find(x => x.Index == index) == undefined) {
        tags.push(tagModel);
        // 更新全局状态
        store().$patch({
            Tags: tags
        });
    }
    clickTag(index);
};

// 入参为路由配置
// 点击 Tag 设置 Checked 更新 Store 跳转路由
export const clickTag = (index: string) => {
    if (index == "/") return;
    let currentTags = store().Tags;
    currentTags.forEach(x => {
        if (x.Index == index) {
            x.Checked = true;
        } else {
            x.Checked = false;
        }
    });
    // 更新全局状态
    store().$patch({
        Tags: currentTags
    });
};

// 解析 Token
export const FormatToken = (token: string) => {
    if (token != undefined) {
        return jwtDecode(token) as UserInfo;
    }
    return null;
};

// 
export const Valid = (param: number): boolean => {
    if (param) {
        if (FormatDate(param) >= GetDate()) {
            return true;
        }
    }
    return false;
};

// 格式化时间
export const FormatDate = (param: number) => {
    // PS: 注意这个地方，要乘以 1000
    const dateTime = new Date(param * 1000);
    const year = dateTime.getFullYear();
    const month = (dateTime.getMonth() + 1 + "").padStart(2, "0");
    const day = (dateTime.getDate() + "").padStart(2, "0");
    const hour = (dateTime.getHours() + "").padStart(2, "0");
    const minute = (dateTime.getMinutes() + "").padStart(2, "0");
    const second = (dateTime.getSeconds() + "").padStart(2, "0");
    return `${year}-${month}-${day} ${hour}:${minute}:${second}`;
};

// 获取当前时间
export const GetDate = () => {
    const dateTime = new Date();
    const year = dateTime.getFullYear();
    const month = (dateTime.getMonth() + 1 + "").padStart(2, "0");
    const day = (dateTime.getDate() + "").padStart(2, "0");
    const hour = (dateTime.getHours() + "").padStart(2, "0");
    const minute = (dateTime.getMinutes() + "").padStart(2, "0");
    const second = (dateTime.getSeconds() + "").padStart(2, "0");
    return `${year}-${month}-${day} ${hour}:${minute}:${second}`;
};

// 设置用户动态路由，更新全局状态
export const SettingUserRouter = async () => {
    // 读取所有节点下的文件
    const temp = import.meta.glob(["../views/*.vue", "../views/*/*.vue", "../views/*/*/*.vue"]);
    let tempArr: any[] = [];
    for (var item in temp) {
        tempArr.push({ filepath: item, component: temp[item] });
    }
    const temp1 = {
        Name: "",
        Index: "",
        FilePath: "",
        Description: ""
    };
    // 递归菜单路由
    const treeMenu: Array<TreeModel> = (await GetMenus(temp1)) as any as Array<TreeModel>;
    // 递归路由，将 List 转换为 Tree
    const menuList: Array<TreeModel> = RecursiveRoutes(treeMenu);
    menuList.forEach(item => {
        if (tempArr.find(x => x.filepath.indexOf(item.FilePath)) > -1) {
            // 添加动态路由
            router.addRoute("admin", {
                name: item.Name,
                path: item.Index,
                component: tempArr.find(y => y.filepath.indexOf(item.FilePath) > -1).component
            });
        }
    });
    // 更新全局状态
    store().$patch({
        UserMenus: treeMenu
    });
};

// 递归菜单路由，输出列表
export const RecursiveRoutes = (treeMenu: Array<TreeModel>) => {
    let list: Array<TreeModel> = [];
    for (let index = 0; index < treeMenu.length; index++) {
        let node = treeMenu[index];
        if (node.Children) {
            let childrenList = RecursiveRoutes(node.Children);
            list = list.concat(childrenList);
        }
        if (node.FilePath == "") {
            continue;
        }
        list.push({
            ID: node.ID,
            Index: node.Index,
            Name: node.Name,
            Icon: node.Icon,
            FilePath: node.FilePath,
            Children: node.Children
        });
    }
    return list;
};