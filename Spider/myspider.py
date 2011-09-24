#!/usr/bin/python
#-*- coding:utf-8 -*-
# edit:vim 7.3  
# system:fedora 14 
# python:python 2.7
# starttime: 11-6-4
''' 爬虫文件的主脚本 '''

#{{{ 导入脚本
import optparse
import setting #导入配置文件
import os
import sys
import re
import datetime
import urllib2
import logging
import sqlite3
import threading
import Queue
#}}}

#{{{全局变量
options={}; #处理后的args参数
hostname=None  #有效的主机名（限定采集范围）
fetchlist=[] #以采集的列表（控制重复采集）
insertcount=0 #记录成功插入记录条数
#}}}

#{{{ 程序参数定义和处理模块
def argsper(): #进行参数的转换处理 
    """

    """
    usage = u"usage: %prog  -u url [options]" 
    parser=optparse.OptionParser(usage)
    parser.add_option("-u","--url",dest="url",help=u"所采集的网址地址",default=setting.URL)
    parser.add_option("-d","--depth",dest="depth",type="int",help=u"采集深度,数值类型",default=setting.DEPTH)
    parser.add_option("-t","--thread",dest="thread",type="int",help=u"并发的线程,数值类型",default=setting.THREAD)
    parser.add_option("-l","--log",dest="logfile",help=u"定制日志文件,文件名",default=setting.LOGFILE)
    parser.add_option("-v",dest="loglevel",type="int",help=u"日志的详细程序,数值类型 0-4,其中0最详细 4安静无日志输出",default=setting.LOGLEVEL)
    global options
    (options, args) = parser.parse_args()
#}}}
    
#{{{ 初始化 和配置日志logging模块
def logconf(): #创建日志对象
    """

    """
    logconfig={}
    logconfig["filename"]=os.path.join(os.getcwd(),"%s.%s"%(options.logfile,setting.LOGFILEEX))
    logconfig["filemode"]=setting.LOGMODE
    logconfig["format"]=setting.LOGFORMAT
    logconfig["level"]= (options.loglevel*10);
    logging.basicConfig(**logconfig)
    #输出到控制台
    if setting.CONSOLE:
        console = logging.StreamHandler()
        console.setLevel(options.loglevel*10)
        formatter = logging.Formatter(u'%(levelname)-8s %(filename)s[line:%(lineno)d] %(message)s')
        console.setFormatter(formatter)
        logging.getLogger('').addHandler(console)
    logging.debug(u"logging init ok!!")
#}}}

#{{{ 过滤html空行和编码
def htmlfilter(lines,code):#过滤html空行和编码
    """

    """
    logging.debug(u"开始过滤采集的数据行数为%d编码为%s"%(len(lines),code))
    ret_lines=[]
    for line in lines:
        line=line.strip()
        if line=='':
            continue
        if code!='' and code!='utf-8':
            try:
                line=line.decode(code).encode('utf-8') 
            except:
                logging.warn(u"解析页面内容编码失败!")
                continue
        line= re.sub('[ ]+',' ',line);
        ret_lines.append(line);
    return ret_lines
#}}}

#{{{ 页面的html行进行正则匹配 取出所有链接
def htmlgetallhref(lines,host):#匹配链接
    """

    """
    ret_lines=[]
    logging.debug(u"开始匹配所有链接")
    content=''.join(lines)
    SEARCHRE="href=['\"]?([^'\"<>]+)['\"]?"
    logging.debug(u"开始匹配：%s"%( SEARCHRE))
    matchs=re.findall(SEARCHRE,content)
    if matchs:
         logging.debug(u"匹配了%d个链接!"%(len(matchs)))
    else:
        logging.warn( u"没有任何匹配的链接！")
    for  url in matchs:
        if url[0:1]=="/":
            url="http://%s%s"%(host,url)
        if url[0:7]=="http://":
            pass
        else:
            continue
        try:
            logging.debug(u"解析出来的URL<<%s>>"%(str(url)))
        except:
            logging.warn(u"解析出来的URL编码异常,地址无效")
            continue
        ret_lines.append(url)
        ret_lines=list(set(ret_lines)) #去掉重复地址
    return ret_lines
        
#}}}

#{{{   抓取页面的方法 
def fetch(uri):#url获取请求数据
    """

    """
    uri=peruri(uri)
    lines_orig=[]
    logging.debug(u"准备采集url:%s"%(uri))
    request = urllib2.Request(uri)
    if setting.UA:
        request.add_header('User-Agent',setting.UA)
    if setting.REFERER:
        request.add_header('Referer',setting.REFERER)

    #判断重复采集
    global fetchlist
    try:
        fetchlist.index(uri)
        logging.debug(u"已经存在该地址，不进行重复采集:%s"%(uri))
        return None
    except: #异常则说明不存在该地址,可以采集
        fetchlist.append(uri)
    try:    
        response = urllib2.urlopen(request)
        try:
            code=re.search('charset=([^\\r\\n]+)',str(response.info())).group(1)
        except:
            logging.debug(u"没有在headers中采集到网页编码")
        lines_orig=response.readlines()
        try:
            code=re.search('charset=([^\\r\\n"\' ]+)','\r\n'.join(lines_orig)).group(1)
        except:
            code=setting.DEFAULTDECODING
            logging.debug(u"没有在html中采集到网页编码")
        lines=htmlfilter(lines_orig,code)
        return lines
    except urllib2.HTTPError, e: 
        logging.warn(u"采集%s失败状态码%d,MSG:%s"%(uri,e.code,str(e)))
        return None
    except  Exception, ex:
        logging.warn(u"采集超时.请检查地址和网络！,根据网络情况可以配置延长超时时间!MSG:%s"%(str(ex)))
        return None

