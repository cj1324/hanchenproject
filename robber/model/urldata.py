#!/usr/bin/env python
# coding: utf-8
""" URL数据模型
"""

class UrlData:

    def __init__(self, url, method='GET', data={}, headers={}):

        #  预先初始化信息 
        self.entry_url = ''  # 根url,入口url
        self.domain = ''
        self.mark = '' # 用来区别某些特殊URL访问
        self.trycount = 3 # 某些情况下超时尝试次数
        self.deep = 0  # 限制请求深度
        self.index = 0 # 现在请求最大页面数


        # requst 相关信息
        self.url = url
        self.method = method
        self.data = data  # POST或 GET的数据
        self.fragment = ''  # URL中# 后面的部分信息
        self.referer = ''
        self.req_headers = headers

        # response 返回的相关信息
        self.resp_headers = {}
        self.code =  -1
        self.msg =  ''
        self.trueurl = ''  #response 中带的url

        # response 进一步解析的数据
        self.html = ''
        self.charset = ''
        self.title = ''
        self.errinfo = '' # 假如失败错误相关信息


