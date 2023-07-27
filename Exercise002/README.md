# 朝夕教育.全栈班.第04期

## Vue 3.2 学习

### 环境准备

安装 Node.js

```bash
# 查看版本
node --version

# 查看(npm)包管理器版本
npm --version

# 解决 npm 下载慢的问题
# 查看镜像源
npm get registry
# 修改镜像源
npm config set registry http://registry.npm.taobao.org/
```

### 包管理器

1. npm
2. yarn
3. pnpm (推荐。速度快，节省磁盘空间)

**pnpm** 安装命令 `npm install -g pnpm`  
`npm install -> pnpm install`  
`npm i <pkg> -> pnpm add <pkg>`  
`npm run <cmd> -> pnpm <cmd>`

### 创建 Vue 应用

`npm init vue@latest` 之后出现的选项默认全部回车  
`cd <目录名>`  
`npm install` 安装对应包  
`npm run dev` 启动运行  
推荐的 IDE 配置是： Visual Studio Code + Volar 扩展  
插件推荐： (Auto Close Tag) (Auto Rename) (CSS Formatter) (ESLint) (Npm Intellisense) (Path Intellisense)

### 使用 pnpm 创建 Vue 应用

```bash
# 安装 pnpm
npm install -g pnpm
#
pnpm create vite vue-project-vite-ts
#
cd ./vue-project-vite-ts
pnpm install
# 简化版命令
pnpm i
pnpm run dev
pnpm dev
# 生成命令
pnpm build
```

|目录名称|作用|
|----|----|
|.vscode|vscode 配置|
|public|公共资源，该目录不会被打包|
|src|项目目录|
|src\assets|静态资源目录，会被编译和打包|
|src\components|组件|
|src\App.vue|根组件|
|src\main.ts|主函数|
|src\style.css|样式表|
|src\vite-env.d.ts|Vite 声明文件|
|index.html|起始页|
|package.json|项目配置 Node.js 初始化后生成(npm init)|
|tsconfig.json|TS 配置文件(浏览器环境，该配置引用了下面的 tsconfig.node.json)|
|tsconfig.node.json|TS 配置文件(Node 环境)|
|vite.config.ts|Vite 配置文件|

#### package.json

|节点名称|作用|
|----|----|
|scripts|项目启动、编译和预览的脚本，可以自定义命令实现个性化|
|dependencies|生产环境依赖包|
|devDependencies|开发环境依赖包|

```json
{
  "name": "vue-project-vite-ts",
  "private": true,
  "version": "0.0.0",
  "type": "module",
  "scripts": {
    "startApp": "vite", // 修改启动命令 pnpm startApp = pnpm run dev = pnpm dev
    "dev": "vite",
    "build": "vue-tsc && vite build",
    "preview": "vite preview"
  },
  "dependencies": {
    "vue": "^3.3.4"
  },
  "devDependencies": {
    "@vitejs/plugin-vue": "^4.2.3",
    "typescript": "^5.0.2",
    "vite": "^4.4.5",
    "vue-tsc": "^1.8.5"
  }
}
```

#### vite.config.ts

```ts
// vue-project-vite-ts\vite.config.ts
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    host: "127.0.0.1",
    // port:3000,
    open: false // 是否自动打开浏览器 True->Auto Open Browser
  }
})
```

### 模板语法

### 文本插值

最基本的数据绑定形式： 文本插值  
`<span>Message: {{ msg }}</span>`

### 原始 HTML

```html
<!-- 双大括号会将数据解释为纯文本 -->
<p>Using text interpolation: {{ rawHtml }}</p>
<p>Using v-html directive: <span v-html="rawHtml"></span></p>
```

动态渲染 HTML 此种方式容易造成 XSS 漏洞。仅在内容安全可信是再使用 **v-html**

### Attribute 绑定

