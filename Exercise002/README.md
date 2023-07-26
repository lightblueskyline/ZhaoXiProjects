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
