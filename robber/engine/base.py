#/usr/bin/env python
# coding: utf-8

""" 引擎类协调各个模块进行工作
"""

import logging
import time
from Queue import Queue
from request.manage import RequestManage
from collect.manage import CollectManage


class Engine():
    """ 核心爬虫管理引擎 负责各个组件的初始化 """

    def __init__(self, conf, ready=True):
        self.log = logging.getLogger('robber.engine')
        self.g_conf = conf
        self.conf = conf['engine']
        self.ready = ready
        if ready:
            self._init_queue()
            self._init_use_filters()
            # 创建收集模块管理类
            self.cm = CollectManage(self.g_conf['collects'],
                                    self.input_queue,
                                    self.output_queue,
                                    self.g_filters,
                                    self.conf)

            # 创建请求模块管理类
            self.rm = RequestManage(self.g_conf['request'],
                                    self.input_queue,
                                    self.output_queue)

    def _init_queue(self):
        """ 初始化queue """

        self.log.debug('Queue init max_size:%d' % self.conf['queue_max_size'])

        self.input_queue = Queue(self.conf['queue_max_size'])
        self.output_queue = Queue(self.conf['queue_max_size'])

    def _init_use_filters(self):
        """ 初始化配置中全局导入的过滤器"""
        self.g_filters = []
        if 'filters' in self.g_conf and \
                isinstance(self.g_conf['filters'], list):
            for flt in self.g_conf['filters']:
                self.log.debug('import filter %s' % flt)
                pass

    def process(self):
        """ 引擎类核心 工作方法 """
        # 收集模块 通用类型的收集类 开始工作
        self.cm.process_common()

        # 请求模块 开始工作
        self.rm.process()

    def check_stop(self):
        pass

    def run(self):
        self.log.debug('Engine run ')
        self.process()
        # 系统工作，并等待结束通知
        while True:
            self.log.debug('loop wait 5(s) ...')
            time.sleep(5)
            u = self.output_queue.get()
            from pprint import pprint
            pprint(u.__dict__)
            break


