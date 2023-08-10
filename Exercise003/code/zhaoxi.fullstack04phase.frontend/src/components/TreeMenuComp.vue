<!-- 封装树状菜单 -->
<template>
    <!-- <el-menu-item :index="item.Index" v-if="item.Children.length == 0">
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
    </el-sub-menu> -->
    <template v-for="item in list" :key="item.index">
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
// import { PropType } from "vue";
import { onMounted } from 'vue';
import TreeMenuModel from '../class/TreeMenuModel'; // 导入模型
import IconComp from "./IconComp.vue";

// --- 变量 ---
// 此处定义 listTreeMenuModel 用以在使用组件处传入数据
// defineProps({
//     listTreeMenuModel: Array<TreeMenuModel>,
// });
const props = defineProps({
    // objTreeMenuModel: Object as PropType<TreeMenuModel>
    list: Array<TreeMenuModel>
});
// 处理和修饰变量
// const item: TreeMenuModel = props.objTreeMenuModel as TreeMenuModel;
onMounted(() => {
    console.log(props.list);
});
</script>

<style scoped lang="scss"></style>