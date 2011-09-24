#!usr/bin/python
#-*- coding:utf-8 -*-

from django.template.defaultfilters import register

#处理django 内建模板 不支持字典的问题。 
@register.filter(name='val')
def val(dict, index):
    if index in dict:
        return dict[index]
    return ''
