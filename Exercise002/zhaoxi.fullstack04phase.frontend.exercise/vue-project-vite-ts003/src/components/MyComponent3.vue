<script setup lang="ts">
defineProps({
    // 1. 类型声明
    // 2. 数据类型
    // 3. 大写字母开头
    title: String
});

const emits = defineEmits(["parentFunc1"]);
const TriggerEmits = () => {
    emits("parentFunc1");
};
</script>

<template>
    <h3>{{ title }}</h3>
    <!-- parentFunc 同父组件中定义的方法名称 -->
    <!-- <button @click="$event => $emit('parentFunc1', '参数来自组件 MyComponent3')">子组件调用父组件中的方法传递参数</button> -->
    <button @click="TriggerEmits">子组件调用父组件中的方法传递参数</button>
</template>

<style scoped></style>