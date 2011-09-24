#!usr/bin/python
#-*- coding:utf-8 -*-

from InterviewProject.testone.models  import Batchs,Details

#数据过滤方法
def filterstr(str_input):
    s=str_input.lower().replace('\r','').replace('\n','') #字符串过滤转换小写，过滤换行
    return s

#进行数据处理的方法
def calc(str_input):
    s=filterstr(str_input) #数据过滤
    maps={};
    for c in list(s): #遍历字符串
        c_i=ord(c)   #取字母编码
        if(maps.has_key(c_i)): 
            maps[c_i]+=1  #如字符存在个数+1
        else: 
            maps[c_i]=1  #不存在个数 =1
    todb(str_input,maps)
    return

#把数据插入 sqlite数据库
def todb(str_todb,maps):
    batch=Batchs()
    batch.text=str_todb
    batch.save()
    for s in maps.keys():
        detail=Details()
        detail.batch=batch
        detail.key=s
        detail.value=maps[s]
        detail.save()
    
