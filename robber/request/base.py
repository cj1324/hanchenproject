#!/usr/bin/env python
# coding: utf-8
"""  爬虫请求线程实现
"""


from util.basethread import BaseThread
from request.fetch import Fetch
import logging
import time

QUEUE_FULL_WAIT = 5


class RequestThread(BaseThread):
    def __init__(self, iqueue, oqueue, delay, **kw):
        BaseThread.__init__(self, **kw)
        self.log = logging.getLogger('robber.request.thread')
        self._init_fetch()
        self.iqueue = iqueue
        self.oqueue = oqueue
        self.delay = delay

    def _init_fetch(self):
        self.fetch = Fetch()

    def process(self):
        while True:
            urldata = self.iqueue.get()
            self.log.debug('iqueue get url: %s' % urldata.url)
            while(urldata.trycount > 0):
                try:
                    self.fetch(urldata)
                except Exception:
                    urldata.trycount -= 1
                    self.log.error('fetch url: %s' % urldata.url)
            self.iqueue.task_done()
            while (self.oqueue.full()):
                self.log.warning('oquque is full, wait...')
                time.sleep(QUEUE_FULL_WAIT)
            self.log.debug('oqueue put url: %s' % urldata.url)
            self.oqueue.put(urldata)
            time.sleep(self.delay)
