import router from "../router/index";
import TagModel from "../class/TagModel";
// import TreeModel from "../class/TreeModel";
import store from "../store/index";
import jwtDecode from "jwt-decode";
import UserInfo from "../class/UserInfo";

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