双大括号不能在 HTML Attribute 中使用。想要响应式的绑定一个 Attribute 应该使用 **v-bind** 指令  
`<div v-bind:id="state.dynamicID">定义一个响应式的 ID</div>`  
**v-bind** 指令指示 Vue 将元素的 ID Attribute 与组件的 dynamicID 属性保持一直。如果绑定的值是 null 或者 undefined 那么该 Attribute 将会从渲染的元素上移除。  
简写形式：  
`<div :id="state.dynamicID">定义一个响应式的 ID</div>`

### 布尔型 Attribute

布尔型 Attribute 依据 true/false 值来决定 Attribute 是否应该存在于该元素上 disabled 就是最常见的例子之一。  
v-bind 在这种场景下的行为略有不同：  
`<button :disabled="isButtonDisabled">按钮</button>`  
当 isButtonDisabled 为真值或一个空字符串(`<button disabled="">`)时，元素会包含这个 disabled Attribute 而当其为其他假值时 Attribute 将被忽略。

### 动态绑定多个值

如果有像这样的一个包含多个 Attribute 的 JavaScript 对象：

```js
const objectOfAttrs = {
    id: 'container',
    class: 'wrapper'
}
```

通过不带参数的 v-bind 你可以将它们绑定到单个元素上：  
`<div v-bind="objectOfAttrs"></div>`

### 使用 JavaScript 表达式

Vue 实际上所有的数据绑定中都支持完整的 JavaScript 表达式：

```js
{{ number + 1 }}
{{ ok ? 'YES':'NO' }}
{{ message.split('').reverse().join('') }}
<div :id="`list-${id}`"></div>
```

这些表达式都会被作为 JavaScript 以当前组件实例为作用域解析执行。  
在 Vue 模板内 JavaScript 表达式可以被使用在如下场景上：  
在文本插值中(双大括号)  
在任何 Vue 指令(以 v- 开头的特殊 Attribute) Attribute 的值中  
**PS:** 每个绑定仅支持单一表达式，也就是一段能够被求值的 JavaScript 代码。一个简单的判断方法： 是否可以合法的写在 **return** 后面。  
因此以下示例是**错误无效**的：

```js
// 这是一个语句，而非表达式
{{ var a = 1 }}

// 条件空值也不支持，请使用三元表达式
{{ if(ok) {return message;} }}
```

### 调用函数

可以在绑定的表达式中使用一个组件暴露的方法：

```html
<span :title="toTitleDate(date)">
    {{ formatDate(date) }}
</span>
```

### 指令 Directive

指令是带有 v- 前缀的特殊 Attribute Vue 提供了许多内置指令，包括上面所介绍的 v-bind & v-html  
指令 Attribute 的期望值为一个 JavaScript 表达式(除了少数几个例外，即之后要讨论的 v-for v-on v-slot) 一个指令的任务是在其表达式的值变化时响应式的更新 DOM 以 v-if 为例：  
`<p v-if="seen">Now you see me</p>`  
这里 v-if 指令会基于表达式 seen 的值的真假来移除或插入该元素。

### 参数 Argument

某些指令会需要一个“参数”，在指令名后通过一个冒号隔开做标识。例如用 v-bind 指令来响应式的更新一个 HTML Attribute

```html
<a v-bind:href="url">...</a>
<!-- 简写形式 -->
<a :href="url">...</a>
```

这里 href 就是一个参数，它告诉 v-bind 指令将表达式 url 的值绑定到元素的 href Attribute 上。  
另一个例子是 v-on 指令，它将监听 DOM 事件：

```html
<a v-on:click="doSomething">...</a>
<!-- 简写形式 -->
<a @click="doSomething">...</a>
```

这里的参数是要监听的事件的名称： click **v-on** 有一个相应的缩写，即 **@** 字符。

### 动态参数

