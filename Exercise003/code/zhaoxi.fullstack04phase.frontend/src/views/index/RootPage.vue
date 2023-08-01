<script setup lang="ts">
import TreeMenu from "../../components/TreeMenu.vue"; // 导入自定义组件
import TreeMenuModel from "../../class/TreeMenuModel"; // 导入模型
import { ref } from "vue";

const listTreeMenuModel: Array<TreeMenuModel> = [
    {
        Index: "/menu",
        Name: "菜单管理",
        Children: [
            {
                Index: "/submenu1",
                Name: "菜单管理 1-1",
                Children: []
            },
            {
                Index: "/submenu2",
                Name: "菜单管理 1-2",
                Children: []
            },
            {
                Index: "/submenu3",
                Name: "菜单管理 1-3",
                Children: [
                    {
                        Index: "/submenu31",
                        Name: "菜单管理 1-3-1",
                        Children: []
                    },
                    {
                        Index: "/submenu32",
                        Name: "菜单管理 1-3-2",
                        Children: []
                    },
                    {
                        Index: "/submenu33",
                        Name: "菜单管理 1-3-3",
                        Children: [
                            {
                                Index: "/submenu331",
                                Name: "菜单管理 1-3-3-1",
                                Children: []
                            },
                            {
                                Index: "/submenu332",
                                Name: "菜单管理 1-3-3-2",
                                Children: []
                            },
                            {
                                Index: "/submenu333",
                                Name: "菜单管理 1-3-3-3",
                                Children: []
                            }
                        ]
                    }
                ]
            },
        ]
    },
    {
        Index: "/role",
        Name: "角色管理",
        Children: []
    },
    {
        Index: "/user",
        Name: "用户管理",
        Children: []
    }
];
//
const isCollapse = ref(false);
</script>

<template>
    <!-- 主页：需要内嵌的页面，放在其中 -->
    <el-container>
        <el-aside style="width: inherit;">
            <el-menu :collapse="isCollapse" router style="height: 100vh;" active-text-color="#ffd04b"
                background-color="#545c64" text-color="#fff">
                <!-- 自定义组件 TreeMenu -->
                <!-- <TreeMenu :listTreeMenuModel="listTreeMenuModel"></TreeMenu> -->
                <TreeMenu v-for="item in listTreeMenuModel" :objTreeMenuModel="item" :key="item.Index"></TreeMenu>
            </el-menu>
        </el-aside>
        <el-container>
            <el-header>
                <!-- 折叠菜单 -->
                <el-radio-group v-model="isCollapse" style="margin-bottom: 20px">
                    <el-radio-button :label="false">expand</el-radio-button>
                    <el-radio-button :label="true">collapse</el-radio-button>
                </el-radio-group>
            </el-header>
            <el-main>
                <router-view></router-view>
            </el-main>
        </el-container>
    </el-container>
</template>