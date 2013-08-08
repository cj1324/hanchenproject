#!/usr/bin/env python
# coding: utf-8

# TODO 需要python27以上版本
import logging
from logging.config import dictConfig

LOG_FORMATTER = '[%(asctime)s] %(levelname)-8s %(name)s %(pathname)s %(funcName)s %(lineno)d %(process)d %(thread)d %(threadName)s: %(message)s'
SIMPLE_FORMATTER = '>> %(levelname)s %(message)s'
DATE_FMT = '%Y-%m-%d %H:%M:%S'

def parse_logger(conf):
    # TODO 根据配置和命令行参数控制日志
    conf_dict = {'version': 1,
                 'disable_existing_loggers': False,
                 'formatters': {'verbose': {'format': LOG_FORMATTER,
                                            'datefmt': DATE_FMT},
                                'simple': {'format': SIMPLE_FORMATTER,
                                            'datefmt': DATE_FMT}},
                 'handlers': {
                     'console': {'level': 'DEBUG',
                                 'class': 'logging.StreamHandler',
                                 'stream': 'ext://sys.stdout',
                                 'formatter': 'verbose'},
                     'file': {'level': 'DEBUG',
                              'class': 'logging.handlers.RotatingFileHandler',
                              'filename': '/tmp/robber.log',
                              'maxBytes': 10485760,
                              'backupCount': 9,
                              'formatter': 'verbose'}},
                     'loggers': {'robber': {'handlers': ['console', 'file'],
                                           'level': 'DEBUG'}}}

    dictConfig(conf_dict)
    return
