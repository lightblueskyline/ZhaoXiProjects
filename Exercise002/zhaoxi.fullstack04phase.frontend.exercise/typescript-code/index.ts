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