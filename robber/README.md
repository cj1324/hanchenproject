盗贼是一个模块化的Web页面爬虫
=============================



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

目录规划
--------

> 见``Directory.md`` 

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
