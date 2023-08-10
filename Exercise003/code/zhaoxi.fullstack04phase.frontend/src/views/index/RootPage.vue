<script setup lang="ts">
import TreeMenuComp from "../../components/TreeMenuComp.vue"; // 导入自定义组件
import HeaderComp from "../../components/HeaderComp.vue"; // 导入自定义组件
import TreeMenuModel from "../../class/TreeMenuModel"; // 导入模型
import { computed } from "vue";
import userStore from "../../store/index"; // 导入 pinia 全局状态管理
import { handleSelect } from "../../tool/index";
import router from "../../router/index"; // 导入路由
import IconComp from "../../components/IconComp.vue";

const listTreeMenuModel: Array<TreeMenuModel> = [
    {
        "Name": "菜单管理",
        "Index": "/menu",
        "FilePath": "",
        "Children": [
            {
                "Name": "菜单列表",
                "Index": "/menu",
                "Children": [],
                "FilePath": "menu.vue"
            }
        ]
    },
    {
        "Name": "角色管理",
        "Index": "/role",
        "FilePath": "",
        "Children": [
            {
                "Name": "角色列表",
                "Index": "/role",
                "Children": [],
                "FilePath": "role.vue"
            }
        ]
    },
    {
        "Name": "用户管理",
        "Index": "/user",
        "FilePath": "",
        "Children": [
            {
                "Name": "用户列表",
                "Index": "/user",
                "Children": [],
                "FilePath": "user.vue"
            }
        ]
    }
];
//
const isCollapse = computed(() => {
    userStore().isCollapse
}); // 计算属性
</script>

<template>
    <!-- 主页：需要内嵌的页面，放在其中 -->
    <div class="common-layout">
        <el-container>
            <el-aside style="width: inherit;">
                <el-menu :collapse="isCollapse" router style="height: 100vh;" active-text-color="#ffd04b"
                    background-color="#545c64" text-color="#fff" @select="handleSelect"
                    :default-active="router.currentRoute.value.path" :unique-opened="true">
                    <el-sub-menu index="/desktop">
                        <template #title>
                            <IconComp iconname="House"></IconComp>
                            <span>主页</span>
                        </template>
                        <el-menu-item index="/desktop">
                            <IconComp iconname="Monitor"></IconComp>
                            <span>工作台</span>
                        </el-menu-item>
                        <el-menu-item index="/personpage">
                            <el-icon>
                                <ElementPlus />
                            </el-icon>
                            <span>个人中心</span>
                        </el-menu-item>
                    </el-sub-menu>
                    <!-- <TreeMenuComp v-for="item in listTreeMenuModel" :objTreeMenuModel="item" :key="item.Index">
                    </TreeMenuComp> -->
                    <TreeMenuComp :list="listTreeMenuModel"></TreeMenuComp>
                </el-menu>
            </el-aside>
            <el-container>
                <el-header>
                    <HeaderComp></HeaderComp>
                </el-header>
                <el-main>
                    <router-view></router-view>
                </el-main>
            </el-container>
        </el-container>
    </div>
</template>