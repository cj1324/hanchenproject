#/usr/bin/env python
# coding: utf-8

""" 引擎类协调各个模块进行工作
"""

import logging
from Queue import Queue

class Engine():

    def __init__(self, conf):
        self.log = logging.getLogger('robber')
        queue_max_size = 256
        self.queue = Queue(queue_max_size)


    def run(self):
        self.log.debug('Engine run ')
