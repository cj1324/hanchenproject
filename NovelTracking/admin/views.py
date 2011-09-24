#!usr/bin/python
#-*- coding:utf-8 -*-
# Django settings for NovelTracking project.

# Create your views here.
from django.http import HttpResponse
from django.shortcuts import render_to_response

def test(request):
    return HttpResponse('test hanchen!!!')

def login(request):
    if request.method=="POST":
#        return HttpResponse(request.POST["txt_user"])
        return HttpResponse("xxxxxxxxxx")
    else:
        return render_to_response('admin/login.htm')
