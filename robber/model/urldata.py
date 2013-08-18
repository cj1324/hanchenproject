#!/usr/bin/env python
# coding: utf-8
""" URL数据模型
"""

class UrlData:

    def __init__(self, url=''):
        self.url = url
        self.method = 'GET'
        self.html = ''
        self.headers = {}
        self.deep = 0
        self.sort = 0
        self.httpcode = 0
        self.httpstatus = 0
        self.trueurl = ''
        self.errinfo = ''
        self.broken = False
        self.charset = ''
        self.domain = ''
        self.referer = ''
        self.entry_url = ''  # 根url,入口url
        self.number301 = 0  # 初始化301


