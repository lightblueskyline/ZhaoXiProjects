<script setup lang="ts">
import { ref, onMounted } from "vue";
import router from "../router";
import store from "../store/index";
import userStore from "../store/index";
import { Expand, House } from "@element-plus/icons-vue";
import { handleSelect, clickTag } from "../tools/index";

const circleUrl = ref("");
const nickName = ref("");
const tags = ref(userStore().Tags); // 从全局状态中读取 Tags
const changeIsCollapse = () => {
    // 修改值
    store().$patch({
        IsCollapse: !store().IsCollapse
    });
    console.log("ChangeIsCollapse");
};
const logOut = () => {
    console.log("logOut");
};
const handleClose = (index: string) => {
    tags.value = tags.value.filter(x => x.Index != index);
    store().$patch({
        Tags: tags.value
    });
    console.log(index);
};
const tagClick = (index: string) => {
    console.log(index);
};
// 页面加载时读取状态
onMounted(() => {
    // 读取当前路由
    let index = router.currentRoute.value.path;
    if (!tags.value.find(x => x.Checked)) {
        handleSelect(index); // 设置 Tags
        clickTag(index) // 点击 Tags
    } else {
        clickTag(index) // 点击 Tags
    }
});
</script>

<template>
    <el-row>
        <el-col :span="1">
            <el-link :underline="false" @click="changeIsCollapse">
                <el-icon>
                    <Expand />
                </el-icon>
            </el-link>
        </el-col>
        <el-col :span="11">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item>
                    <a href="/">
                        <el-icon class="middle">
                            <House />
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
                <el-avatar :size="30" :src="circleUrl"></el-avatar>
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
                            <el-dropdown-item @click="logOut">退出</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-divider></el-divider>
            <div>
                <el-tag v-for="item in tags" :key="item.Index" closable class="ml-2"
                    :effect="item.Checked ? 'dark' : 'plain'" @close="handleClose(item.Index)"
                    @click="tagClick(item.Index)">{{ item.Name }}</el-tag>
            </div>
        </el-col>
    </el-row>
</template>

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
    line-height: 70px;
}

.el-dropdown {
    margin-top: 15px;
    margin-left: 5px;
}

.ml-2 {
    margin-left: 10px; // 彼此之间间隔
    cursor: pointer; // 设置鼠标样式
}
</style>