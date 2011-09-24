#!usr/bin/python
#-*- coding:utf-8 -*-

from django.db import models

# Create your models here.

class Batchs(models.Model):
    text=models.TextField()
    created=models.DateTimeField(auto_now_add=True)

    def __unicode__(self):
        return self.text;

#    def getlast(self):
#        return self.objects.order_by('created')[0];

class Details(models.Model):
    batch=models.ForeignKey(Batchs)
    key=models.IntegerField()
    value=models.IntegerField()

    def __unicode__(self): 
        return 'key:%s|value:%s'%(self.key,self.value)

