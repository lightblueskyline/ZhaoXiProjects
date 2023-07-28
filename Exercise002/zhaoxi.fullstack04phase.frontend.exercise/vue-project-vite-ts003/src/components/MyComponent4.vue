<script setup lang="ts">
defineProps({
    // 1. 类型声明
    // 2. 数据类型
    // 3. 大写字母开头
    title: String
});

const emits = defineEmits(['parentFunc2', 'parentFunc3']);
const Handler = () => {
    emits("parentFunc2");
    emits("parentFunc3");
}
</script>

<template>
    <h3>{{ title }}</h3>
    <!-- parentFunc 同父组件中定义的方法名称 -->
    <!-- <button @click="$event => $emit('parentFunc1', '参数来自组件 MyComponent3')">子组件调用父组件中的方法传递参数</button> -->
    <button @click="Handler">通过声明触发的事件</button>
</template>

<style scoped></style> 