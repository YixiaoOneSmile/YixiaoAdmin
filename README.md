### 5 分钟快速创建应用后台管理系统

欢迎使用快速开发脚手架 YixiaoAdmin，通过阅读以下内容，你可以快速熟悉 YixiaoAdmin，并用极短的时间快速创建属于自己应用的后台管理系统。

### 关于 YixiaoAdmin

YixiaoAdmin 是一套基于 Vue2.0、.Net Core6.0 的轻量级前后端分离的后台管理系统脚手架。其中.Net Core6.0 部分集成了 ORM 框架 EFCore、身份认证框架 IdentityServer4、接口文档框架 Swagger、Excel 操作框架 EPPlus，并且通过 T4 模板快速生成相关代码与接口，减少重复工作，极大简化了开发过程，有效缩短开发周期；前端采用 Vue2.0，集成了 Vuex、ElementUI、Echart，相关增删改页面均可由代码生成器生成。

### 环境依赖

前端依赖于 Node.Js 16.18 环境（建议使用 NVM 管理 Node 版本）
后台依赖于 .NetCore 6.0

### 目录结构说明

### 快速使用

#### 0.初始化项目

进入项目根目录
进入 src/vue 目录
初始化前端目录，使用命令，npm i

#### 1.修改项目名称

修改代码仓库名称
修改后端代码解决方案名称

#### 2.配置数据库连接字符串

打开 YixiaoAdmin.WebApi/appsettings.json 文件
将"YixiaoAdminDatabase"的值修改为新的连接字符串

打开 YixiaoAdmin.EntityFrameworkCore/YixiaoAdminContextFactory.cs
将 ConnectString 变量的值修改为与"YixiaoAdminDatabase"相同的连接字符串

#### 3.创建实体

在 YixiaoAdmin.Models 中创建公有实体类并继承于 Entity 实体

在 YixiaoAdmin.EntityFrameworkCore/YixiaoAdminContext.cs 文件中添加该实体（DbSet）

#### 4.迁移数据库

&emsp;进入程序包管理控制台，切换至 YixiaoAdmin.EntityFrameworkCore 项目

&emsp;创建迁移， 输入命令 add-migration [你的迁移名字]

&emsp;启动迁移，输入命令 update-database

#### 5.转换所有 T4 模板

&emsp;在 Visual Studio 左侧顶部工具栏中选择生成->转换所有 T4 模板

#### 6.将 CodeHelper 生成代码剪切至对应位置

&emsp;进入 Common/YixiaoAdmin.CodeHelper 项目

&emsp;将 T4Controllers 中所有[你的实体名字]Controller.cs 文件剪切至 YixiaoAdmin.WebApi

&emsp;将 T4Dtos 中所有[你的实体名字]DTO.cs 文件剪切至 YixiaoAdmin.Models/ViewModels

&emsp;将 T4IServices 中所有 I[你的实体名字]Services.cs 文件剪切至 YixiaoAdmin.IServices

&emsp;将 T4Services 中所有[你的实体名字]Services.cs 文件剪切至 YixiaoAdmin.Services

#### 7.配置 ViewModel

&emsp;进入 YixiaoAdmin.Models/ViewModels 目录

&emsp;配置所有的[你的实体名字]DTO.cs 文件

&emsp;进入文件，将需要渲染在页面上的实体属性加入 [Description("[你的实体属性的名字]")]

&emsp;重新生成 YixiaoAdmin.Models 项目

#### 8.配置前端界面

&emsp;进入 Common/YixiaoAdmin.CodeHelper/T4Views 目录

&emsp;双击 ViewTemplate.tt 进入后 ctrl+S 执行 T4 模板

&emsp;将新生成的.Vue 文件移动到 vue/src/views 下

&emsp;进入 Common/YixiaoAdmin.CodeHelper/T4Views/Axios 目录

&emsp;双击 api.tt 进入后 ctrl+S 执行 T4 模板

&emsp;展开 api.tt 进入 api.js 将 T4 模板生成代码复制至 vue/src/api/api.js 中的指定位置

&emsp;进入 Common/YixiaoAdmin.CodeHelper/T4Views/Vue-Router 目录

&emsp;双击 index.tt 进入后 ctrl+S 执行 T4 模板

&emsp;展开 index.tt 进入 index.js 将 T4 模板生成代码复制至 vue/src/router/index.js 中的指定位置

#### 7.启动项目

前端 cd src/vue
npm run serve

后端使用 VS 运行 YixiaoAdmin.WebAPI 项目

### 更多

YixiaoAdmin 尚处于不稳定阶段，希望框架使用者能够不断参与项目的优化
