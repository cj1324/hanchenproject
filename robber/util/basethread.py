#!/usr/bin/env python
# coding: utf-8
"""  基础的线程类

"""
import logging

from threading import Thread
from threading import Event




class BaseThread(Thread):
    """ 基础的线程类
        实现停止功能和日志记录
    """
    def __init__(self, **kw):
        Thread.__init__(self, **kw)
        self._ev_stop = Event()
        self.log = logging.getLogger()

    def need_stop(self):
        self._ev_stop.isSet()

    def stop(self):
        self._ev_stop.set()

    def process(self):
        """ 线程处理的核心方法 
           注意：need_stop的check
        """
        raise NotImplementedError

    def run(self):
        self.process()
