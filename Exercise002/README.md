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

### 侦听器

计算属性允许我们声明性地计算衍生值。然而在有些情况下，我们需要在状态变化时执行一些“副作用”： 例如更改 DOM 或是根据异步操作的结果去修改另一处的状态。  
在组合式 API 中，我们可以使用 `watch` 函数在每次响应状态发生变化时触发回调函数：

```ts
// 侦听输入框新旧值的变化
watch(searchVal, (newQuestion, oldQuestion) => {
    console.log(`newQuestion: ${newQuestion}`);
    console.log(`oldQuestion: ${oldQuestion}`);
});
```

### 模板引用

虽然 Vue 的声明性渲染模型为你抽象了大部分对 DOM 的直接操作，但在某些情况下，我们仍然需要直接访问底层 DOM 元素。要实现这一点，我们可以使用特殊的 `ref attribute`  
`<input ref="templateVal" />`  
`ref` 是一个特殊的 `attribute` 和 `v-for` 章节中提到的 `key` 类似。它允许我们在一个特定的 DOM 元素或子组件实例被挂载后，获得对它的直接引用。这可能很有用，比如说在组件挂载时将焦点设置到一个 `input` 元素上，或在一个元素上初始化一个第三方库。

```ts
// ---------- 模板引用 ----------
const templateVal = ref(); // 创建模板引用名称
onMounted(() => {
  console.log("The component is now mounted.");
  // 页面加载完成后，将光标设置至此元素
  templateVal.value.focus();
  console.log("Set focus to input(templateVal)");
});
```

```html
<p><input type="text" name="" id="" ref="templateVal"></p>
```

### 组件基础

组件允许我们将 UI 划分为独立的、可重用的部分，并且可以对每个部分进行单独的思考。在实际应用中，组件常常被组织成层层嵌套的树状结构。

### 定义一个组件

当使用构建步骤时，我们一般会将 Vue 组件定义在一个单独的 `.vue` 文件中，这被叫做单文件组件(简称为： SFC)

```html
<!-- vue-project-vite-ts003\src\components\MyComponent.vue -->
<!-- 使用双单词定义组件文件名称 -->
<script setup lang="ts">
import { ref } from "vue";

const count = ref(0);
</script>

<template>
    <button @click="$event => count++">You click me {{ count }} times.</button>
</template>
```

使用组件：

```ts
// 在脚本区块顶部引入
import MyComponent from './components/MyComponent.vue';
```

```html
<!-- 组件基础 -->
<div>
  <h2>组件基础</h2>
  <p>
    <!-- 作为标签来使用 -->
    <!-- 组件可以复用 -->
    <MyComponent></MyComponent>
  </p>
</div>
```

### 组件传递 props

定义组件：

```html
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
```

使用组件：

```html
<!-- import MyComponent1 from './components/MyComponent1.vue'; -->
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
```

### 组件监听事件

在组件的模板表达式中，可以直接使用 `$emit` 方法触发自定义事件。  
`<div><button @click="$emit('change')">调用父组件并修改变量</button></div>`  
父组件可以通过 `v-on`(简写 @)来监听事件  
子组件：

```html
<!-- vue-project-vite-ts003\src\components\MyComponent2.vue -->
<!-- 组件监听事件 -->
<script setup lang="ts">
defineProps({
    // 1. 类型声明
    // 2. 数据类型
    // 3. 大写字母开头
    title: String
});
</script>

<template>
    <h3>{{ title }}</h3>
    <!-- parentFunc 同父组件中定义的方法名称 -->
    <button @click="$event => $emit('parentFunc')">子组件调用父组件中的方法并修改变量</button>
</template>

<style scoped></style>
```

父组件：

```ts
import MyComponent2 from './components/MyComponent2.vue';
// ---------- 组件监听事件 ----------
const emitsBoolean = ref(true);
const parentFunc = () => {
  emitsBoolean.value = !emitsBoolean.value;
};
```

```html
<!-- 组件监听事件 -->
<div>
  <h2>组件监听事件</h2>
  <p>{{ emitsBoolean }}</p>
  <p>
    <!-- <MyComponent2 title="標題參數" @parentFunc="$event => { emitsBoolean = !emitsBoolean }"></MyComponent2> -->
    <MyComponent2 title="標題參數" @parentFunc="parentFunc"></MyComponent2>
  </p>
</div>
```

### 组件监听事件传递参数

子组件：

