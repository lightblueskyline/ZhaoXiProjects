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
                        <el-menu-item index="/">
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

<script setup lang="ts">
import TreeMenuComp from "../../components/TreeMenuComp.vue"; // 导入自定义组件
import HeaderComp from "../../components/HeaderComp.vue"; // 导入自定义组件
import userStore from "../../store/index"; // 导入 pinia 全局状态管理
import { handleSelect } from "../../tool/index";
import router from "../../router/index"; // 导入路由
import IconComp from "../../components/IconComp.vue";

const _UserStore = userStore();
const listTreeMenuModel = _UserStore.UserMenus;
const isCollapse = _UserStore.isCollapse;
</script>

<style scoped lang="scss"></style>