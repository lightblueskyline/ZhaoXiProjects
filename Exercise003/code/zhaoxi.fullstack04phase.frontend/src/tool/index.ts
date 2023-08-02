// 公共方法
import TagModel from "../class/TagModel";
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