同样的指令参数上也可以使用一个 JavaScript 表达式，需要包含在一对方括号内：  
`<a v-bind:[attributeName]="url">...</a>`  
简写形式：  
`<a :[attributeName]="url">...</a>`  
这里的 attributeName 会作为一个 JavaScript 表达式被动态执行，计算得到的值会被用作最终的参数。举例来说：如果组件实例有一个数据属性 attributeName 其值为 "href" 那么这个绑定就等价于 v-bind:href  
相似的，还可以将一个函数绑定到动态的事件名称上：  
`<a v-on:[eventName]="doSomething">...</a>`  
简写形式：  
`<a :[eventName]="doSomething">...</a>`  
在此示例中，当 eventName 的值是 "focus" 时 v-on:[eventName] 就等价于 v-on:focus

### 动态参数值的限制

动态参数中表达式的值应当是一个字符串，或者是 null 特殊值 null 意为显示移除该绑定。其他非字符串的值会触发警告。

### 动态参数语法的限制

动态参数表达式因为某些字符的缘故有一些语法限制。比如空格和引号，在 HTML Attribute 名称中都是不合法的。例如下面的示例：  
这会触发一个编译器警告 `<a:['foo'+bar]="value">...</a>`  
如果需要传入一个复杂的动态参数，推荐使用计算属性替换复杂的表达式，也是 Vue 最基础的概念之一。  
当使用 DOM 内嵌模板(直接写在 HTML 文件里的模板)时，需要避免在名称中使用大写字母，因为浏览器会强制将其转换为小写：  
`<a:[someAttr]="value">...</a>`  
上面的例子将会在 DOM 内嵌模板中被转换为 `:[someattr]` 如果组件拥有 "someAttr" 属性而非 "someattr" 这段代码将不会工作。单文件组件内的模板不受此限制。

### 修饰符 Modifier

修饰符是以点开头的特殊后缀，表明指令需要以一些特殊的方式被绑定。例如： `.prevent` 修饰符会告知 `v-on` 指令对触发的事件调用 `event.preventDefault()`  
`<form @submit.prevent="onSubmit">...</form>`  
之后在讲到 `v-on` 和 `v-model` 的功能时，将会看到其他修饰符的例子。  
完整的指令语法： `v-on:submit.prevent="onSubmit"`

### 响应式的基础

可以使用 `reactive()` 函数创建一个响应式对象或数组：  
代理对象： `const state = reactive({count:99})`  
`console.log(state.count)`  
需要注意的是：  

```ts
import {reactive} from 'vue';

const raw = {count:99}
const state = reactive(raw)
console.log(state)
// 代理对象和原始对象不是全等的
console.log(raw === state)
```

只有代理对象时响应式的，更改原始对象不会触发更新。  
因此，使用 Vue 的响应式系统的最佳实践是： 仅使用你声明对象的代理版本。

### reactive() 的局限性

reactive() API 有两条限制：

1. 仅对对象类型有效(对象、数组和 Map, Set 这样的集合类型)，而对 string, number, boolean 这样的原始类型无效。
2. 因为 Vue 的响应式系统是通过属性访问进行追踪的，因此我们**必须始终保持对该响应式对象的相同引用**。这意味着我们不可以随时的“替换”一个响应式对象，因为这将会导致对初始引用的响应性连接丢失：  
`let state = reactive({count:0})`  
`state = reactive({count:1})` 上面的引用 `({count:0})` 将不再被追踪(响应性连接丢失！)  
同时这也意味着当我们将响应式对象的属性赋值或解构至本地变量时，或是将该属性传入一个函数时，我们会失去响应性：  

    ```ts
    const state = reactive({count:0});
    // n 是一个局部变量，同 state.count
    // 失去响应性连接
    let n = state.count
    // 不影响原始的 state
    n++
    // count 也和 state.count 失去了响应性连接
    let {count}=state
    // 不会影响原始的 state
    count ++
    ```

### ref()

`reactive()` 的种种限制归根结底是因为 JavaScript 没有可以作用于所有值类型的“引用”机制。为此 Vue 提供了一个 `ref()` 方法来允许我们创建可以使用任何值类型的响应式 `ref`  