```html
<script setup lang="ts">
defineProps({
    // 1. 类型声明
    // 2. 数据类型
    // 3. 大写字母开头
    title: String
});
</script>

<template>
    <h3>{{ title }}</h3>
    <!-- parentFunc 同父组件中定义的方法名称 -->
    <button @click="$event => $emit('parentFunc1', '参数来自组件 MyComponent3')">子组件调用父组件中的方法传递参数</button>
</template>

<style scoped></style>
```

父组件：

```ts
import MyComponent3 from './components/MyComponent3.vue';
// ---------- 组件监听事件传递参数 ----------
const parentFunc1 = (param: string) => {
  console.log(param);
};
```

```html
<!-- 组件监听事件传递参数 -->
<div>
  <h2>组件监听事件传递参数</h2>
  <p>
    <MyComponent3 title="标题参数 MyComponent3" @parentFunc1="parentFunc1"></MyComponent3>
  </p>
</div>
```

### 声明事件的触发

子组件：

```html
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
```

父组件：

```ts
import MyComponent4 from './components/MyComponent4.vue';
// ---------- 声明事件的触发 ----------
const parentFunc2 = () => {
  console.log("vue-project-vite-ts003\src\components\MyComponent4.vue - parentFunc2");
};
const parentFunc3 = () => {
  console.log("vue-project-vite-ts003\src\components\MyComponent4.vue - parentFunc3");
};
```

```html
<!-- 声明事件的触发 -->
<div>
  <h2>声明事件的触发</h2>
  <p>
    <MyComponent4 title="标题参数 MyComponent4" @parentFunc2="parentFunc2" @parentFunc3="parentFunc3"></MyComponent4>
  </p>
</div>
```

## TypeScript 学习

安装 TypeScript 需要 Node.js 环境  
TypeScript 安装命令：

```bash
npm install -g typescript

# 查看安装后版本
tsc --version

# 编译
tsc index.ts
# 指定输出目录
tsc --outFile ./compile_js/index.js index.ts
```

### 基本数据类型

```ts
let message: string = "Hello TypeScript";
console.log(message);

// 布尔值
let isDone: boolean = true;

// 数值
let num: number = 666;

// 字符串
let myName: string = "M_0v0_M";
// 模板字符串
let sentence: string = `Hello, My name is ${myName}`;

// 空值
// JavaScript 没有空值(void)的概念，在 TypeScript 中，可以用 void 表示没有任何返回值的函数
function alertName(): void {
    alert(`Hello, My name is ${myName}`);
}
// 声明一个 void 类型的变量没有什么作用，因为你只能将它赋值为 undefined 和 null
let unusable: void = undefined; // 无意义的变量定义(吃饱了撑的)

// Null 和 Undefined
// 在 TypeScript 中，可以使用 null & undefined 来定义这两个原始数据类型
let u: undefined = undefined;
let n: null = null;
```

### 任意值(Any)

**任意值(Any)**用来表示允许赋值为任意类型。  

```ts
let myFavoriteNumber:any = "seven";
myFavoriteNumber = 7;
```

在任意值上访问任何属性都是允许的

```ts
let anyThing:any = "Hello";
console.lot(anyThing.myName);
console.lot(anyThing.myName.firstName);
```

允许调用任何方法

```ts
let anyThing:any = "Hello";
anyThing.setName("Jane");
anyThing.setName("Jane").sayHello();
anyThing.myName.setFirstName("Cat");
```

声明一个变量为任意值之后，对它的任何操作，返回内容的类型都是任意值。

### 类型推论

如果没有明确的指定类型，那么 TypeScript 会依照类型推论(Type Inference)的规则推断出一个类型。

```ts
let myFavoriteNumber = "seven";
// myFavoriteNumber = 7; // 报出类型错误
// 等同于：
let myFavoriteNumber:string = "seven";
// myFavoriteNumber = 7; // 报出类型错误
```

PS: **如果定义的时候没有赋值，不管之后有没有赋值，对会被推断成 any 类型且完全不被类型检查**

```ts
let myFavoriteNumber;
myFavoriteNumber = "Seven";
myFavoriteNumber = 7;
```

### 联合类型

联合类型(Union Type)表示类型可以为多种类型中的一种。

```ts
let myFavoriteNumber: string | number;
myFavoriteNumber = "Seven";
myFavoriteNumber = 7;
// myFavoriteNumber = false; // 报错
```

PS: **访问联合类型的属性和方法**  
当 TypeScript 不确定一个联合类型的变量到底是哪个类型的时候，我们**只能访问此联合类型的所有类型里共有的属性和方法**

