#!/usr/bin/env python
# coding: UTF-8

""" 存储模块的管理器
"""

import logging


class StorageMange(object):
    def __init__(self, sconf, iqueue):
        self.conf = sconf
        self.log = logging.getLogger('robber.storage.manage')
