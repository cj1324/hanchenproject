#!usr/bin/python
#-*- coding:utf-8 -*-
#filename gaebookpj/view.py  
#  这里是该模块的注释！！
#
#
''' 这是一个PYTHON 编程模板 这里写模块说明 请修改 '''

__author__="寒晨"	 	#作者
__version__="1.0" 		#版本
__date__="2010.10.4"	#日期
__copyright__="寒晨所有&2010"#授权
__license__="Python"	#证书

import os
from google.appengine.api import users
from google.appengine.ext import webapp
from google.appengine.ext.webapp import template


class BaseRequestHandler(webapp.RequestHandler):
    def get_basic_template_values(self):
        curr_user=users.get_current_user_admin()
        is_admin=users.is_current_user_admin()
        logout_url=users.create_logout_url(self.reques.uri)
        login_url=users.create_login_url(self.request.uri)
        values={
            'curr_user':curr_user,
            'is_admin':is_admin,
            'logout_url':logout_url,
            'login_url':login_url,
        }
        return values

    def template_render(self,template_name,template_values={}):
        directory=os.path.split(os.path.dirname(__file__))[0]
        template_path=os.path.join(directory,'templates',template_name)
        values=self.get_basic_template_values()
        values.update(template_values)
        self.tesponse.out.write(template.render(template_path,values,debug=DEBUG).decode('utf-8'))


class ClassName:
    ''' 这里是类的说明！！ 请修改  '''
    def __init__(self):
        ''' 这里是该类构造函数的说明 请修改 '''
        return



if __name__=="__main__":
    cn=ClassName()
    print help(cn)


