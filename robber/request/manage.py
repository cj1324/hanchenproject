#!/usr/bin/env python
# coding: utf-8

""" 请求模块管理类
"""

import logging
from request.base import RequestThread


class RequestManage(object):
    def __init__(self, rconf, iqueue, oqueue):
        self.conf = rconf
        self.iqueue = iqueue
        self.oqueue = oqueue
        self.log = logging.getLogger('robber.request.manage')
        self._init_requestthreads()

    def _init_requestthreads(self):
        self.log.debug('RequestThread init requestthreads')
        self.threads = []
        for i in range(self.conf['thread_nums']):
            rt = RequestThread(self.iqueue,
                               self.oqueue,
                               self.conf['thread_delay'])
            rt.setDaemon(True)
            self.threads.append(rt)
        self.log.debug('RequestThread init requestthreads ok.')

    def process(self):
        """搜集模块工作方法，启动线程处理queue数据 """
        self.log.debug('RequestManage process start.')
        for thread in self.threads:
            self.log.debug('RequestThread [%s] start' % thread.getName())
            thread.start()
        self.log.info('RequestManage process ok.')

    def check_stop(self):
        """ 检查request模块是否空闲 """

        self.log.debug('RequestManage check stop.')
        qsize = self.iqueue.qsize()
        if qsize == 0:
            return True
        return False

    def stop(self):
        self.log.info('RequestManage stop start.')
        for thread in self.threads:
            self.log.debug('RequestThread [%s] stop' % thread.getName())
            thread.stop()
        self.log.info('RequestManage stop all ok.')
