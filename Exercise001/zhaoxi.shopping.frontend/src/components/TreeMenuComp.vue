<!-- 菜单组件 -->
<template>
    <template v-for="item in list" :key="item.Index">
        <el-menu-item :index="item.Index" v-if="!item.Children">
            <IconComp :icon="item.Icon"></IconComp>
            <template #title><span>{{ item.Name }}</span></template>
        </el-menu-item>
        <el-sub-menu :index="item.Index" v-else>
            <template #title>
                <IconComp :icon="item.Icon"></IconComp>
                <span>{{ item.Name }}</span>
            </template>
            <!-- 递归调用组件 -->
            <TreeMenuComp :list="item.Children"></TreeMenuComp>
        </el-sub-menu>
    </template>
</template>

<script setup lang="ts">
import TreeModel from '../class/TreeModel';
import IconComp from './IconComp.vue';

defineProps({
    list: Array<TreeModel>
});
</script>