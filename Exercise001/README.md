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
