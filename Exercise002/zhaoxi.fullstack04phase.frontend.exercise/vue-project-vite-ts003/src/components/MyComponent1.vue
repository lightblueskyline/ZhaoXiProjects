<script setup lang="ts">
import { ref } from "vue";

defineProps({
    // 1. 类型声明
    // 2. 数据类型
    // 3. 大写字母开头
    title: String
});

const count = ref(0);
</script>

<template>
    <h3>{{ title }}</h3>
    <button @click="$event => count++">You click me {{ count }} times.</button>
</template>

<style scoped></style>