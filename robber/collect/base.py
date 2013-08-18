#!/usr/bin/env python
# coding: utf-8

""" 入口收集模块默认情况是根据参数
"""
from util.urltool import url_valid

class COLLECT_TYPE(object):
    COMMON = 0  # 处理参数
    DAEMON = 1  # 守护独立运行
    RESPONSE = 2  # 处理Response


class BaseCollect(object):
    def __init__(self):

        self.__collect_type = COLLECT_TYPE.COMMON

        self.__need_config = False

        self.__options = None

        self._configed = False
        self._processed = False

        self._urls = []

    def config(self, params):
        self.__options = params
        self._configed = True

    def reset(self):
        self._urls = []
        self._processed = False

    @property
    def options(self):
        if self._configed:
            return self.__options
    @property
    def collect_type(self):
      return self.__collect_type

    def process(self):
        if self.__need_config:
            if not self._configed:
                # 请先配置参数
                raise
        self.run()
        self._processed = True

    def run(self):
        raise NotImplementedError

    def url_valid(self,url):
        if url_valid(url):
            return url

    def output(self):
        if self._processed:
            return self._urls
        else:
            # 请先配置运行process
            raise

