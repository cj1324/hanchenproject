#!/usr/bin/env python
# coding: utf-8
""" URL请求发起模块
    部分代码片段来自ksspider
"""

#import os
#import time
#import socket
import urllib2
from urllib2 import URLError
from urllib2 import HTTPError
import traceback
from gzip import GzipFile
from StringIO import StringIO
import zlib
from zlib import decompress, MAX_WBITS

fetch_const = {'USER_AGENT': '',
               'REFERER': 'http://www.jiashule.com',
               'ACCEPT': '*/*',
               'MAX_CONTENT_LENGTH': 10000000,  # 9.5M
               'ENC_GZIP': True,
               'ENC_DEFLATE': True}


class FetchException(URLError):
    pass


class Fetch(object):
    """ 包裹urllib2 实现一些 压缩和填充urldata 类型 """
    def __init__(self):
        self._opener = urllib2.build_opener()
        # 传输过程中进行编码
        self._acceptencoding_gzip = fetch_const['ENC_GZIP']
        self._acceptencoding_deflate = fetch_const['ENC_DEFLATE']
        self._acceptencoding_content = self._get_acceptencoding()
        self._req_headers = {'User-Agent': fetch_const['USER_AGENT'],
                             'Accept': fetch_const['ACCEPT'],
                             'Referer': fetch_const['REFERER']}

        if self._acceptencoding_content:
            self._req_headers['Accept-Encoding'] = \
                self._acceptencoding_content

    def dict_to_headers(self, _dict):
        """{'a':1, 'b':2} -> [('a',1), ('b',2)]"""
        return list(_dict.iteritems())

    def set_http_opener(self, opener):
        """设置http请求句柄"""
        self._opener = opener

    def set_headers(self, **headers):
        """设置http请求头"""
        for key in headers:
            value = headers[key]
            self._req_header[key] = value

    def _get_acceptencoding(self):
        if self._acceptencoding_gzip and\
                self._acceptencoding_deflate:
            return 'gzip, deflate'
        if self._acceptencoding_gzip:
            return 'gzip'
        if self._acceptencoding_deflate:
            return 'deflate'
        return None

    def _check_method(self, method):
        if method in ('GET', 'POST'):
            return True
        raise FetchException('HTTP请求的方法不受支持 method: %s' % method)

    def _decode_resp_data_html(self, resp_data, headers):
        if self._acceptencoding_deflate:
            if headers.get('content-encoding', '') == 'deflate':
                return self._resp_data_decode_method_deflate(resp_data)
        if self._acceptencoding_gzip:
            if headers.get('content-encoding', '') == 'gzip':
                return self._resp_data_decode_method_gzip(resp_data)
        return resp_data

    def _resp_data_decode_method_gzip(self, resp_data):
        gf = GzipFile(fileobj=StringIO(resp_data), mode="r")
        return gf.read(fetch_const['MAX_CONTENT_LENGTH']+1)

    def _resp_data_decode_method_deflate(self, resp_data):
        try:
            return decompress(resp_data, -MAX_WBITS)
        except zlib.error:
            return decompress(resp_data)
        except Exception:
            traceback.print_exc()
            return resp_data

    def _build_opener(self, urldata):
        """ urllib2 opener headers 初始化 """
        assert self._opener is not None
        if self._req_headers:
            self._opener.addheaders =\
                self.dict_to_headers(self._req_headers)

        if urldata.req_headers:
            self._opener.addheaders =\
                self.dict_to_headers(urldata.req_headers)

    def _get_contentlength(self, headers):
        """ 获取 headers content_length """
        content_length = headers.get('content-length')
        if content_length is None:
            return 0
        try:
            content_length = int(content_length)
        except Exception:
            traceback.print_exc()
            content_length = 0
        return content_length

    def _check_contentlenght(self, content_length):
        if content_length > fetch_const['MAX_CONTENT_LENGTH']:
            return False
        return True

    def _resp_fill_urldata(self, urldata, resp, resp_headers):
        urldata.resp_headers = resp_headers
        urldata.code = resp.code
        if hasattr(resp, 'msg'):
            urldata.msg = resp.msg
        urldata.trueurl = resp.url

    def _http_error_fill_urldata(self, urldata, error):
        urldata.code = error.code
        urldata.msg = error.msg
        urldata.errinfo = repr(error)
        _hdrs = error.hdrs.headers
        _heders = {}
        for h in _hdrs:
            kv = h.split(':')
            _heders[kv[0].lower().strip()] = kv[1].strip()
        urldata.resp_headers = _heders
        urldata.trueurl = error.filename

    def request(self, urldata):
        """发起http请求
        """
        resp = None  # 原始http response 对象
        self.url = urldata.url
        assert '?' not in urldata.url
        self._check_method(urldata.method)
        self._build_opener(urldata)

        try:
            # 如果req_headers不为空，则设置请求头
            if urldata.method == 'GET':
                if urldata.data:
                    self.url = self.url + '?' + urldata.data
                resp = self._opener.open(self.url)
            elif urldata.method == 'POST':
                resp = self._opener.open(self.url, urldata.data)
            _resp_headers = resp.headers.dict
            _content_length = self._get_contentlength(_resp_headers)
            self._check_contentlenght(_content_length)
            try:
                _resp_data = resp.read(fetch_const['MAX_CONTENT_LENGTH']+1)
            except Exception:
                traceback.print_exc()
                raise FetchException('response read 失败')
            self._resp_fill_urldata(urldata, resp, _resp_headers)
            _html_data = self._decode_resp_data_html(_resp_data,
                                                     _resp_headers)
            urldata.html = _html_data
            return urldata
        except HTTPError, e:
            self._http_error_fill_urldata(urldata, e)
            return urldata
        except FetchException, e:
            urldata.trycount -= 1
            if urldata.trycount == 0:
                urldata.errinfo = repr(e)
        except URLError, e:
            traceback.print_exc()
            urldata.trycount = 0
            urldata.errinfo = repr(e)
        except Exception, e:
            traceback.print_exc()
            urldata.errinfo = repr(e)
        finally:
            if hasattr(resp, 'close'):
                resp.close()


if __name__ == "__main__":
    fet = Fetch()
