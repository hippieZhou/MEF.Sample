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

- 0-Docs

> 该层主要用于添加一些和该项目项目的一些描述性文档内容

- 1-Base

> 该层主要用于添加一些该框架的核心操作，比如 **Domain**、**Infrastructure**、**CrossCutting** 相关抽象和实现。在使用过程中请更加具体的业务场景并参考 **TDD（领域模型设计）** 来编码。

- 2-Modules

> 该层主要用于添加一些模块的业务查询，每个插件的命名规范请严格保存一致，模块之间不允许出现相互引用的情况，但可以引用该层以下的相关服务。

- 3-BlankApp

> 该项目作为主程序，主要负责全局容器的管理和维护操作，针对全局性的抽象基础配置初始化相关操作请在主程序进行相关实现。


## 编码规范

- 为了让代码更具可维护性，请确保同一层级的项目不要直接相互引用，如果同级直接需要业务共享，请尝试使用 **依赖注入** 或 **时间聚合器** 的方式来进行传递
- 尽可能少的使用 **new** 关键字，如需使用服务请严格使用 **依赖注入** 的方式来获取相应服务
- 各模块之间应严格遵守同一套编码规范，避免风格差异

## 相关说明

## 相关参考

- https://github.com/PrismLibrary
- https://github.com/PrismLibrary/Prism-Samples-Wpf
- https://github.com/brianlagunas/PrismOutlook
