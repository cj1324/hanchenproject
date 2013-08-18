#/usr/bin/env python
# coding: utf-8

""" 引擎类协调各个模块进行工作
"""

import logging
from Queue import Queue
from collect.base import COLLECT_TYPE



class Engine():

    def __init__(self, conf, ready=True):
        self.log = logging.getLogger('robber.engine')
        self.g_conf = conf
        self.conf = conf['engine']
        self.ready = ready
        if ready:
            self._init_queue()
            self._init_use_filters()
            self.cm = CollectMange(self.g_conf['collects'],
                                   self.g_queue,
                                   self.g_filters,
                                   self.conf)

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
        self.cm.process_common()



class CollectMange(object):
    def __init__(self, conf, queue, filters, e_conf):
        self.log = logging.getLogger('robber.collect')
        self._conf = conf
        self._e_conf = e_conf
        self._filters = filters
        self._queue = queue
        self._common_list = []
        self._daemon_list = []
        self._response_list = []
        self._init_list()

    def _init_list(self):
        for  k  in self._conf:
            # TODO 缺少模块导入前检查
            self.log.debug('init collect: %s ' % k)
            try:
                mod = __import__('collect.%s' % k, fromlist=[k])
                collect_cls  = getattr(mod, k)
            except:
                raise
            obj =  collect_cls()
            if obj.collect_type ==COLLECT_TYPE.COMMON:
                self._common_list.append({'name': k,
                                          'cls': collect_cls,
                                          'config': self._conf[k]})
            if obj.collect_type == COLLECT_TYPE.DAEMON:

                self._daemon_list.append({'name': k,
                                          'cls': collect_cls,
                                          'config': self._config[k]})
            if obj.collect_type == COLLECT_TYPE.RESPONSE:
                self._response_list.append({'name': k,
                                            'cls': collect_cls,
                                            'config': self._config[k]})
            del obj
            self.log.debug('init collect: %s end' % (k))
    @property
    def need_daemon(self):
        if len(self._daemon_cls):
            return True
        return False

    @property
    def need_response(self):
        if len(self._response_cls):
            return True
        return False

    def process_common(self):
        for common in self._common_list:
            cls = common['cls']
            name = common['name']
            config = common['config']
            if config is None:
                # fix config None
                config = {}
            if 'sites' in config:
                assert(isinstance(config['sites'], list))
                config['sites'].extend(self._e_conf['sites'])
            else:
                config['sites'] = self._e_conf['sites']
            cobj = cls()
            cobj.config(config)
            self.log.debug('start collect:%s process' % name)
            cobj.process()
            self.log.debug('start collect:%s process end' % name)
            urls = cobj.output()
            for u in urls:
                self.log.debug('queue put url: %s' % u.url)
                self._queue.put(u)

    def process_daemon(self):
        raise NotImplementedError

