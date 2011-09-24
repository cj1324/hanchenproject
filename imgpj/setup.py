#!usr/bin/python
#-*- coding:utf-8 -*-
#filename hcpj/imgpj/setup.py  
#   利用py2exe打包时使用
#
#
'''  打包功能  '''

__author__="寒晨"	 	#作者
__version__="1.0" 		#版本
__date__="2010.9.3"	#日期
__copyright__="寒晨所有&2010"#授权
__license__="Python"	#证书


from distutils.core import setup  
import py2exe  

includes = ["encodings", "encodings.*"]  
options = {"py2exe":  
            {   "compressed": 1,  
                "optimize": 2,  
                "includes": includes,  
                "bundle_files": 1  
            }  
          }  
setup(     
    version = "0.1.0",  
    description = "by: chen1324@gmail.com 2010.9.3",  
    name = "AddWatermark",  
    options = options,  
    zipfile=None,  
    windows=[{"script": "form.py"}],    
      
    )
