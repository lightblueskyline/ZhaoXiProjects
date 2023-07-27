<script setup lang="ts">
import { reactive, ref, computed } from "vue"

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

// ---------- 响应式的基础 ----------
const raw = { count: 99 }; // 定义常量
const state1 = reactive(raw); // 包装为代理对象
// debugger // 运行至此处会自动中断至此处
console.log(raw)
console.log(state1)
console.log(raw === state1); // 原始对象和代理对象不是完全相等的

// ---------- ref() ----------
const number = ref(100);
console.log(number);
console.log(number.value);

// ---------- 解构 (ES6) ----------
const obj = {
  foo: ref(1),
  bar: ref(2)
};
const { foo, bar } = obj
console.log(foo, bar);

// ---------- 数组和集合类型的 ref 解包 ----------
const book = ref("资治通鉴");
const books = reactive([book]);
// 这里需要 .value
console.log(books[0].value);

// ---------- 计算属性 ----------
const author = reactive({
  name: 'John Doe',
  books: ['Vue-1', 'Vue-2', 'Vue-3']
});
const publishedBooksMessage = computed(() => {
  return author.books.length > 0 ? 'Yes' : 'No';
});
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
    <a v-on:click="DoSomething">点我做某事</a>
    <br>
    <a @click="DoSomething">...简写形式，点我做某事...</a>
  </div>
  <!-- 解包 -->
  <div>{{ number }}</div>
  <!-- 计算属性 -->
  <div>
    <p>Has published books</p>
    <!-- 表达式写法，逻辑复杂时不太优雅 -->
    <span>{{ author.books.length > 0 ? 'Yes' : 'No' }}</span>
    <!-- 通过计算属性 -->
    <br>
    <span>{{ `通过计算属性 ${publishedBooksMessage}` }}</span>
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
