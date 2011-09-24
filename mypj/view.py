#!usr/bin/python
#-*- coding:utf-8 -*-
#filename mypj/view.py  
#  这里是该模块的注释！！
#
#
''' 这是一个PYTHON 编程模板 这里写模块说明 请修改 '''

__author__="寒晨"	 	#作者
__version__="1.0" 		#版本
__date__="2010.11.12"	#日期
__copyright__="寒晨所有&2010"#授权
__license__="Python"	#证书

import os
import datetime;
from django.http import HttpResponse
from django.shortcuts import render_to_response


def MainRequest(request):
    hr='我是标题'
    content= os.path.join(request.META.items());
    
    return render_to_response('main.html',{'title':hr,'content':content})


def TestRequest(request,urlp,urlp2):
    hr= HttpResponse("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\r\n")
    hr.write("<html>\r\n")
    hr.write("<head>\r\n")
    hr.write("<title>")
    hr.write("我是标题")
    hr.write("</title>\r\n")
    hr.write("</head>\r\n")
    hr.write("<body>\r\n")
    hr.write("<div>")
    hr.write("时间：")
    hr.write(datetime.datetime.now());
    hr.write("</div>\r\n")
    hr.write("<div>实际地址:")
    hr.write(urlp)
    hr.write("</div>\r\n")
    hr.write("<div>实际地址2:");
    hr.write(urlp2)
    hr.write("</div>\r\n")
    hr.write("</body>\r\n")
    hr.write("</html>\r\n")
    return hr
  


class ClassName:
    ''' 这里是类的说明！！ 请修改  '''
    def __init__(self):
        ''' 这里是该类构造函数的说明 请修改 '''
        return



if __name__=="__main__":
    cn=ClassName()
    print help(cn)


