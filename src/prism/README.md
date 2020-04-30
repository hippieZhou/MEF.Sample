<div align="center">

# 基于 Prism 框架进行的系统架构设计

</div>

## 环境安装

改示例框架是基于 **.NET Framework** 来进行开发的，Prism 版本可以自由选择，单需要确保整个项目的版本保存一致。

为了提高开发消息，统一编码风格，团队开发人员请安装如下 Visual Studio 插件

- [Prism Template Pack](https://github.com/PrismLibrary/Prism)
- [XamlStyler](https://github.com/Xavalon/XamlStyler/)

> 建议统一使用 Visual Studio 2019 版本

## 使用介绍

该实例项目参考目前流行的开发模式：**DDD** 来进行设计，最大限度使用了 Prism 的内置功能。

- 0-Docs

> 该层主要用于添加一些和该项目项目的一些描述性文档内容

- 1-Domain

> 领域层，主要存放一些领域模型；

 - 2-Application

> 应用层，主要存放一些公共业务的抽象和实现，供上层提供相应业务服务；

- 3-Infrastructure

> 基础设施层，主要存放一些和基础设施相关的操作（具体实现）；

- 3-CrossCutting

> 横切面层，主要处理一些切面业务的相关操作（具体实现）；

- 4-Modules

> 该层中的每个项目都为一个相互独立的业务子模块，要求子模块之间不允许出现相互引用的情况，但可以引用该层以下的相关服务。

- 5-BlankApp

> 该项目作为主程序，主要负责全局容器的管理和维护操作，针对全局性的抽象基础配置初始化相关操作请在主程序进行相关实现。

## 编码规范

- 为了让代码更具可维护性，请确保同一层级的项目不要直接相互引用，如果同级直接需要业务共享，请尝试使用 **依赖注入** 或 **事件聚合器** 的方式来进行传递
- 尽可能少的使用 **new** 关键字，如需使用服务请严格使用 **依赖注入** 的方式来获取相应服务
- 各模块之间应严格遵守同一套编码规范，避免风格差异

## 相关说明

## 相关参考

- https://github.com/PrismLibrary
- https://github.com/PrismLibrary/Prism-Samples-Wpf
- https://github.com/brianlagunas/PrismOutlook
