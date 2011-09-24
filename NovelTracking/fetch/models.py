#!usr/bin/python
#-*- coding:utf-8 -*-
# Django settings for NovelTracking project.


__author__="寒晨"         #作者
__date__="2011.3.14"    #日期
__copyright__="寒晨所有&2010"#授权
__license__="Python 2.7"    #证书
from django.db import models
from NovelTracking.novel.models import Sites 

class Rules(models.Model):
    '''
    采集规则
    '''

    f_tager=models.CharField(max_length=500) #采集的目标地址
    f_regular=models.TextField()            #采集的正则规则 (确定采集范围)
    f_web=models.ForeignKey(Sites)          #所采集的网站
    f_rmarket=models.TextField()            #所采集的网站的备注
    f_timer=models.IntegerField(default=10) #采集的间隔时间
    f_params=models.TextField()             #采集需要的参数
    f_last=models.DateTimeField(auto_now=True)  #最后的采集时间
    f_create=models.DateTimeField(auto_now_add=True)   #采集规则的创建时间
    f_delete=models.BooleanField(default=False)     #采集规则是否已经删除


class Regular(models.Model):
    '''
    采集规则实际的正则 
    '''
    f_rules=models.ForeignKey(Rules)
    f_name=models.CharField(max_length=200)
    f_regu=models.CharField(max_length=500)





