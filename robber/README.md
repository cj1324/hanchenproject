盗贼是一个模块化的Web页面爬虫
=============================

目录规划
--------
* collect 收集模块

  * 爬虫开始爬虫或爬虫运行中进行收集URL
  * 支持filter过滤器.
  * 分3个类别
  * threading
    * 模块开始时只运行一次 有默认值.处理配置入参
    * 创建单独线程一直运行(整个进程不主动退出)
    * 当request结果响应后处理 每个结果一次

  * 限制爬取范围

    * URL去重 (get参数变化处理)
    * URL最大数限制
    * URL域范围限制
    * URL爬取深度限制
    * 协议限制,仅支持http

* request 基于多线程模型

  * 包裹urllib2进行获取页面内容
  * 由于网络情况这里可能发生卡死
  * 考虑多处超时，超量 第一版本不实现
  * 考虑登录 第一版本不实现
  * 主要处理urldata模型

* storage 存储模块 
> 写数据库模块 

* filter 过滤模块
> 需要导入，有顺序要求，如果需要参数由调用方的收集或存储模块提供

  * collect.filter 收集模块的过滤器
  * storage.filter 存储模块的过滤器
  * common.filter 通用的过滤器


* engine 引擎模块

  * 负责协调各个模块工作

* model 模型数据

  * urldata 关键数据模型

* conf 配置目录

  * 爬虫运行必须的配置

* test 自动化测试用例

  * 第一版本应该会有少量关键用例

* util 工具函数目录

  * 处理日志参数
  * 命令行参数
  * yaml配置文件解析
  * Thread的基本封装
  * url一些正则校验

* steal 命令行入口

  * 直接运行的程序


依赖的模块
----------
* python2.7 

  * threading
  * urllib2
  * unittest

* chardet
  * 检测web页面编码

* pyyaml
  * 配置文件格式 ,简单存储格式
  * 可读性比json好很多

* HTTPretty (未启用)

  * 配合unittest 进行http request方面的测试

* rpyc (未启用)

  * 性能分析
  * 辅助调试


* 其他依赖根据配置模块决定

执行测试用例
------------
> 在爬虫根目录中执行
> 目前只有http request模块有部分测试用例

``python -m unittest test``

> 或指定某TestCase

``python -m unittest test.xxxxxTestCase``

配置文件demo
------------
* 见 conf/default.yaml




ChangeLog
---------
* [X] 实现命令行参数处理
* [X] 编写日志模块配置
* [X] 配置文件结构定义和处理
* [X] 设计和编写filter部分demo代码
* [X] 设计和编写collect部分demo代码
* [X] 设计和编写CollectMange类
* [X] 简单从yaml配置到queue走通
* [ ] 设计和编写request部分demo代码
* [ ] 设计和编写storage部分demo代码
* [ ] 设计和编写engine部分代码
* [ ] 第一阶段测试运行
* [ ] 发布0.1版本