```ts
// function getLength(param: string | number): number {
//     return param.length; // 报错 number 没有 length 属性
// }

function getLength(param: string | number): string {
    return param.toString();
}

let myFavoriteNumber: string | number;
myFavoriteNumber = "Seven";
console.log(myFavoriteNumber.length);
myFavoriteNumber = 7;
console.log(myFavoriteNumber.length); // 报错
```

### 接口

属性无默认值，若需要有默认值，请使用 Class

```ts
interface Person {
    Name: string;
    Age: number;
};
let tom: Person = {
    Name: "Tom",
    Age: 25
};
console.log(tom.Name, tom.Age);
// let tom1: Person = {
//     Name: "Tom1"
//     // 报错：使用时，不允许缺少 Age
// }
```

PS: **可选属性** 不用完全匹配一个形状

```ts
interface Person {
    Name: string;
    Age?: number;
};
let tom: Person = {
    Name: "Tom"
};
```

PS: **任意属性**

```ts
interface Person {
    Name: string;
    Age?: number;
    [propName: string]: any;
};
let tom: Person = {
    Name: "Tom",
    Gender: "Male"
};
```

PS: **只读属性**

```ts
interface Person {
    readonly ID: number; // 只允许初始化时赋值
    Name: string;
    Age?: number;
};
let tom: Person = {
    ID: 9527,
    Name: "Tom",
    Age: 25
};
// tom.ID = 777; // 报错
```

### 数组

```ts
let fibonacci: number[] = [1, 1, 2, 3, 5];
fibonacci.push(8);
```

### 函数01：函数声明、函数表达式

```ts
// 函数声明(Function Declaration)
function sum(x, y) {
    return x + y;
}
// 函数表达式(Function Expression)
let mySum = function (x, y) {
    return x + y;
}
function sumTS(x: number, y: number): number {
    return x + y;
}
```

### 函数表达式

```ts
// 此代码只对等号右边的匿名函数进行了类型定义
// 等号左边的 mySum 是通过赋值操作进行类型推断得出
let mySum = function (x: number, y: number): number {
    return x + y;
}
// 应该如此定义
let mySum1: (x: number, y: number) => number = function (x: number, y: number): number {
    return x + y;
}
```

### 用接口定义函数的形状

```ts
interface SearchFunc {
    (source: string, subString: string): boolean;
}
let mySearch: SearchFunc;
mySearch = function (source: string, subString: string): boolean {
    return source.search(subString) !== -1;
}
```

### 函数02： 可选参数、参数默认值

```ts
// 函数可选参数
// 注意： 可选参数必须在必需参数之后，可选参数之后不允许再出现必需参数
function buildName(firstName: string, lastName?: string): string {
    if (lastName) {
        return firstName + " " + lastName;
    } else {
        return firstName;
    }
}
let tomcat = buildName("Tom", "Cat");
let tom = buildName("Tom");
```

### 函数默认值

```ts
function buildName(firstName: string, lastName: string = "Cat"): string {
    if (lastName) {
        return firstName + " " + lastName;
    } else {
        return firstName;
    }
}
let tomcat = buildName("Tom", "Cat");
let tom = buildName("Tom");
function buildName1(firstName: string = "Tom", lastName: string): string {
    return firstName + " " + lastName
}
let tomcat1 = buildName1("Tom", "Cat");
let cat1 = buildName1(undefined, "Cat");
```

### 函数03： 剩余参数、重载

```ts
function push1(array, ...items) {
    items.forEach(function (item) {
        array.push(item);
    });
}
let a1: any[] = [];
push1(a1, 1, 2, 3);
function push2(array: any[], ...items: any[]) {
    items.forEach(function (item) {
        array.push(item);
    });
}
let a2 = [];
push1(a2, 1, 2, 3);
```

### 函数重载

```ts
function reverse(x: number | string): number | string | void {
    if (typeof x === "number") {
        return Number(x.toString().split("").reverse().join(""));
    } else if (typeof x === "string") {
        return (x.split("").reverse().join(""))
    }
}

// 通过重载更加清晰化
function reverse(x: number): number;
function reverse(x: string): string;
function reverse(x: number | string): number | string | void {
    if (typeof x === "number") {
        return Number(x.toString().split("").reverse().join(""));
    } else if (typeof x === "string") {
        return (x.split("").reverse().join(""))
    }
}
reverse(123);
reverse("321");
```

### 类型断言01： 语法，将一个联合类型断言为其中一个类型

