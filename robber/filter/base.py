#!/usr/bin/env python
# coding: utf-8l


class FILTER_TYPE(object):
    COMMON = 0
    COLLECT = 1
    STORAGE = 2


class PROC_OBJECT(object):

    OBJECT = 100
    RESPONSE = 101
    STREAM = 102  # 文件流
    STRING = 103  # 标准字符串 不考虑unicode
    URL = 104  # 完整的URL 可能包含GET参数
    DOMAIN = 105
    DATETIME = 106
    DATE = 107
    TIME = 108
    INT = 109
    FLOAT = 110
    LONG = 111
    UNICODE = 112

    LIST = 200
    URL_LIST = 201
    DOMAIN_LIST = 202
    STR_LIST = 203

    DICT = 300
    JSON = 301  # 字符串形式存在
    YAML = 302  # 字符串形式存在
    KV = 303  # key value


class BaseFilter(object):

    def __init__(self):

        # 是否需要配置
        self.__need_config = False

        # 假如输入并非要求的数据结构是否尝试自动转化数据
        self.__auto_convert = False

        # 过滤模块的类型。决定其适用范围
        self.__filter_type = FILTER_TYPE.COMMON

        # 输入对象要求的数据类型 可用配合 _auto_conver 进行自动处理
        self.__input_type = PROC_OBJECT.OBJECT

        # 输出对象要求的数据类型
        self.__output_type = PROC_OBJECT.OBJECT

        # 以下变量可能在运行过程中发生变化

        # 假如需要配置是否被正确配置了
        self._configed = False

        self._options = None

        self._params = None

        # 校验通过的input变量
        self._input = None

        # 用来确认  process 是否被有效调用了
        self._processed = False

        # process 方法结果数据先写到这里
        self._ouput = None

    def _verify(self, obj, obj_type):
        pass


    @property
    def auto_convert(self):
        return self.__auto_convert

    @property
    def need_config(self):
        return self.__need_config

    @property
    def output_type(self):
        return self.__output_type

    @property
    def input_type(self):
        return self.__input_type

    @property
    def options(self):
        if self.configed:
            return self._options
        # 未配置不允许读取
        raise

    @property
    def params(self):
        return self._params

    def config(self, option):
        if self._configed:
            # 只允许配置一次
            raise
        self._options = option
        self._configed = True

    def update(self, params):
        self._params = params

    def input_convert(self, obj):
        # TODO 第一个版本不实现这个功能
        return obj

    def input(self, obj):
        pass

    def process(self):
        if self.__need_config:
            if not self._configed:
                # 请先配置参数
                raise
        self.run()
        self._processed = True

    def run(self):
        ''' 这个方法是具体类需要实现的关键方法
        '''
        raise NotImplementedError

    def output(self):
        if self._processed and self._verify(self._output, self.output_type):
            return self._output
        else:
          raise
