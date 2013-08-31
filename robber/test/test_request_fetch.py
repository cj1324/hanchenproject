#!/usr/bin/env python
# coding: utf-8
""" 测试request模块中fetch功能

"""
import unittest
from request.fetch import Fetch
from model.urldata import UrlData
from pprint import pprint


class TestFetchBaidu(unittest.TestCase):
    """测试访问百度的连通性 """
    def setUp(self):
        pass

    def test_request(self):
        req_url = 'http://baidu.com'
        urld = UrlData(req_url)
        fet = Fetch()
        fet.request(urld)
        self.assertEqual(urld.code, 200)
        self.assertEqual(req_url, urld.trueurl)
        self.assertEqual(urld.msg, 'OK')

    def tearDown(self):
        pass

class TestFetchSina(unittest.TestCase):
    """测试访问新浪的连通性 """
    def setUp(self):
        pass

    def test_request(self):
        req_url = 'http://www.sina.com.cn'
        urld = UrlData(req_url)
        fet = Fetch()
        fet.request(urld)
        self.assertEqual(urld.code, 200)
        self.assertEqual(req_url, urld.trueurl)
        self.assertEqual(urld.msg, 'OK')

    def tearDown(self):
        pass

class TestFetchSina404(unittest.TestCase):
    """测试访问新浪404页面触发 HTTPError """
    def setUp(self):
        pass

    def test_request(self):
        req_url = 'http://www.sina.com.cn/404.html'
        urld = UrlData(req_url)
        fet = Fetch()
        fet.request(urld)
        self.assertEqual(urld.code, 404)
        self.assertEqual(req_url, urld.trueurl)
        self.assertEqual(urld.msg, 'Not Found')

    def tearDown(self):
        pass

class TestFetchUnknow(unittest.TestCase):
    """测试访问GitHub500页面触发实际上 是200页面 """
    def setUp(self):
        pass

    def test_request(self):
        req_url = 'https://unknowndomain99.com'
        urld = UrlData(req_url)
        fet = Fetch()
        fet.request(urld)
        self.assertEqual(urld.code , -1)
        self.assertEqual(urld.trueurl, '')
        self.assertEqual(urld.msg, '')

    def tearDown(self):
        pass