```ts
import {ref} from 'vue';

// ref() 将传入参数的值包装为一个带 .value 属性的 ref 对象
const count = ref(0);
console.log(count); // {value:0}
console.log(count.value); // 0
count.value++;
console.log(count.value); // 1
```

和响应式对象的属性类似， `ref` 的 `.value` 属性也是响应式的  
`ref` 被传递给函数或是从一般对象上被解构时，不会丢失响应性

```ts
const obj = {
    foo: ref(1),
    bar: ref(2)
};
const {foo, bar} = obj;
console.log(foo, bar);
```

简言之，`ref()` 让我们能创造一种对任意值的“引用”，并能够在不丢失响应性的前提下传递这些引用。

### ref() 在模板中的解包

当 `ref` 在模板中作为顶层属性被访问时，它们会被自动“解包”，所以不需要使用 `.value` 下面是之前的计数器例子，用 `ref()` 代替：

```html
<script setup>
    import {ref} from 'vue'

    const count = ref(0)

    function increment() {
        count.value++
    }
</script>

<template>
    <button @click="increment">
    <!-- 无需 .value -->
    {{count}}
    </button>
</template>
```

### ref 在响应式对象中的解包

当一个 `ref` 被嵌套在一个响应式对象中，作为属性被访问或更改时，它会自动解包，因此会表现的和一般属性一样：

```ts
const count = ref(0);
const state = reactive({count});

console.log(state.count); // 0
state.count = 1;
console.log(count.value); // 1
```

### 数组和集合类型的 ref 解包

同响应式对象不同，当 `ref` 作为响应式数组或像 Map 这种原生集合类型的元素被访问时，不会进行解包。

```ts
const books = reactive([ref("Vue 3 Guide")]);
// 这里需要 .value
console.log(books[0].value);
//
const map = reactive(new Map(['count']));
// 这里需要 .value
```

### 计算属性

模板中的表达式虽然方便，但也只能用来做简单的操作。如果在模板中写入太多的逻辑，会让模板变得臃肿，难以维护。比如说，我们有这样一个包含嵌套数组的对象：

```ts
const author = reactive({
    name: 'John Doe',
    books: [
        'Vue - 1',
        'Vue - 2',
        'Vue - 3'
    ]
});
// 我们像根据 author 是否已有一些书籍来展示不同的信息
// <p>Has published books:</p>
// <span>{{ author.books.length > 0 ? 'Yes':'No' }}</span>
```

推荐使用计算属性来描述依赖响应式状态的复杂逻辑。

```ts
import { reactive, ref, computed } from "vue"

const author = reactive({
  name: 'John Doe',
  books: ['Vue-1', 'Vue-2', 'Vue-3']
});
// ---------- 计算属性 ----------
const publishedBooksMessage = computed(() => {
  return author.books.length > 0 ? 'Yes' : 'No';
});
// <span>{{ `通过计算属性 ${publishedBooksMessage}` }}</span>
```

### 计算属性缓存 vs 方法

你可能注意到我们在表达式中像这样调用一个函数也会获得同计算属性相同的结果：  
`<p>{{ calculateBooksMessage() }}</p>`  
组件中  
`function calculateBooksMessage(){ return author.books.length>0?"Yes":"No"}`  
若我们将同样的函数定义为一个方法而不是计算属性，两种方法在结果上确实是完全相同的，然而，不同之处在于计算属性值会基于其响应式依赖缓存。一个计算属性仅会在其响应式依赖更新时才重新计算。这意味着只要 `author.books` 不改变，无论多少次访问 `publishedBooksMessage` 都会立即返回先前的计算结果，而不用重复执行 `getter` 函数。  
这也解释了为什么下面的计算属性永远不会更新，因为 `Date.now()` 并不是一个响应式依赖：  
`const now = computed(()=>{Date.now()});`  
相比之下，方法调用总是会在重新渲染发生时再次执行函数。

