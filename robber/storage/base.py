#!/usr/bin/env python
# coding: UTF-8

""" 存储模块的基类
"""

from util.basethread import BaseThread


class BaseStorage(BaseThread):
    def __init__(self, iqueue, **kw):
        self.__need_config = False

        self.__options = None

        self._configed = False

        BaseThread.__init__(self, **kw)

    def config(self, params):
        self.__options = params
        self._configed = True

    def reset(self):
        self._configed = False
        self.__options = None

    @property
    def options(self):
        if self._configed:
            return self.__options

    def process(self):
        raise NotImplementedError

    def run(self):
        if self.__need_config:
            if not self._configed:
                # 请先配置参数
                raise
        self.process()