类型断言(Type Assertion)可以用来手动指定一个值的类型。  
语法： **值 as 类型**(推荐使用此种方式) 或者 **<类型>值**

### 将一个联合类型断言为其中一个类型

```ts
interface Cat {
    Name: string;
    run(): void;
}
interface Fish {
    Name: string;
    swim(): void;
}
function getName(animal: Cat | Fish): string {
    return animal.Name;
}
function isFish(animal: Cat | Fish): boolean {
    // 断言
    if (typeof (animal as Fish) === "function") {
        return true;
    }
    return false;
}
```

### 类型断言02： 将父类断言为更加具体的子类

```ts
class ApiError extends Error {
    code: number = 0;
}
class HttpError extends Error {
    statusCode: number = 200;
}
function isApiError(error: Error): boolean {
    if (typeof (error as ApiError).code === "number") {
        return true;
    }
    return false;
}
// class 可以使用 instanceof
// interface 不可以使用 instanceof
function isApiError1(error: Error): boolean {
    if (error instanceof ApiError) {
        return true;
    }
    return false;
}
```

### 类型断言03： 将任何一个类型断言为 Any

```ts
// window.foo = 1; // 报错
(window as any).foo = 1;
```

### 类型断言04： 将 Any 断言为一个具体的类型

```ts
function getCacheData(key: string): any {
    return (window as any).cache[key];
}
interface Cat {
    Name: string;
    run(); void;
}
const tom = getCacheData("Tom") as Cat;
tom.run();
```

### 类型断言05： 类型断言的限制

- 联合类型可以被断言为其中一个类型
- 父类可以被断言为子类
- 任何类型都可以被断言为 any
- any 可以被断言为任何类型

PS: **类型之间有交叉就可以互相断言**

### 类型断言06： 双重断言

```ts
interface Cat {
    run(): void;
}
interface Fish {
    swim(): void;
}
function testCat(param: Cat) {
    return (param as any as Fish);
}
```

### 类型断言07： 类型断言 vs 类型转换

类型断言： **指鹿为马**(本质未变)  
类型转换： **使用魔法**

```ts
function toBoolean(something: any): boolean {
    return something as boolean;
}
toBoolean(1); // 结果为： 1 (类型断言不是类型转换，不会真的影响到变量的类型)
// 真正的类型转换
function toBoolean1(something: any): boolean {
    return Boolean(something);
}
toBoolean1(1); // 结果为： true
```

### 类型断言08： 类型断言 vs 类型声明

1. A 断言为 B 时，A 和 B 有重叠的部分即可
2. A 声明为 B 时，A 必需具备 B 的所有属性和方法

类型声明更加严格

### 类型断言09： 类型断言 vs 泛型

```ts
// 通过泛型实现
// 最优解决方案
function getCacheData<T>(key: string): T {
    return (window as any).cache[key];
}
interface Cat {
    Name: string;
    run(); void;
}
const tom = getCacheData<Cat>("Tom");
tom.run();
```

### 使用 type 关键字定义类型别名和字符串字面量类型

```ts
function getName(n: string | (() => string)): string {
    if (typeof n === "string") {
        return n;
    } else {
        return n();
    }
}
// 类型别名就是为类型起别名
type Name = string;
type NameResolver = () => string;
type NameOrResolver = Name | NameResolver;
function getName1(n: NameOrResolver): Name {
    if (typeof n === "string") {
        return n;
    } else {
        return n();
    }
}
// 类型别名常用于联合类型
type EventNames = "click" | "scroll" | "mouseover";
function handleEvent(ele: Element, evetn: EventNames) {
    // do something
}
```

### 元组

数组合并了相同类型的对象，**元组**(Tuple)合并了不同类型的对象。

```ts
// 对元组进行初始化时，需要提供所有元组类型中指定的项
let tom: [string, number];
tom = ["Tom", 25];
tom.push("Jane"); // 添加越界元素
// tom.push(true); // 错误，类型出错
```

### 枚举

```ts
enum Days { Sun, Mon, Tue, Wed, Thu, Fri, Sat };
console.log(Days["Mon"] === 1); // true
```

### 类01： 概念、构造函数、属性和方法

```ts
class Animal {
    public _name;
    constructor(name: string) {
        this._name = name;
    } // 构造器
    sayHi() {
        return `My name is ${this._name}`;
    }
}
let temp = new Animal("Jack");
console.log(temp.sayHi());
// 逻辑中附带行为，使用类实现
// 若只作为约束，使用接口
```

### 类02： 存取器 get set