### 可写计算属性(不推荐)

计算属性默认是只读的。当你尝试修改一个计算属性时，你会收到一个运行时警告。只在某些特殊场景中你可能才需要用到“可写”的属性，你可以通过同时提供 `getter & setter` 来创建。

```ts
import { ref,computed } from "vue";
const firstName = ref("John");
const lastName = ref("Doe");
const fullName = computed(() => {
    // getter
    get() {
        return firstName.value + " " + lastName;
    },
    // setter
    set(newValue) {
        // PS: 这里使用的是解构赋值语法
        [firstName.value, lastName.value] = newValue.split(" ");
    }
});
fullName.value = "上官 海棠";
```

### Class 与 Style 绑定

数据绑定的一个常见需求场景是操纵元素的 `CSS class` 列表和内联样式。  
因为 `class` 和 `style` 都是 `Attribute` 我们可以和其他 `attribute` 一样使用 `v-bind` 将它们和动态的字符串绑定。  
但是，在处理比较复杂的绑定时，通过拼接生成字符串是麻烦且易出错的。  
因此 Vue 专门为 `class` 和 `style` 的 `v-bind` 用法提供了特殊的功能增强。  
除了字符串外，表达式的值也可以是对象或数组。

### 绑定对象

我们可以给 :`class`(`v-bind:class` 的缩写) 传递一个对象来动态切换 `class`  
`<div :class="{ active: isActive }"></div>`  
上面的语法表示 `active` 是否存在取决于数据属性 `isActive` 的真假值

### 多个 Class

可以在对象中写多个字段来操作多个 `class` 此外 `:class` 指令也可以和一般的 `class attribute` 共存。  
`const isActive = ref(true)`  
`const hasError = ref(false)`  
配合以下模板：  
`<div class="static" :class="{ active:isActive,'text-danger':hasError}">...</div>`  
渲染结果是：  
`<div class="static active">...</div>`

### 绑定数组

可以给 :class 绑定一个数组来渲染多个 CSS class  
`const activeClass = ref("active")`  
`const errorClass = ref("text-danger")`
`<div :class="[activeClass, errorClass]">...</div>`  
渲染结果是：  
`<div class="active text-danger">...</div>`  
若想在数组中有条件的渲染某个 `class` 可以使用三元表达式：  
`<div :class="[isActive?activeClass:'', errorClass]">...</div>`  
`errorClass` 会一直存在，但 `activeClass` 只会在 `isActive` 为真时才存在。  
然而，这可能在有多个依赖条件的 `class` 时会有些冗长。因此也可以在数组中嵌套对象：  
`<div :class="[{active:isActive}, errorClass]">...</div>`

### 在组件上使用

对于只有一个根元素的组件，当你使用了 class attribute 时，这些 class 会被添加到跟元素上，并与该元素上已有的 class 合并。  
示例： 声明一个组件 MyComponent 模板如下：  
子组件模板： `<p class="foo bar">...</p>`  
使用时添加一些 class `<MyComponent class="baz boo">...</MyComponent>`  
渲染后的 HTML `<p class="foo bar baz boo">...</p>`

### 绑定内联样式-绑定对象

`:style` 支持绑定 JavaScript 对象值，对应的是 HTML 元素的 `style` 属性  
`const activeColor = ref("red")`  
`const fontSize = ref(30)`
`<div :style="{color:activeColor, fontSize:fontSize+'px'}">...</div>`  
`<div :style="{color:activeColor, font-size:fontSize+'px'}">...</div>`  
直接绑定一个样式对象，可以使模板更加简洁：  
`const styleObj = reactive({color:"red", fontSize:"13px"});`  
`<div :style="styleObj">...</div>`  
若样式对象需要更复杂的逻辑，可以使用返回样式对象的计算属性。

### Style 绑定数组

