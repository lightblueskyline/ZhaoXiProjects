<template>
    <div class="common-layout">
        <el-container>
            <el-aside style="width: inherit;">
                <el-menu active-text-color="#ffd04b" background-color="#545c64" class="el-menu-vertical-demo" router
                    :collapse="isCollapse" :unique-opened="true" default-active="2" text-color="#fff">
                    <!-- 静态菜单 -->
                    <el-sub-menu index="/desktop">
                        <template #title>
                            <IconComp icon="House"></IconComp>
                            <span>我的主页</span>
                        </template>
                        <el-menu-item index="/desktop">
                            <IconComp icon="Monitor"></IconComp>
                            <span>工作台</span>
                        </el-menu-item>
                        <el-menu-item index="/person">
                            <IconComp icon="User"></IconComp>
                            <span>个人信息</span>
                        </el-menu-item>
                    </el-sub-menu>
                    <!-- <el-menu-item index="/menu">
                        <IconComp icon="Menu"></IconComp>
                        <span>菜单列表</span>
                    </el-menu-item>
                    <el-menu-item index="/role">
                        <IconComp icon="Grid"></IconComp>
                        <span>角色列表</span>
                    </el-menu-item>
                    <el-menu-item index="/user">
                        <IconComp icon="UserFilled"></IconComp>
                        <span>用户列表</span>
                    </el-menu-item> -->
                    <!-- 动态菜单 -->
                    <TreeMenuComp :list="treeList"></TreeMenuComp>
                </el-menu>
            </el-aside>
            <el-container>
                <el-header style="height: initial;">
                    <HeaderComp></HeaderComp>
                </el-header>
                <el-main><router-view></router-view></el-main>
            </el-container>
        </el-container>
    </div>
</template>
<script setup lang="ts">
import { computed } from "vue";
import userStore from "../../store/index";
import HeaderComp from "../../components/HeaderComp.vue";
import IconComp from "../../components/IconComp.vue";
import TreeMenuComp from "../../components/TreeMenuComp.vue"; // 导入菜单组件

const treeList = computed(() => userStore().UserMenus);
const isCollapse = computed(() => userStore().IsCollapse);
</script>