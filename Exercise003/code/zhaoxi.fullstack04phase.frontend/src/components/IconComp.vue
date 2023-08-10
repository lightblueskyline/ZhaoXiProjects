<!-- 图标组件 -->
<template>
    <!-- <el-button :icon="Expand"></el-button> -->
    <!-- <el-button :icon="iconname" circle></el-button> -->
    <component :is="iconname!" class="icon"></component>
</template>

<script setup lang="ts">
// import { Expand } from "@element-plus/icons-vue";
defineProps({
    iconname: String
});
</script>

<style scoped>
/* 参照 Element-Plus 官方效果编写图标样式 */
.icon {
    height: 1em;
    width: 1em;
    vertical-align: middle;
    margin-right: 5px;
    text-align: center;
    font-size: 18px;
}
</style>