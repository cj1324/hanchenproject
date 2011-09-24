需求

编写一个爬虫：
myspider.py  -u www.chinaunix.net -d 3 -t 10 -l logfile -v log_level
-d标示递归深度
-t标示线程并发数
-v标示日志详细程度，越高越详细
-l 制定日志文件

将网页转为utf8编码，存入使用sqlite3数据库
使用logger模块记录爬虫日志


数据库脚本
建库脚本: sqlite3 sqlite3.db
建表脚本:
CREATE TABLE twebpage(id integer primary key,depth integer,url text,content text,created  datetime);



setting.py为项目的配置文件
myspider.py 为程序的主文件
myspider.py -h 查看帮助


