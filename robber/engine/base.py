#/usr/bin/env python
# coding: utf-8

""" 引擎类协调各个模块进行工作
"""

import logging
import time
from Queue import Queue
from collect.base import COLLECT_TYPE



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
                                   self.g_queue,
                                   self.g_filters,
                                   self.conf)

            # 创建请求模块管理类
            self.rm = RequestManage(self.g_conf['request'],
                                   self.g_queue)

    def _init_queue(self):
        """ 初始化queue """

        self.log.debug('Queue init max_size:%d' % self.conf['queue_max_size'])
        self.g_queue = Queue(self.conf['queue_max_size'])

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

    def run(self):
        self.log.debug('Engine run ')

        self.process()

        # 系统工作，并等待结束通知
        while True:
            time.sleep(30)



class CollectManage(object):
    def __init__(self, conf, queue, filters, e_conf):
        self.log = logging.getLogger('robber.collect')
        self._conf = conf
        self._e_conf = e_conf
        self._filters = filters
        self._history_urls = []
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

    def check_put_queue(self, urls):
        """ 检查URL是否有效和插入queue中"""

        for u in urls:
            self.log.debug('queue put url: %s' % u.url)
            if u.url in self._history_urls:
                self.log.info('queue put url: %s' % u.url)
                self._history_urls.append(u.url)
                self._queue.put(u)
            else:
                self.log.debug('Ignore history already exists '
                               'in the URL url:%s'%u.url)

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

class RequestManage(object):
    def __init__(self, conf, queue):
        pass

    def process(self):
        """搜集模块工作方法，创建线程处理queue数据 """
        pass

