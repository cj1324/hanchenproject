#!usr/bin/python
#-*- coding:utf-8 -*-

from django.http import HttpResponse
from django.shortcuts import render_to_response
from  InterviewProject.testone import algo
from InterviewProject.testone.models import Batchs,Details


def  main(request):
    if request.method=="POST":
        algo.calc(request.POST["txt_value"]);
        return HttpResponse("&nbsp;&nbsp;&nbsp;&nbsp;以成功提交数据，请登录后台进行数据查看 <a href=\"/testone/admin/\" >点击进入后台</a>")
    else:
        return render_to_response("testone/main.html")



def admin(request):
    bat= Batchs.objects.order_by('-created')[0];
    msg_text='';
    maps={}
    lists=[];
    text=''
    created=''
    if bat:
        text=bat.text
        created=bat.created
        for i in range(26):
            k=i+97;
            o=Details.objects.filter(batch=bat,key=k)
            lists.append(chr(k))
            if o:
                maps[chr(k)]=o[0].value
    else:
        msg_text=" 您没有提交任何数据！！"
    obj={'text':text,'created':created,'maps':maps,'lists':lists,'msg_text':msg_text}
    return render_to_response("testone/admin.html",obj)
