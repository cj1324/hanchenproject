#!usr/bin/python
#-*- coding:utf-8 -*-
#filename hcpj/imgpj/coreFun.py  
#  加水印的核心模块
#
#
''' 加水印操作的核心模块， 主要是文件操作 '''

__author__="寒晨"	 	#作者
__version__="1.0" 		#版本
__date__="2010.9.2"	#日期
__copyright__="寒晨所有&2010"#授权
__license__="Python"	#证书

import os
from PIL import Image,ImageEnhance
import  shutil;


def CheckFileNum(dir):
    ''' 扫描目录获取图片数量方法 '''
    imglist=[]
    try:
        RecDir(dir,imglist)
        #print imglist
    except:
        raise
        
    return imglist

def RecDir(dir,totallist):
    ''' 递归目录的方法 '''
    #print "<<"+str(len(totallist))+">>"+dir
    sublists=[]
    try:
        sublists=os.listdir(dir)
    except Exception,e:
        #可能目录没有访问权限 进行异常处理 跳过该目录
        pass
        return
    for i in range(len(sublists)):
        #print os.path.join(dir,sublists[i])
        if os.path.isdir(os.path.join(dir,sublists[i])):
            RecDir(os.path.join(dir,sublists[i]),totallist)
        elif CheckImg(os.path.join(dir,sublists[i])):
            totallist.append(os.path.join(dir,sublists[i]))
        
    
    
def CheckImg(fullname):
    '''  检查文件是否是图片  '''
    if os.path.isfile(fullname):
        ext=os.path.splitext(fullname)[-1]
        if ext==".jpg":
            return True
        elif ext==".gif":
            return True
        elif ext==".png":
            return True
        else:
            return False
        

def reduce_opacity(im, opacity):
    """ 处理透明的问题 """
    assert opacity >= 0 and opacity <= 1
    if im.mode != 'RGBA':
        im = im.convert('RGBA')
    else:
        im = im.copy()
    alpha = im.split()[3]
    alpha = ImageEnhance.Brightness(alpha).enhance(opacity)
    im.putalpha(alpha)
    return im

def RunPrint(watch,imgfile,opacity):
    ''' 打水印的方法 '''
    try:
        imgsou=Image.open(imgfile).convert("RGBA")
        #os.rename(imgfile,imgfile+".bak")
        shutil.copyfile(imgfile,imgfile+".bak")
        imgmark=Image.open(watch)
        imgmark=reduce_opacity(imgmark,opacity)
        imgsou.paste(imgmark,GetPos(imgsou.size,imgmark.size))
        imgsou.save(imgfile)

    except:
        raise
        return False
    pass
    return True;


def GetPos(s_size,m_size):
    ''' 设置水印的坐标 '''
    x=s_size[0]-m_size[0]-10;
    y=s_size[1]-m_size[1]-10;
    return (x,y)
