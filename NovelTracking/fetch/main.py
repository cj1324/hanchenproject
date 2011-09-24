#!usr/bin/python
#-*- coding:utf-8 -*-
# Django settings for NovelTracking project.


__author__="寒晨"         #作者
__date__="2011.3.14"    #日期
__copyright__="寒晨所有&2010"#授权
__license__="Python 2.7"    #证书

import re
import urllib

from NovelTracking.fetch.models import Regular
from NovelTracking.fetch.models import Rules

def fetch_main(url):
    '''获取数据入口'''
    web=urllib.urlopen(url)
    code=re.search('charset=([^\\r\\n]+)',str(web.info())).group(1)
    lines_orig=web.readlines()
    lines=filter_main(lines_orig,code)
    cont='\r\n'.join(lines)# 如果 join lines_org 输出不乱码
    return cont

def filter_main(lines,code):
    '''
    进行去空行 和空格处理
    '''
    ret_lines=[]
    for line in lines:
        line=line.strip()
        if line=='':
            continue
        if code!='' and code!='utf-8':
            try:
                line=line.decode(code).encode('utf-8') 
            except:
                line='decode Error:'+line
        line= re.sub('[ ]+',' ',line);
        ret_lines.append(line);
    return ret_lines

def startcorn():
    '''
    开始定时任务
    '''
        
    return

def start_main():
    '''
    开始获取数据和收集数据到数据库
    '''
    
if __name__=='__main__':
    fetch_main()
