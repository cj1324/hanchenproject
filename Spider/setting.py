#!/usr/bin/python
#-*- coding:utf-8 -*-
# edit:vim 7.3  
# system:fedora 14 
# starttime: 11-6-4
''' 配置文件 '''

#采集的配置

URL=None  #默认采集的地址 如果为None 就必须有参数
DEPTH=2  #默认的递归深度
THREAD=2   #默认的进程并发数

DEFAULTDECODING='utf-8' #假如没有采集到网页编码默认解析该设置项编码

#请求头的配置
UA='Mozilla/5.0 (X11; Linux i686) AppleWebKit/534.30 (KHTML, like Gecko) Chrome/12.0.742.77 Safari/534.30'    
#配置浏览器User-Agent (这里是linux+chrome 12.0.742.77 beta )
REFERER=''  #配置REFERER
DEFAULTTIMEOUT=20 #设置请求的超时时间

#日志的配置
LOGFILE='logfile'  #默认的日志文件名
LOGFILEEX='log'   #日志文件的扩展名
LOGLEVEL=2   #记录日志的级别 
LOGMODE='w'  #日志模式 w每次运行都重新写。 a 追加
LOGFORMAT= '%(asctime)s %(levelname)s %(message)s'
CONSOLE=True #控制台输出日志
#数据库的配置

DBFILE='sqlite.db'








