#!usr/bin/python
#-*- coding:utf-8 -*-
#filename gaebookpj/models.py  
#  GAE的ORM框架的模型层 同时也是用来定义表
# 
#
''' GAE的模型层 采用GQL来实现 '''

__author__="寒晨"	 	#作者
__version__="1.0" 		#版本
__date__="2010.10.4"	#日期
__copyright__="寒晨所有&2010"#授权
__license__="Python"	#证书

import os

from google.appengine.ext import db


class Fictions(db.Model):
    """  保存小说信息表  """

    #小说ID 
    F_Id=db.IntegerProperty(default=0)
    
    # 小说官方ID
    F_OffId=db.IntegerProperty(default=0)

    #小说拥有者
    F_User=db.UserProperty(auto_current_user=True)

    #小说名
    F_Name=db.StringProperty(multiline=False)
    
    #小说作者
    F_Author=db.StringProperty(multiline=False)
    
    #小说排序
    F_Order=db.IntegerProperty(default=0)
    
    #小说类别
    F_Class=db.StringListProperty(multiline=False)
    
    #小说章节
    #F_Chapter=db.StringProperty(multiline=False)
    
    #小说最后更新时间
    F_UpdateTime=db.DateTimeProperty(auto_now_add=True)
    
    #小说添加时间
    F_AddTime=db.DateTimeProperty(auto_now_add=True)
    
    #是否关注该小说
    F_IsAttention=db.BooleanProperty(required=True,default=True)
    
    #小说是否已读
    F_IsRead=db.BooleanProperty(required=True,default=True)


    
    def __init__(self, parent=None, key_name=None, app=None, **entity_values):
        """ 小说模型层类 初始化 方法 """
        db.Model.__init__(self, parent, key_name, app, **entity_values)


    @proprety
    def getNotRead(self):
        return self.gql("where F_IsRead=:1",False)


class Directory(db.Model):
    """ 小说章节表 """
    #小说章节ID
#    F_Id=db.IntegerProperty(required=True,default=0)

    #官方章节ID
    F_OffId=db.IntegerProperty(required=True,default=0)

    #章节名称
    F_Name=db.StringProperty(required=True,multiline=False)

    #小说ID
#    F_FicId=db.IntegerProperty(required=True,default=0)

    #小说对象的引用
    F_Fiction=db.ReferenceProperty(Fictions)

    #小说官方地址
    F_Url=db.StringProperty(required=True,multiline=False)

    #小说更新时间
    F_UpdateTime=db.DateTimeProperty(required=True,auto_now_add=True)

    #添加时间
    F_AddTime=db.DateTimeProperty(required=True,auto_now_add=True)


    def __init__(self, parent=None, key_name=None, app=None, **entity_values):
        """ 小说章节模型层类 初始化 方法 """
        db.Model.__init__(self, parent, key_name, app, **entity_values)



class Config(db.Model):
    """ 程序配置项的存储表 """
    
    #配置项ID
#   F_Id=db.IntegerProperty(required=True,default=0)
    
    #配置项属性名
    F_Name=db.StringProperty(required=True,multiline=False)

    #配置项的值
    F_Value=db.StringProperty(required=True,multiline=False)

    #配置项的默认值
    F_Default=db.StringProperty(required=True,multiline=False)

    #配置项的备注
    F_Memo=db.StringProperty()

    def __init__(self, parent=None, key_name=None, app=None, **entity_values):
        """ 小说章节模型层类 初始化 方法 """
        db.Model.__init__(self, parent, key_name, app, **entity_values)


class Logo(db.Model):
    """ 存储系统日志 """

    #存储的日志ID
#    F_Id=db.IntegerProperty(required=True,default=0)
    
    #日志类型
    F_Class=db.StringProperty(required=True,multiline=False)

    #日志信息
    F_Info=db.StringProperty(required=True,multiline=False)

    #日志添加时间
    F_AddTime=db.DateTimeProperty(required=True,auto_now_add=True)

    def __init__(self, parent=None, key_name=None, app=None, **entity_values):
        """ 小说章节模型层类 初始化 方法 """
        db.Model.__init__(self, parent, key_name, app, **entity_values)

class AboutWeb(db.Model):
    """  相关网站 """
    
    #相关网站ID
#    F_Id=db.IntegerProperty(required=True,default=0)
    
    #相关网站的名称
    F_Name=db.StringProperty(required=True,multiline=False)

    #相关网站URL
    F_Url=db.StringProperty(required=True,multiline=False)

    #相关小说备注
    F_Memo=db.StringProperty(required=True,multiline=False)

    #该地址的添加时间
    F_AddTime=db.DateTimeProperty(required=True,auto_now_add=True)

    def __init__(self, parent=None, key_name=None, app=None, **entity_values):
        """ 小说章节模型层类 初始化 方法 """
        db.Model.__init__(self, parent, key_name, app, **entity_values)


if __name__=="__main__":

