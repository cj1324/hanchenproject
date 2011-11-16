#-*- coding:utf-8 -*-

from django.db import models

# Create your models here.



class Book(models.Model):
    name=models.CharField(max_length=50) #书名
    isbn=models.CharField(max_length=20)  #ISBN码书号
    press=models.CharField(max_length=20) #出版社
    price=models.DecimalField(max_digits=5,decimal_places=2,null=True) #价格
    pubtime=models.DateField(null=True) #出版时间
    ebook=models.BooleanField(default=False) #是否有电子书(备用)
    number=models.IntegerField(default=1)  #共几本
    last=models.IntegerField(default=0)   #剩余多少本未借出
    author=models.CharField(max_length=20) #作者或译者
    tag=models.CharField(max_length=50,null=True)  #标签.多个用逗号隔开 (备用)
    recom=models.BooleanField(default=False) #推荐 （备用）
    memo=models.CharField(max_length=500,null=True)  #书籍备注（500字）
    type=models.ForeignKey('BookType')    #类型
    created=models.DateTimeField(auto_now_add=True) #创建时间



class BookType(models.Model):
    name=models.CharField(max_length=20) #类型名
    partype=models.ForeignKey('BookType', blank=True, null=True, on_delete=models.SET_NULL) #多级表示父级别
    memo=models.CharField(max_length=200) #备注
    created=models.DateTimeField(auto_now_add=True) #创建时间

