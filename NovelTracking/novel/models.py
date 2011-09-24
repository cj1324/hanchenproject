#!usr/bin/python
#-*- coding:utf-8 -*-
#  About Novel Models


__author__="寒晨"         #作者
__date__="2011.3.14"    #日期
__copyright__="寒晨所有&2010"#授权
__license__="Python2.7"    #证书

from django.db import models



class Sites(models.Model):
    '''小说网站'''

    f_name=models.CharField(max_length=100) #小说网站名称
    f_url=models.URLField(max_length=500)   #网站首页地址
    f_remark=models.TextField()             #网站备注
    f_praise=models.IntegerField(default=0)      #用户推荐
    f_create=models.DateTimeField(auto_now_add=True) #添加记录时间
    f_delete=models.BooleanField(default=False)  #记录是否已删除


class Genres(models.Model):
    '''
    小说类型
    '''

    f_name=models.CharField(max_length=10)  #小说类型名
    f_remark=models.TextField()     #小说类型备注
    

class Novels(models.Model):
    '''
    小说信息
    '''

    f_offid=models.IntegerField(null=False) #官方ID
    f_name=models.CharField(max_length=100)           #小说名
    f_auther=models.CharField(max_length=20,default='匿名')          #小说作者
    f_intro=models.TextField()                          #小说简介
    f_level=models.IntegerField(blank=True,null=True) #小说等级（排序）
    f_published=models.ForeignKey(Sites)       #小说发布站
    f_type=models.ManyToManyField(Genres)            #类型(多对多)
    f_click=models.IntegerField(default=1)  #点击数
    f_home=models.BooleanField() #首页推荐
    f_create=models.DateTimeField(auto_now_add=True)  #创建时间
    f_delete=models.BooleanField() #是否已删除

class Chapters(models.Model):
    ''' 小说章节模型'''
    
    f_offid=models.IntegerField(null=True)  #官方章节ID
    f_name=models.CharField(max_length=100) #章节名称
    f_novel=models.ForeignKey(Novels)       #小说(外键)
    f_offurl=models.URLField(max_length=500)#官方URL
    f_create=models.DateTimeField(auto_now_add=True)#创建时间
    f_delete=models.BooleanField() #是否已删除

class Piracys(Sites):
    '''
    盗版小说网站 指定小说目录
    '''

    f_novelurl=models.URLField()  #指定小说目录
    f_novel=models.ForeignKey(Novels)   #小说（外键）
    f_click=models.IntegerField(default=0) #点击数
    f_recom=models.IntegerField(default=0)#推荐
    
class Tops(models.Model):
    '''
    小说各类排行榜！
    '''

    f_name=models.CharField(max_length=50) #排行版名称\
    f_click=models.IntegerField(default=0) #点击数
    f_remark=models.TextField()  #排行榜的备注
    f_phase=models.IntegerField() #排行榜最近期数
    f_update=models.DateTimeField(auto_now=True) #排行榜更新时间
    f_create=models.DateTimeField(auto_now_add=True) #排行版创建时间
    f_delete=models.BooleanField(default=False) #该排行是否已经删除

class Ranks(models.Model):
    '''
    各类排行榜的排名关联小说
    '''

    f_novel=models.ForeignKey(Novels)   #小说 （外键）
    f_top=models.ForeignKey(Tops)       #排行榜
    f_phase=models.IntegerField()       #期数
    f_num=models.IntegerField()         #排名