#处理缺少协议的地址
def peruri(uri):
    if len(uri)>7 and uri[0:7]!="http://":
        logging.debug(u"开始过滤uri,加入协议头")
        uri="http://"+uri
    if len(uri)<7:
        logging.debug(u"开始过滤uri,加入协议头")
        uri="http://"+uri
    return uri

#}}}

#{{{获取某个URL主机名
def gethost(url):
    request = urllib2.Request(url)
    return request.get_host()

def getmainhost(url):
    mainhost=gethost(url)
    lsstr=mainhost.split('.');
    mainhost='.'.join(lsstr[1:])
    return mainhost
#}}}

#{{{ 插入到sqlite3数据库方法
def insertdb(depth,url,lines):
    logging.debug(u"插入深度%d地址:%s的内容到数据库"%(depth,url))
    con = sqlite3.connect(setting.DBFILE)
    con.text_factory = str
    cur = con.cursor()
    SQL="insert into  twebpage values(null,?,?,?,datetime('now'))"
    try:
        cur.execute(SQL,(depth,url,"".join(lines)))
        global insertcount
        insertcount+=1
    except sqlite3.IntegrityError, e:
        logging.warn(u"插入数据库失败%s url:%s"%(str(e),url))
    con.commit()
    con.close()
#}}}    

#{{{ 多线程实现类
class FetchThread(threading.Thread):
    def __init__(self,q,tn,mutex):
        self.queue=q
        self.mutex=mutex
        threading.Thread.__init__(self,name=(u" thead<%d> "%(tn)))
    def run(self):
        logging.info(u"线程:%s 启动"%(self.getName()))
        global hostname
        while True:
            if self.queue.qsize()>0:
                self.mutex.acquire()#线程锁
                job=self.queue.get()
                self.mutex.release()
                logging.info(u"线程:%s 队列长度:%d 深度:%d GET PAGE url:%s"%(self.getName(),self.queue.qsize(),job["depth"],job["url"]))
                lines=fetch(job["url"])
                if lines:
                    logging.info(u"线程:%s 队列长度:%d 深度:%d TO DBASE url:%s"%(self.getName(),self.queue.qsize(),job["depth"],job["url"]))
                    self.mutex.acquire()#线程锁V
                    insertdb(job["depth"],job["url"],lines)
                    self.mutex.release()
                    logging.debug(u"curr depth %d  option depth %d  url:%s"%(job["depth"],options.depth,job["url"]))
                    if job["depth"]<options.depth:
                        urls=htmlgetallhref(lines,job["host"])
                        self.mutex.acquire()
                        for u in urls:
                            if hostname == getmainhost(u):
                                logging.debug(u"线程:%s 队列长度:%d 深度:%d Add Queue  url:%s"\
                                        %(self.getName(),self.queue.qsize(),(job["depth"]+1),u))
                                self.queue.put({"depth":(job["depth"]+1),"url":u,"host":gethost(u)})
                            else:
                                logging.debug(u"该地址不属于%s主机,不进行采集 地址:%s"%(hostname,u))
                        self.mutex.release()
                else:
                    logging.info(u"线程:%s 深度:%d Failure url:%s"%(self.getName(),job["depth"],job["url"]))
            else:
                break
        logging.info(u"线程:%s 结束!"%(self.getName()))
#}}}

#{{{ 入口main方法
def main(): #入口函数
    """

    """
    argsper()
    if(options.url==None):
        print u"没有找到采集地址请 运行myspider.py -h 查看帮助,或配置setting.py"
        exit()#退出
    logconf()
    logging.debug(u"输入参数:%s"%(sys.argv));
    logging.debug(u"选项:%s"%(str(options))) 
    logging.debug(u"开设设置链接超时时间！")
    urllib2.socket.setdefaulttimeout(setting.DEFAULTTIMEOUT)
    sttime=datetime.datetime.now()
    global  hostname
    hostname=getmainhost(peruri(options.url))
    logging.info(u"可采集的地址为:http://*.%s/*"%(hostname))
    logging.debug(u"开始采集首页")
    lines=fetch(options.url)
    if lines:
        logging.info(u"采集首页成功！")
        lists=htmlgetallhref(lines,gethost(peruri(options.url)))
        insertdb(0,options.url,lines)
        que= Queue.Queue(0);
        mutex = threading.Lock()#创建锁
        for x  in lists:
            if hostname==getmainhost(x):
                logging.debug(u"深度1采集地址:%s 加入队列"%(x))
                que.put({'depth':1,'url':x,'host':gethost(x)})
            else:
                logging.debug(u"该地址不属于%s主机,不进行采集 地址:%s"%(hostname,x))
        threads=[]
        for t in range(options.thread):
            currth=FetchThread(que,t,mutex)
            currth.start()
            threads.append(currth)
        for th in threads:
            th.join()
        entime=datetime.datetime.now()
        span=(sttime-entime).seconds
        logging.info(u"程序运行时间为%d秒,共采集%d个页面,成功插入%d条数据"%(span,len(fetchlist),insertcount))
        exit()
    else:
        logging.warn(u"没有采集到首页退出")
        exit()

#}}}

if __name__=="__main__":
    main()

