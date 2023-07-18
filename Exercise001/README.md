# 朝夕教育

R19-2023年4月录制Vue3+.NET7电商管理后台---VIP--完整53P

## 开发环境

- 操作系统： Windows
- 开发工具： Visual Studio 2022 & Visual Studio Code
- Node.js 版本： 18+
- 包管理器： npm 9.3.1 & pnpm 7.16.1(选装)
- 前端框架： Vue3.2
- 后端框架： .NET7
- 数据服务： MySQL

## 技术栈

- 前端体系： Vue3, Vite, TypeScript, Sass, Router, Element Plus, Axios, Pinia
- 后端体系： .NET7, WebAPI, SqlSugar, Autofac, JWT

## 后端项目

### 项目分层、数据库设计

- Class Library (Model, Interface, Service)
- 数据库表 (Log4Net, Menu, MenuRoleRelation, Role, UserRoleRelation, Users)

### Autofac 的使用

目的： 批量注册  
用以替换内置 IOC  
安装 NuGet 包： Autofac, Autofac.Extensions.DependencyInjection

### Automapper 的安装和配置

作用： DTO 与实体之间的映射，替代手工赋值  
安装 NuGet 包： AutoMapper, AutoMapper.Extensions.Microsoft.DependencyInjection

### SqlSugar 的介绍和安装

安装 NuGet 包： SqlSugarCore

### CodeFirst 的实现

1. 根据字符串中的配置信息动态生成数据库
2. 通过反射生成表结构
3. 添加测试数据

### 创建 MySQL 数据库

```sql
-- sudo mysql -u root -p
-- 创建用户
CREATE USER 'ZhaoXiProjects'@'%' IDENTIFIED WITH mysql_native_password BY 'P@ssw0rd';
-- 创建数据库
CREATE DATABASE ZhaoXiExercise001;
-- 授予权限
GRANT ALL PRIVILEGES ON ZhaoXiExercise001.* TO 'ZhaoXiProjects'@'%';
```

### 使用 JWT 生成 Token

安装 NuGet 包： Microsoft.AspNetCore.Authentication.JwtBearer

```json
// C:\Z_Documents\ZhaoXiProjects\Exercise001\Zhaoxi.Shopping.Backend\ExeWebApi\appsettings.json
"JWTTokenOptions": {
  "Audience": "http://localhost:5148",
  "Issuer": "http://localhost:5148",
  "SecurityKey": "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB"
}
```

### JSON 格式化

安装 NuGet 包： Microsoft.AspNetCore.Mvc.NewtonsoftJson

```csharp
// C:\Z_Documents\ZhaoXiProjects\Exercise001\Zhaoxi.Shopping.Backend\ExeWebApi\Program.cs
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // 忽略循环引用
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    // 统一设置 API 日期格式
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // 设置 JSON 返回格式同 Model 一致 (默认 JSON 中的首字母为小写，这里修改为同后端 Model 一致)
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
```

### Swagger 配置

1. Authorization
2. SchemaFilter

### 文件上传服务设计

1. 多种上传场景分析
2. 策略 + 工厂模式的讲解和应用

### 本地上传功能实现

### 七牛云上传功能实现

注册云账号： `https://portal.qiniu.com/signup`  
个人中心获取密钥信息  
安装 SDK 编写上传逻辑
