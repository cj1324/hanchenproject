#!usr/bin/python
#-*- coding:utf-8 -*-
# Django settings for NovelTracking project.



import re
import urllib

def fetch_main():
    '''获取数据入口'''

    web=urllib.urlopen("http://www.qidian.com")
    code=re.search('charset=([^\\r\\n]+)',str(web.info())).group(1)
    lines_orig=web.readlines()
    lines=filter_main(lines_orig,code)
    cont='\r\n'.join(lines)# 如果 join lines_org 输出不乱码
    print 'write!'
    f=open('./ps.txt','w')
    f.write(cont) #打开是乱码
    print cont  #直接输出不乱码。。。
    f.flush()
    f.close()
    print 'ok!'
    return

def filter_main(lines,code):
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


if __name__=='__main__':
    fetch_main()
