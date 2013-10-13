#!/usr/bin/env python
# coding: UTF-8

""" 存储模块的管理器
"""

import logging


class StorageMange(object):
    def __init__(self, sconf, iqueue):
        self._conf = sconf
        self.log = logging.getLogger('robber.storage.manage')

    def _init_list(self):
        for k in self._conf:
            self.log.debug('init storage: %s ' % k)
            try:
                mod = __import__('storage.%s' % k, fromlist=[k])
                storage_cls = getattr(mod, k)
            except:
                raise
            self._storage_list.append({'name': k,
                                      'cls': storage_cls,
                                      'config': self._config[k]})
            self.log.debug('init storage: %s end' % (k))
