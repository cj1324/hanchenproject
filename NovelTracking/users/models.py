#!usr/bin/python
#-*- coding:utf-8 -*-
# Django settings for NovelTracking project.

from django.db import models

# Create your models here.
class Members(models.Model):
    f_name=models.CharField(max_length=100) #用户名
    f_email=models.EmailField()             #你的email
    f_passwd=models.CharField(max_length=32) #密码
    f_nickname=models.CharField(max_length=20) #昵称 
    f_headpic=models.CharField(max_length=200) #用户头象
    f_mobile=models.CharField(max_length=20)  #手机
    f_key=models.CharField(max_length=50)    #邮箱认证key
    f_isadmin=models.IntegerField(default=0)  #是否是管理员
    f_level=models.IntegerField(default=1)   #等级
    f_state=models.IntegerField(default=1)   #状态
    f_regip=models.CharField(max_length=20)  #注册IP
    f_create=models.DateTimeField(auto_now_add=True) #注册时间 
    f_delete=models.BooleanField(default=False)    #是否可用

    
