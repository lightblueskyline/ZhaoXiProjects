<template>
    <el-row>
        <el-col :span="1">
            <el-link :underline="false" @click="SwitchMenuExpand">
                <IconComp iconname="Expand"></IconComp>
            </el-link>
        </el-col>
        <el-col :span="11">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item>
                    <a href="/">
                        <el-icon class="middle">
                            <HomeFilled />
                        </el-icon>
                        <span class="middle">首页</span>
                    </a>
                </el-breadcrumb-item>
                <el-breadcrumb-item>
                    <span class="middle">{{ router.currentRoute.value.name }}</span>
                </el-breadcrumb-item>
            </el-breadcrumb>
        </el-col>
        <el-col :span="12">
            <div class="dropdown">
                <el-avatar :size="30" :src="faceUrl"></el-avatar>
                <el-dropdown>
                    <span>
                        {{ nickName }}
                        <el-icon>
                            <arrow-down />
                        </el-icon>
                    </span>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item>我的主页</el-dropdown-item>
                            <el-dropdown-item @click="logout">退出</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-divider />
            <div>
                <el-tag v-for="item in tags" :key="item.Index" closable class="ml-2"
                    :effect="item.Checked ? 'dark' : 'plain'" @close="handleClose(item.Index)"
                    @click="tagClick(item.Index)">{{ item.Name }}</el-tag>
            </div>
        </el-col>
    </el-row>
</template>

<script setup lang="ts">
import IconComp from "./IconComp.vue"; // 导入自定义组件
import router from "../router/index";
import { ref, onMounted } from "vue";
import userStore from "../store/index"; // 导入 pinia 全局状态管理
import { handleSelect, tagClick, FormatToken } from "../tool/index";
// --- 变量 ---
const faceUrl = ref(FormatToken(userStore().token)?.Image);
const nickName = ref(FormatToken(userStore().token)?.NickName);
// 切换展开折叠菜单
const SwitchMenuExpand = () => {
    userStore().$patch({
        isCollapse: !userStore().isCollapse
    });
};
// Tag
const tags = ref(userStore().tags); // 从全局状态管理读取 Tags
const handleClose = (index: string) => {
    // 排除点击的项目
    tags.value = tags.value.filter(x => x.Index != index);
    userStore().$patch({
        tags: tags.value
    });
};

// --- 方法 ---
// 页面加载完成后从路由匹配当前路径渲染到 Tag
onMounted(() => {
    // 读取路由
    let index = router.currentRoute.value.path;
    if (!tags.value.find(x => x.Checked)) {
        // 判断此 Tag 是否被选中
        // 设置 Tag
        handleSelect(index);
        // 点击 Tag
        tagClick(index);
    } else {
        // 点击 Tag
        tagClick(index);
    }
});
/**
 * 登出
 */
const logout = () => {
    userStore().$reset();
    router.push({ path: "/login" });
};
</script>

<style scoped lang="scss">
.el-header {
    .el-col {
        height: 50px;
        line-height: 50px;

        .el-breadcrumb {
            line-height: inherit;
        }

        .el-icon {
            margin-right: 5px;
        }

        .el-divider {
            margin: 0;
        }
    }
}

.dropdown {
    float: right;
    height: 50px;
    // line-height: 70px;
}

.el-dropdown {
    margin-top: 15px;
    margin-left: 5px;
}

.ml-2 {
    // 彼此之间间隔一二
    margin-left: 10px;
    // 设置鼠标手样式
    cursor: pointer;
}
</style>
