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

// function getLength(param: string | number): string {
//     return param.toString();
// }

// let myFavoriteNumber: string | number;
// myFavoriteNumber = "Seven";
// console.log(myFavoriteNumber.length);
// myFavoriteNumber = 7;
// console.log(myFavoriteNumber.length); // 报错

// interface Person {
//     Name: string;
//     Age: number;
// };
// let tom: Person = {
//     Name: "Tom",
//     Age: 25
// };
// console.log(tom.Name, tom.Age);
// // let tom1: Person = {
// //     Name: "Tom1"
// //     // 报错：使用时，不允许缺少 Age
// // }

// interface Person {
//     Name: string;
//     Age?: number;
// };
// let tom: Person = {
//     Name: "Tom"
// };

// interface Person {
//     Name: string;
//     Age?: number;
//     [propName: string]: any;
// };
// let tom: Person = {
//     Name: "Tom",
//     Gender: "Male"
// };

// interface Person {
//     readonly ID: number; // 只允许初始化时赋值
//     Name: string;
//     Age?: number;
// };
// let tom: Person = {
//     ID: 9527,
//     Name: "Tom",
//     Age: 25
// };
// // tom.ID = 777; // 报错

// let fibonacci: number[] = [1, 1, 2, 3, 5];
// fibonacci.push(8);

// // 函数声明(Function Declaration)
// function sum(x, y) {
//     return x + y;
// }
// // 函数表达式(Function Expression)
// let mySum = function (x, y) {
//     return x + y;
// }
// function sumTS(x: number, y: number): number {
//     return x + y;
// }

// // 此代码只对等号右边的匿名函数进行了类型定义
// // 等号左边的 mySum 是通过赋值操作进行类型推断得出
// let mySum = function (x: number, y: number): number {
//     return x + y;
// }
// // 应该如此定义
// let mySum1: (x: number, y: number) => number = function (x: number, y: number): number {
//     return x + y;
// }

// interface SearchFunc {
//     (source: string, subString: string): boolean;
// }
// let mySearch: SearchFunc;
// mySearch = function (source: string, subString: string): boolean {
//     return source.search(subString) !== -1;
// }

// // 函数可选参数
// // 注意： 可选参数必须在必需参数之后，可选参数之后不允许再出现必需参数
// function buildName(firstName: string, lastName?: string): string {
//     if (lastName) {
//         return firstName + " " + lastName;
//     } else {
//         return firstName;
//     }
// }
// let tomcat = buildName("Tom", "Cat");
// let tom = buildName("Tom");

// function buildName(firstName: string, lastName: string = "Cat"): string {
//     if (lastName) {
//         return firstName + " " + lastName;
//     } else {
//         return firstName;
//     }
// }
// let tomcat = buildName("Tom", "Cat");
// let tom = buildName("Tom");
// function buildName1(firstName: string = "Tom", lastName: string): string {
//     return firstName + " " + lastName
// }
// let tomcat1 = buildName1("Tom", "Cat");
// let cat1 = buildName1(undefined, "Cat");

// function push1(array, ...items) {
//     items.forEach(function (item) {
//         array.push(item);
//     });
// }
// let a1: any[] = [];
// push1(a1, 1, 2, 3);
// function push2(array: any[], ...items: any[]) {
//     items.forEach(function (item) {
//         array.push(item);
//     });
// }
// let a2 = [];
// push1(a2, 1, 2, 3);

// function reverse(x: number | string): number | string | void {
//     if (typeof x === "number") {
//         return Number(x.toString().split("").reverse().join(""));
//     } else if (typeof x === "string") {
//         return (x.split("").reverse().join(""))
//     }
// }
// // 通过重载更加清晰化
// function reverse(x: number): number;
// function reverse(x: string): string;
// function reverse(x: number | string): number | string | void {
//     if (typeof x === "number") {
//         return Number(x.toString().split("").reverse().join(""));
//     } else if (typeof x === "string") {
//         return (x.split("").reverse().join(""))
//     }
// }
// reverse(123);
// reverse("321");

// interface Cat {
//     Name: string;
//     run(): void;
// }
// interface Fish {
//     Name: string;
//     swim(): void;
// }
// function getName(animal: Cat | Fish): string {
//     return animal.Name;
// }
// function isFish(animal: Cat | Fish): boolean {
//     // 断言
//     if (typeof (animal as Fish) === "function") {
//         return true;
//     }
//     return false;
// }

// class ApiError extends Error {
//     code: number = 0;
// }
// class HttpError extends Error {
//     statusCode: number = 200;
// }
// function isApiError(error: Error): boolean {
//     if (typeof (error as ApiError).code === "number") {
//         return true;
//     }
//     return false;
// }
// // class 可以使用 instanceof
// // interface 不可以使用 instanceof
// function isApiError1(error: Error): boolean {
//     if (error instanceof ApiError) {
//         return true;
//     }
//     return false;
// }

// // window.foo = 1; // 报错
// (window as any).foo = 1;

// function getCacheData(key: string): any {
//     return (window as any).cache[key];
// }
// interface Cat {
//     Name: string;
//     run(); void;
// }
// const tom = getCacheData("Tom") as Cat;
// tom.run();

