#!usr/bin/python
#-*- coding:utf-8 -*-

from django.http import HttpResponse
from django.shortcuts import render_to_response
from PIL import Image
import StringIO,os
from InterviewProject import settings

def  main(request):
    if request.method=="POST":
        filestream=request.FILES["fi_input"] 
        if filestream:
            m= getimginfo(filestream)
            if m:
                return render_to_response("testtwo/main.html",{'msg_text':"OK","info":m,"visb":"block"})
        return render_to_response("testtwo/main.html",{'msg_text':'请上传合法图片！',"visb":"none"})
    else:
        return render_to_response("testtwo/main.html")

def getimginfo(filestream):
    filesize = filestream.size;
    #filetype = filestream['content_type'] 
    filename = filestream.name;
    imgfile=StringIO.StringIO(filestream.read()) 
    try:
        img=Image.open(imgfile)
        (width, height) = img.size
        diskpath=os.path.join(settings.MEDIA_ROOT,filename)
        urlpath=os.path.join("/static/",filename)
        tofile=open(diskpath,'wb+')
        for chunk in filestream.chunks():
            tofile.write(chunk)
        tofile.close()
        obj= {'filename':filename,'width':width,'height':height,'size':filesize,'diskpath':diskpath,'urlpath':urlpath}
        return obj
    except Exception,e:
        print e
        return False;
    
