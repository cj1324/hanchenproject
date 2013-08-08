#!/usr/bin/env python
# coding: utf-8

"""
处理命令行输入参数
"""

from optparse import OptionParser
from optparse import OptParseError


def parse_opts():
    parser = OptionParser(usage='usage: %prog [options]')
    parser.add_option('-c', '--config', dest="config",
                      help=u'指定配置文件地址')
    parser.add_option('-s', '--sock', dest="sock",
                      help=u'本地sock文件,方便统计和调试')
    parser.add_option('-l', '--sites', dest='site_list',
                      help=u'用逗号分隔的多个站点列表,也支持单个')
    parser.add_option('-d', '--debug', dest='debug', type='int',
                      help=u'调试级别0, 1, 2, default 0')
    # -d
    # 0. 默认为0 控制台显示 ERROR, CRITICAL级别日志
    # 1. 在控制台输出 INFO, WARNING, ERROR, CRITICAL级别日志
    # 2. 细化rpyc的统计数据.开启其他调试手段
    # 3. 显示最详细的日志在控制台
    opts, args = parser.parse_args()
    if not opts.config:
        parser.print_help()
        raise OptParseError('print_help')
    return opts
