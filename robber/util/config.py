#!/usr/bin/env python
# coding: utf-8

import os
import yaml

def parse_conf(opts):

    # 在命令行参数中已经判断这里直接断言
    assert(os.path.isfile(opts.config))
    conf_data = None

    conf_file = file(opts.config, 'r')
    try:
        conf_data = yaml.load(conf_file)
    except Exception:
        raise
    return conf_data

