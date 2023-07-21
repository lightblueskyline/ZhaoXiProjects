<template>
    <div class="common-layout">
        <el-container>
            <el-aside style="width: inherit;">
                <el-menu active-text-color="#ffd04b" background-color="#545c64" class="el-menu-vertical-demo"
                    default-active="2" text-color="#fff">
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
                    <TreeMenuComp :list="treeList"></TreeMenuComp>
                </el-menu>
            </el-aside>
            <el-container>
                <el-header>
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
import TreeMenuComp from "../../components/TreeMenuComp.vue";

const treeList = computed(() => userStore().UserMenus);
</script>