<!-- 使用双单词定义组件文件名称 -->
<script setup lang="ts">
import { ref } from "vue";

const count = ref(0);
</script>

<template>
    <button @click="$event => count++">You click me {{ count }} times.</button>
</template>

<style scoped></style>