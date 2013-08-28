#!/usr/bin/env python
# coding: utf-8
"""  爬虫请求模块
"""

__date__ = "2013-08-28"

from util.basethread import BaseThread
import logging

class RequestThread(BaseThread):
    def __init__(self, **kw):
        BaseThread.__init__(self, **kw)
        self.log = logging.getLogger('robber.request.thread')

    def process(self):
        pass
        # TODO
