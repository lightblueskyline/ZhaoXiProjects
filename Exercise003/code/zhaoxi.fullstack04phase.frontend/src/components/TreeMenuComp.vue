<!-- 封装树状菜单 -->
<template>
    <template v-for="item in list " :key="item.index">
        <el-menu-item :index="item.Index" v-if="!item.Children">
            <IconComp iconname="Notebook"></IconComp>
            <template #title>
                <span>{{ item.Name }}</span>
            </template>
        </el-menu-item>
        <el-sub-menu :index="item.Index" v-else>
            <template #title>
                <IconComp iconname="Folder"></IconComp>
                <span>{{ item.Name }}</span>
            </template>
            <TreeMenuComp :list="item.Children"></TreeMenuComp>
        </el-sub-menu>
    </template>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import TreeMenuModel from '../class/TreeMenuModel'; // 导入模型
import IconComp from "./IconComp.vue";

const props = defineProps({
    list: Array<TreeMenuModel>
});

onMounted(() => {
    console.log(props.list);
});
</script>

<style scoped lang="scss"></style>