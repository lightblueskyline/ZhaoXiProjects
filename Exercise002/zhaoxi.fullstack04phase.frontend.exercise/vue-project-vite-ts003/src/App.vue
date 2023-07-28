<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import MyComponent from './components/MyComponent.vue';
import MyComponent1 from './components/MyComponent1.vue';
import MyComponent2 from './components/MyComponent2.vue';
import MyComponent3 from './components/MyComponent3.vue';
import MyComponent4 from './components/MyComponent4.vue';

// ---------- 条件渲染 ----------
const awesome = ref(true);
const ok = ref(true);

// ---------- v-for ----------
const parentMessage = ref("Parent");
const items = ref([{ message: "Foo" }, { message: "Bar" }]);

// ---------- v-for 与对象 ----------
const myObject = ref({
  title: "How to do lists in Vue",
  author: "Jane Doe",
  publishAt: "2016-04-10"
});

// ---------- 监听事件 ----------
const Func1 = () => {
  console.log("Func1()");
};

// ---------- 内联事件处理器 ----------
const count = ref(0);

// ---------- 方法事件处理器 ----------
const name = ref("Vue.js");
function greet(event) {
  alert(`Hello ${name.value}`);
  // event 是 DOM 原生事件
  if (event) {
    alert(event.target.tagName);
  }
}

// ---------- 按键修饰符 ----------
const submit = () => {
  console.log("submit");
}

// ---------- 表单输入绑定 ----------
const text = ref();

// ---------- 注册周期钩子 ----------
onMounted(() => {
  console.log("The component is now mounted.");
});

// ---------- 侦听器 ----------
const searchVal = ref();
watch(searchVal, (newValue, oldValue) => {
  // 参数一： 新值
  // 参数二： 旧值
  console.log(`newValue: ${newValue}`);
  console.log(`oldValue: ${oldValue}`);
});

// ---------- 模板引用 ----------
const templateVal = ref(); // 创建模板引用名称
onMounted(() => {
  console.log("The component is now mounted.");
  // 页面加载完成后，将光标设置至此元素
  templateVal.value.focus();
  console.log("Set focus to input(templateVal)");
});

// ---------- 组件监听事件 ----------
const emitsBoolean = ref(true);
const parentFunc = () => {
  emitsBoolean.value = !emitsBoolean.value;
};

// ---------- 组件监听事件传递参数 ----------
const parentFunc1 = (param: string) => {
  console.log(param);
};

// ---------- 声明事件的触发 ----------
const parentFunc2 = () => {
  console.log("vue-project-vite-ts003\src\components\MyComponent4.vue - parentFunc2");
};
const parentFunc3 = () => {
  console.log("vue-project-vite-ts003\src\components\MyComponent4.vue - parentFunc3");
};
</script>

<template>
  <!-- 条件渲染 -->
  <div>
    <h2>条件渲染</h2>
    <!-- awesom = false h1 将不会显示，显示 else 部分 -->
    <h3 v-if="awesome">Vue is awesome!</h3>
    <h3 v-else>awesome = false</h3>
    <h3 v-show="ok">Hello</h3>
  </div>

  <!-- v-for -->
  <div>
    <h2>v-for</h2>
    <ul>
      <li v-for="item in items">{{ item.message }}</li>
    </ul>
    <h2>v-for 索引</h2>
    <ul>
      <li v-for="(item, index) in items">{{ parentMessage }}-{{ index }}-{{ item.message }}</li>
    </ul>
  </div>

  <!-- v-for 与对象 -->
  <div>
    <h2>v-for 与对象</h2>
    <ul>
      <li v-for="(value, key) in myObject">{{ value }} - {{ key }}</li>
    </ul>
    <ul>
      <li v-for="(value, key, index) in myObject">{{ value }} - {{ key }} - {{ index }}</li>
    </ul>
  </div>

  <!-- 在 v-for 里使用范围值 -->
  <div>
    <h2>在 v-for 里使用范围值</h2>
    <span v-for="n in 10">{{ n }}</span>
  </div>

  <!-- v-if & v-for -->
  <div v-if="ok">
    <span v-for="n in 10">{{ n }}</span>
  </div>

  <!-- 监听事件 -->
  <div>
    <h2>监听事件</h2>
    <button v-on:click="Func1">点击我</button>
  </div>

  <!-- 内联事件处理器 -->
  <div>
    <button @click="count++">Add 1</button>
    <p>Count is {{ count }}</p>
  </div>

  <!-- 方法事件处理器 -->
  <div>
    <button @click="greet">Greet</button>
  </div>

  <!-- 按键修饰符 -->
  <div>
    <input type="text" placeholder="回车登录" @keyup.enter="submit">
  </div>

  <!-- 表单输入绑定 -->
  <div>
    <h2>表单输入绑定</h2>
    <p>{{ text }}</p>
    <p>
      <!-- 通过内联事件实现 -->
      <!-- <input type="text" name="" id="" :value="text" @input="event => text = event.target.value"> -->
      <!-- 通过 v-model 实现 -->
      <input type="text" name="" id="" v-model="text">
    </p>
  </div>

  <!-- 侦听器 -->
  <div>
    <h2>侦听器</h2>
    <!-- 监听改变前、后值 -->
    <p><input type="text" name="" id="" v-model="searchVal"></p>
  </div>

  <!-- 模板引用 -->
  <div>
    <h2>模板引用</h2>
    <p><input type="text" name="" id="" ref="templateVal"></p>
  </div>

  <!-- 组件基础 -->
  <div>
    <h2>组件基础</h2>
    <p>
      <!-- 作为标签来使用 -->
      <MyComponent></MyComponent>
    </p>
  </div>

  <!-- 组件传递 props -->
  <div>
    <h2>组件传递 props</h2>
    <p>
      <MyComponent1 title="标题一"></MyComponent1>
    </p>
    <p>
      <MyComponent1 title="标题二"></MyComponent1>
    </p>
  </div>

  <!-- 组件监听事件 -->
  <div>
    <h2>组件监听事件</h2>
    <p>{{ emitsBoolean }}</p>
    <p>
      <!-- <MyComponent2 title="標題參數" @parentFunc="$event => { emitsBoolean = !emitsBoolean }"></MyComponent2> -->
      <MyComponent2 title="标题参数 MyComponent2" @parentFunc="parentFunc"></MyComponent2>
    </p>
  </div>

  <!-- 组件监听事件传递参数 -->
  <div>
    <h2>组件监听事件传递参数</h2>
    <p>
      <MyComponent3 title="标题参数 MyComponent3" @parentFunc1="parentFunc1"></MyComponent3>
    </p>
  </div>

  <!-- 声明事件的触发 -->
  <div>
    <h2>声明事件的触发</h2>
    <p>
      <MyComponent4 title="标题参数 MyComponent4" @parentFunc2="parentFunc2" @parentFunc3="parentFunc3"></MyComponent4>
    </p>
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
