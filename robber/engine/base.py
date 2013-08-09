#/usr/bin/env python
# coding: utf-8

""" 引擎类协调各个模块进行工作
"""

import logging
from Queue import Queue


class Engine():

    def __init__(self, conf, ready=True):
        self.log = logging.getLogger('robber.engine')
        self.g_conf = conf
        self.conf = conf['engine']
        self.ready = ready
        if ready:
            self._init_queue()
            self._init_use_filters()

    def _init_queue(self):
        self.log.debug('Queue init max_size:%d' % self.conf['queue_max_size'])
        self.g_queue = Queue(self.conf['queue_max_size'])

    def _init_use_filters(self):
        self.g_filters = []
        if 'filters' in self.g_conf and \
                isinstance(self.g_conf['filters'], list):
            for flt in self.g_conf['filters']:
                self.log.debug('import filter %s' % flt)
                pass

    def run(self):
        self.log.debug('Engine run ')