```ts
class Animal {
    constructor(name) {
        this.name = name;
    }
    get name() {
        return "Jack";
    }
    set name(value) {
        console.log("setter: " + value);
    }
}
let temp = new Animal("Kitty"); // setter: Kitty
temp.name = "Tom"; // setter: Tom
console.log(temp.name); // Jack
```

### 类03： 静态方法

使用 `static` 修饰符

```ts
class Animal {
    public _name;
    constructor(name: string) {
        this._name = name;
    }
    sayHi() {
        return `My name is ${this._name}`;
    }
    static sayHello() {
        return "Hello, 你好呀！";
    }
}
let temp = new Animal("Jack");
console.log(temp.sayHi()); // My name is Jack
console.log(Animal.sayHello()); // Hello, 你好呀！
```

### 类04： 三中访问修饰符： public private protected

- public 全局的，公共的，当前类所涉及到的地方都可以使用
- private 私有的，只能在类的内部使用，无法在实例化后通过类的实例属性来访问
- protected 受保护的， private 不允许子类访问， protected 可以在子类中访问

```ts
class Animal {
    protected name: string;
    public constructor(name: string) {
        this.name = name;
    }
}
class Cat extends Animal {
    constructor(name: string) {
        super(name);
        console.log(this.name);
    }
}
```

### 类05： 参数属性和只读属性关键字

修饰符和 readonly 还可以使用在构造函数参数中，等同于类中定义该属性同时给该属性赋值，使代码更简洁。

```ts
class Animal {
    // protected name: string;
    public constructor(public name: string) {
        // this.name = name;
    }
}
```

只读属性

```ts
class Animal {
    readonly name: string;
    public constructor(name: string) {
        this.name = name;
    }
}
let temp = new Animal("Jack");
console.log(temp.name);
// temp.name = "Tom"; // 报错
```

### 类06: 抽象类

定义抽象类和其中的抽象方法 `abstract`  抽象类不允许被实例化

```ts
abstract class Animal {
    public name: string;
    public constructor(name: string) {
        this.name = name;
    }
    public abstract sayHi(): void;
}
// let temp = new Animal("Jack"); // 无法实例化抽象类
class Cat extends Animal {
    public eat() {
        console.log(`${this.name} is eating.`);
    }

    public sayHi(): void {
        console.log(`My name is ${this.name}`);
    }
}
let cat = new Cat("Tom");
cat.sayHi();
cat.eat();
```

### 类与接口、类继承接口、接口继承接口、接口继承类

TypeScript 中，接口可以继承类

### 泛型01： 概念，简单示例

```ts
function createArray<T>(length: number, value: T): Array<T> {
    let result: T[] = [];
    for (let index = 0; index < length; index++) {
        result[index] = value;
    }
    return result;
}
createArray<string>(3, "X");
createArray(6, "Y");
```

### 泛型02： 多个类型参数

```ts
function swap<T, U>(tuple: [T, U]): [U, T] {
    return [tuple[1], tuple[0]];
}
swap([7, "Seven"]); // ["Seven", 7]
```

### 泛型03： 泛型约束

```ts
interface Lengthwise {
    length: number;
}
// 约束了泛型 T 必需符合接口 Lengthwise 的形状，也就是必需包含 length 属性
function loggingIdentity<T extends Lengthwise>(arg: T) {
    console.log(arg.length);
    return arg;
}
// 如此调用会报错
// loggingIdentity(7);
```

### 泛型04： 泛型接口

```ts
interface CreateArrayFunc {
    <T>(length: number, value: T): Array<T>;
}
let createArray: CreateArrayFunc;
createArray = function <T>(length: number, value: T): Array<T> {
    let result: T[] = [];
    for (let index = 0; index < length; index++) {
        result[index] = value;
    }
    return result;
}
createArray(6, "Z");

interface CreateArrayFunc1<T> {
    (length: number, value: T): Array<T>;
}
```

### 泛型05： 泛型类

```ts
class GenericNumber<T>{
    zeroValue: T;
    add: (x: T, y: T) => T;
}
let myGenericNumber = new GenericNumber<number>();
myGenericNumber.zeroValue = 0;
myGenericNumber.add = function (x, y) { return x + y; }
```

### 泛型参数的默认类型

```ts
function createArray<T = string>(length: number, value: T): Array<T> {
    let result: T[] = [];
    for (let index = 0; index < length; index++) {
        result[index] = value;
    }
    return result;
}
```

### 声明合并、同名函数、接口、类的合并