可以给 :style 绑定一个包含多个样式对象的数组。这些对象会被合并后渲染到同一元素上：  
`<div :style="[baseStyle, overridingStyle]">...</div>`

### 样式多值

可以对一个样式属性提供多个(不同前缀)值  
`<div :style="{display:['-webkit-box','-ms-flexbox','flex']}">...</div>`  
数组仅会渲染浏览器支持的最后一个值。

### 条件渲染

`v-if` 指令用于条件性的渲染一块内容。这块内容只会在指令的表达式返回真值时才被渲染。
`<h1 v-if="awesome">Vue is awesome!</h1>`  
也可以使用 `v-else` 为 `v-if` 添加一个 else 区块。  
`<button @click="awesome =!awesome">Toggle</button>`

```html
<h3 v-if="awesome">Vue is awesome!</h3>
<h3 v-else>awesome = false</h3>
```

一个 `v-else` 元素必须跟在一个 `v-if` 或者 `v-else-if` 元素后面，否则它将不会被识别。  
`v-else-if` 提供的是相应于 `v-if` 的 "else if" 区块。它可以连续多次重复使用。

### \<template> 上的 v-if

因为 `v-if` 是一个指令，它必须依附于某个元素。但如果我们想要切换不止一个元素呢？在这种情况下我们可以在一个 `<template>` 元素上使用 `v-if` 这只是一个不可见的包装元素，最后渲染的结果并不会包含这个 `<template>` 元素。

```html
<template v-if="ok">
    <h1>Title</h1>
    <p>Paragraph 1</p>
    <p>Paragraph 2</p>
</template>
```

`v-else` & `v-else-if` 也可以在 `<template>` 元素上使用。

### v-show

另一个可以用来按条件显示一个元素的指令是 `v-show` 其用法基本一样：  
`<h3 v-show="ok">Hello</h3>`  
不同之处在于 `v-show` 会在 DOM 渲染中保留该元素。  
`v-show` 仅切换了该元素上名为 `display` 的 CSS 属性。  
`v-show` 不支持在 `<template>` 元素上使用，也不能和 `v-else` 搭配使用。

### **v-if** *vs* **v-show**

`v-if` 是“真实的”按条件渲染，因为它确保了在切换时，条件区块内的事件监听器和子组件都会被销毁与重建。  
`v-if` 也是惰性的： 如果在初次渲染时条件值为 false 则不会做任何事。条件区块只有当条件首次变为 true 时才被渲染。  
相比之下 `v-show` 简单许多，元素无论初始条件如何，始终都会被渲染，只有 `CSS display` 属性会被切换。  
总的来说 `v-if` 有更高的切换开销，而 `v-show` 有更高的初始渲染开销。因此，如果需要频繁切换，则使用 `v-show` 较好，如果在运行时绑定条件很少改变，则 `v-if` 会更合适。

### v-for

可以使用 v-for 指令基于一个数组来渲染一个列表。 v-for 指令的值需要使用 item in items 形式的特殊语法，其中 items 源数据的数组，而 item 是迭代项的别名：  
`const items = ref([{message:"Foo"},{message:"Bar"}])`  
`<ul>`  
`<li v-for="item in items">{{ item.message }}</li>`  
`</ul>`  
在 v-for 块中可以完整的访问父作用域内的属性和变量。 v-for 也支持使用可选的第二个参数表示当前项的位置索引。  
`const parentMessage = ref("Parent")`  
`const items = ref([{message:"Foo"},{message:"Bar"}])`  
`<ul>`  
`<li v-for="(item, index) in items">{{ parentMessage }}-{{ index }}-{{ item.message }}</li>`  
`</ul>`

### v-for 与对象

可以使用 v-for 来遍历一个对象的所有属性。遍历的顺序会基于对该对象调用 `Object.keys()` 的返回值来决定。

```ts
const myObject = reactive({
  title: "How to do lists in Vue",
  author: "Jane Doe",
  publishAt: "2016-04-10"
});

const myObject = ref({
  title: "How to do lists in Vue",
  author: "Jane Doe",
  publishAt: "2016-04-10"
});
```

