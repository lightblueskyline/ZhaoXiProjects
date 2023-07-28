<!-- 组件监听事件 -->
<script setup lang="ts">
defineProps({
    // 1. 类型声明
    // 2. 数据类型
    // 3. 大写字母开头
    title: String
});

const emits = defineEmits(["parentFunc"]);
const TriggerEmits = () => {
    emits("parentFunc");
};
</script>

<template>
    <h3>{{ title }}</h3>
    <!-- parentFunc 同父组件中定义的方法名称 -->
    <!-- <button @click="$event => $emit('parentFunc')">子组件调用父组件中的方法并修改变量</button> -->
    <button @click="TriggerEmits">子组件调用父组件中的方法并修改变量</button>
</template>

<style scoped></style>