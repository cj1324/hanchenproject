#!/usr/bin/env python
# coding: utf-8

import os
import sys
import logging
import traceback

from engine import Engine

from util.cmdline import parse_opts
from util.cmdline import OptParseError
from util.config import parse_conf
from util.logger import parse_logger


def main():
    opts = None  # 命令行参数
    conf = None  # 全局的配置
    eng = None  # 主线程核心引擎对象

    # 加载命令行参数
    try:
        opts = parse_opts()
    except OptParseError:
        return

    # 加载系统运行配置
    try:
        conf = parse_conf(opts)
    except Exception:
        print '>> yaml syntax error'
        traceback.print_exc()
        return

    # 加载日志配置
    parse_logger(conf)

    # 开始日志记录
    log = logging.getLogger('robber')
    log.debug('logging modules init ok.')
    try:
        eng = Engine(conf)
        log.debug('Engine modules init ok.')
    except Exception:
        traceback.print_exc()
        log.critical('Engine modules init fatal.')
        return

    try:
        eng.run()
        log.info('all job done exit.')
        sys.exit(0)
    except KeyboardInterrupt:
        log.warning('rev KeyboardInterrupt exit.')
        print '<< KeyboardInterrupt'
        os._exit(-1)  # 警告: 危险的快速退出
    except Exception:
        log.critical('engine object run fatal.')
        traceback.print_exc()

if __name__ == '__main__':
    main()
    sys.exit(-1)