第二个参数键 `(value, key)`  
第三个参数索引 `(value, key, index)`

```html
<div>
  <h2>v-for 与对象</h2>
  <ul>
    <li v-for="(value, key) in myObject">{{ value }} - {{ key }}</li>
  </ul>
  <ul>
    <li v-for="(value, key, index) in myObject">{{ value }} - {{ key }} - {{ index }}</li>
  </ul>
</div>
```

### 在 v-for 里使用范围值

`v-for` 可以直接接收一个整数值。在这种用例中，会将该模板基于 1...n 的取值范围重复多次。  
`<span v-for="n in 10">{{n}}</span>`  
**PS:** 此处 n 的初始值是从 1 开始而非 0

### **v-if** & **v-for**

PS:  
同时使用 `v-if` 和 `v-for` 是不推荐的，因为这样二者的优先级不明显。不建议处于同一层级。  
当 `v-if` 和 `v-for` 同时存在于某个元素上的时候 `v-if` 会首先被执行。

```html
<div v-if="ok">
  <span v-for="n in 10">{{ n }}</span>
</div>
```

### 监听事件

可以使用 `v-on` (简写为： @)来监听 DOM 事件，并在事件触发时执行对应的 JavaScript 用法： `v-on:click="methodName"` 或者 `@click="methodName"`  
事件处理器的值可以是：

1. 内联事件处理器： 事件被触发时执行的内联 JavaScript 语句(与 onclick 类似)。
2. 方法事件处理器： 一个指向组件上定义的方法的属性名或是路径。

```ts
const Func1 = () => {
  console.log("Func1()");
};
```

```html
<button v-on:click="Func1">点击我</button>
```

### 内联事件处理器

内联事件处理器通常用于简单场景。例如：

```ts
const count = ref(0);
```

```html
<div>
  <button @click="count++">Add 1</button>
  <p>Count is {{ count }}</p>
</div>
```

### 方法事件处理器

随着事件处理器的逻辑变得愈发复杂，内敛代码方式变得不够灵活。因此 `v-on` 也可以接收一个方法名或对某个方法的调用。

```ts
const name = ref("Vue.js");
function greet(event) {
  alert(`Hello ${name.value}`);
  // event 是 DOM 原生事件
  if (event) {
    alert(event.target.tagName);
  }
}
```

```html
<button @click="greet">Greet</button>
```

### 按键修饰符

在监听键盘事件时，经常需要检查特定按键。 Vue 允许在 v-on 或 @ 监听按键事件时添加案件修饰符。

```html
<!-- 仅在按键为 enter 时调用 submit -->
<input type="text" placeholder="回车登录" @keyup.enter="submit">
```

### 按键别名

Vue 为一些常用的按键提供了别名：

|普通按键|系统按键|
|----|----|
|.enter|.ctrl|
|.tab|.alt|
|.delete|.shift|
|.esc|.meta|
|.space||
|.up||
|.down||
|.left||
|.right||

### 表单输入绑定

在前端处理表单时，我们常常需要将表单输入框的内容同步给 JavaScript 中相应的变量。手动连接值绑定和更改事件监听器可能会很麻烦。

```ts
const text = ref();
```

```html
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
```

### 生命周期

每个 Vue 组件实例在创建时都需要经历一系列的初始化步骤，比如设置好数据侦听，编译模板，挂载实例到 DOM 以及在数据改变时更新 DOM 在此过程中，它也会运行被称为生命周期钩子的函数，让开发者有机会在特定阶段运行自己的代码。

### 注册周期钩子

举例说明 `onMounted` 钩子可以用来完成组件完成初始渲染并创建 DOM 节点后运行代码。

```ts
import { ref, onMounted } from 'vue';

onMounted(() => {
  console.log("The component is now mounted.");
});
```
