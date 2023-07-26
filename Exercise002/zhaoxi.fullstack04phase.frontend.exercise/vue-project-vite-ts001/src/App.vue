<script setup lang="ts">
import { reactive } from "vue"

const state = reactive({
  message: "Hello World!",
  tempHtml: "<p style='color:red;'>原始 HTML</p>",
  dynamicID: "dynamicID666",
  // dynamicID: undefined,
  isButtonDisabled: true,
  multiAttr: {
    id: "container",
    class: "wrapper",
  },
  seen: false,
});

function TempFunc() {
  return "这是一个方法 TempFunc";
}

// 定义一个方法，建议此种方法定义方式
const TempFunc1 = () => {
  return "这是一个方法 TempFunc1";
};

const DoSomething = () => {
  console.log("DoSomething");
};
</script>

<template>
  <p>M_0v0_M</p>
  <!-- 文本插值 -->
  <div>
    Message:{{ state.message }}
  </div>
  <!-- HTML 渲染 -->
  <div>
    <div>{{ state.tempHtml }}</div>
    <div v-html="state.tempHtml"></div>
  </div>
  <!-- Attribute 绑定 -->
  <div v-bind:id="state.dynamicID">定义一个响应式的 ID</div>
  <!-- Attribute 绑定，简写形式 -->
  <div :id="state.dynamicID">定义一个响应式的 ID</div>
  <!-- 布尔型 Attribute -->
  <div>
    <button :disabled="state.isButtonDisabled">按钮</button>
  </div>
  <!-- 动态绑定多个值 -->
  <div v-bind="state.multiAttr"></div>
  <!-- 使用 JavaScript 表达式 -->
  <div>{{ state.message + " JavaScript 表达式" }}</div>
  <!-- 调用函数 -->
  <div>{{ state.message }}</div>
  <div>{{ TempFunc() }}</div>
  <div>{{ TempFunc1() }}</div>
  <!-- 指令 Directive -->
  <div>
    <p v-if="state.seen">Now you see me</p>
  </div>
  <!-- 参数 Argument -->
  <div>
    <a v-on:click="DoSomething">......</a>
    <br>
    <a @click="DoSomething">...简写形式...</a>
  </div>
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}

.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}

.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
