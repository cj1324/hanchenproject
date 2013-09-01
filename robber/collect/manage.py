#!/usr/bin/env python
# coding: utf-8
""" 收集器管理模块

"""

import logging
from collect.base import COLLECT_TYPE


class CollectManage(object):
    def __init__(self, conf, iqueue, oqueue, filters, e_conf):
        self.log = logging.getLogger('robber.collect.manage')
        self._conf = conf
        self._e_conf = e_conf
        self._filters = filters
        self._history_urls = []
        self._iqueue = iqueue
        self._oqueue = oqueue
        self._common_list = []
        self._daemon_list = []
        self._response_list = []
        self._init_list()

    def _init_list(self):
        for k in self._conf:
            # TODO 缺少模块导入前检查
            self.log.debug('init collect: %s ' % k)
            try:
                mod = __import__('collect.%s' % k, fromlist=[k])
                collect_cls = getattr(mod, k)
            except:
                raise
            obj = collect_cls()
            if obj.collect_type == COLLECT_TYPE.COMMON:
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

    def check_put_queue(self, urls):
        """ 检查URL是否有效和插入queue中"""

        for u in urls:
            self.log.debug('queue put url: %s' % u.url)
            if u.url in self._history_urls:
                self.log.debug('Ignore history already exists '
                               'in the URL url:%s' % u.url)
            else:
                self.log.info('queue put url: %s' % u.url)
                self._history_urls.append(u.url)
                self._iqueue.put(u)

    def process_common(self):
        """ 处理通用收集器逻辑 """

        for common in self._common_list:
            cls = common['cls']
            name = common['name']
            config = common['config']
            if config is None:
                # fix config None
                config = {}

            # merge sites config
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
            self.check_put_queue(urls)

    def process_daemon(self):
        """ 处理守护线程收集器 """
        raise NotImplementedError
