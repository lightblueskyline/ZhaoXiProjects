<!-- 封装树状菜单 -->
<script setup lang="ts">
import { PropType } from "vue";
import TreeMenuModel from '../class/TreeMenuModel'; // 导入模型

// 此处定义 listTreeMenuModel 用以在使用组件处传入数据
// defineProps({
//     listTreeMenuModel: Array<TreeMenuModel>,
// });
const props = defineProps({
    objTreeMenuModel: Object as PropType<TreeMenuModel>
});
// 处理和修饰变量
const item: TreeMenuModel = props.objTreeMenuModel as TreeMenuModel;
</script>

<template>
    <el-menu-item :index="item.Index" v-if="item.Children.length == 0">
        <el-icon>
            <Notebook />
        </el-icon>
        <template #title>
            <span>{{ item.Name }}</span>
        </template>
    </el-menu-item>
    <el-sub-menu :index="item.Index" v-else>
        <template #title>
            <el-icon>
                <Folder />
            </el-icon>
            <span>{{ item.Name }}</span>
        </template>
        <TreeMenuComp v-for="subItem in item.Children" :objTreeMenuModel="subItem" :key="subItem.Index"></TreeMenuComp>
    </el-sub-menu>
</template>

<style scoped lang="scss"></style>