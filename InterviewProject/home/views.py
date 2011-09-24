#!usr/bin/python
#-*- coding:utf-8 -*-

#from django.http import HttpResponse
from django.shortcuts import render_to_response

def  main(request):
    return render_to_response("home.html")