// interface Cat {
//     run(): void;
// }
// interface Fish {
//     swim(): void;
// }
// function testCat(param: Cat) {
//     return (param as any as Fish);
// }

// function toBoolean(something: any): boolean {
//     return something as boolean;
// }
// toBoolean(1); // 结果为： 1 (类型断言不是类型转换，不会真的影响到变量的类型)
// // 真正的类型转换
// function toBoolean1(something: any): boolean {
//     return Boolean(something);
// }
// toBoolean1(1); // 结果为： true

// // 通过泛型实现
// function getCacheData<T>(key: string): T {
//     return (window as any).cache[key];
// }
// interface Cat {
//     Name: string;
//     run(); void;
// }
// const tom = getCacheData<Cat>("Tom");
// tom.run();

// function getName(n: string | (() => string)): string {
//     if (typeof n === "string") {
//         return n;
//     } else {
//         return n();
//     }
// }
// // 类型别名就是为类型起别名
// type Name = string;
// type NameResolver = () => string;
// type NameOrResolver = Name | NameResolver;
// function getName1(n: NameOrResolver): Name {
//     if (typeof n === "string") {
//         return n;
//     } else {
//         return n();
//     }
// }
// // 类型别名常用于联合类型
// type EventNames = "click" | "scroll" | "mouseover";
// function handleEvent(ele: Element, evetn: EventNames) {
//     // do something
// }

// // 对元组进行初始化时，需要提供所有元组类型中指定的项
// let tom: [string, number];
// tom = ["Tom", 25];
// tom.push("Jane"); // 添加越界元素
// // tom.push(true); // 错误，类型出错

// enum Days { Sun, Mon, Tue, Wed, Thu, Fri, Sat };
// console.log(Days["Mon"] === 1); // true

// class Animal {
//     public _name;
//     constructor(name: string) {
//         this._name = name;
//     } // 构造器
//     sayHi() {
//         return `My name is ${this._name}`;
//     }
// }
// let temp = new Animal("Jack");
// console.log(temp.sayHi());
// // 逻辑中附带行为，使用类实现
// // 若只作为约束，使用接口

// class Animal {
//     constructor(name) {
//         this.name = name;
//     }
//     get name() {
//         return "Jack";
//     }
//     set name(value) {
//         console.log("setter: " + value);
//     }
// }
// let temp = new Animal("Kitty"); // setter: Kitty
// temp.name = "Tom"; // setter: Tom
// console.log(temp.name); // Jack

// class Animal {
//     public _name;
//     constructor(name: string) {
//         this._name = name;
//     }
//     sayHi() {
//         return `My name is ${this._name}`;
//     }
//     static sayHello() {
//         return "Hello, 你好呀！";
//     }
// }
// let temp = new Animal("Jack");
// console.log(temp.sayHi()); // My name is Jack
// console.log(Animal.sayHello()); // Hello, 你好呀！

// class Animal {
//     protected name: string;
//     public constructor(name: string) {
//         this.name = name;
//     }
// }
// class Cat extends Animal {
//     constructor(name: string) {
//         super(name);
//         console.log(this.name);
//     }
// }

// abstract class Animal {
//     public name: string;
//     public constructor(name: string) {
//         this.name = name;
//     }
//     public abstract sayHi(): void;
// }
// // let temp = new Animal("Jack"); // 无法实例化抽象类
// class Cat extends Animal {
//     public eat() {
//         console.log(`${this.name} is eating.`);
//     }

//     public sayHi(): void {
//         console.log(`My name is ${this.name}`);
//     }
// }
// let cat = new Cat("Tom");
// cat.sayHi();
// cat.eat();

// function createArray<T>(length: number, value: T): Array<T> {
//     let result: T[] = [];
//     for (let index = 0; index < length; index++) {
//         result[index] = value;
//     }
//     return result;
// }
// createArray<string>(3, "X");
// createArray(6, "Y");

// function swap<T, U>(tuple: [T, U]): [U, T] {
//     return [tuple[1], tuple[0]];
// }
// swap([7, "Seven"]); // ["Seven", 7]

// interface Lengthwise {
//     length: number;
// }
// // 约束了泛型 T 必需符合接口 Lengthwise 的形状，也就是必需包含 length 属性
// function loggingIdentity<T extends Lengthwise>(arg: T) {
//     console.log(arg.length);
//     return arg;
// }
// // 如此调用会报错
// // loggingIdentity(7);

// interface CreateArrayFunc {
//     <T>(length: number, value: T): Array<T>;
// }
// let createArray: CreateArrayFunc;
// createArray = function <T>(length: number, value: T): Array<T> {
//     let result: T[] = [];
//     for (let index = 0; index < length; index++) {
//         result[index] = value;
//     }
//     return result;
// }
// createArray(6, "Z");

// interface CreateArrayFunc1<T> {
//     (length: number, value: T): Array<T>;
// }

// class GenericNumber<T>{
//     zeroValue: T;
//     add: (x: T, y: T) => T;
// }
// let myGenericNumber = new GenericNumber<number>();
// myGenericNumber.zeroValue = 0;
// myGenericNumber.add = function (x, y) { return x + y; }

function createArray<T = string>(length: number, value: T): Array<T> {
    let result: T[] = [];
    for (let index = 0; index < length; index++) {
        result[index] = value;
    }
    return result;
}