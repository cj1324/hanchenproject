#!/usr/bin/env python
# coding: utf-8
""" 空的处理engin参数的收集模块
"""


from util.urltool import checkfix_scheme
from model.urldata import UrlData
from collect.base import BaseCollect


class NullCollect(BaseCollect):
    def __init__(self):
        BaseCollect.__init__(self)

    def run(self):
        for  url in self.options['sites']:
            real_url = checkfix_scheme(url)
            if self.url_valid(real_url):
                self._urls.append(UrlData(real_url))
            else:
                raise
