#!/usr/bin/env python
# coding: utf-8


import re

url_regex = re.compile(
        r'^(?:http|ftp)s?://' # http:// or https://
        r'(?:(?:[A-Z0-9](?:[A-Z0-9-]{0,61}[A-Z0-9])?\.)+(?:[A-Z]{2,6}\.?|[A-Z0-9-]{2,}\.?)|' #domain...
        r'localhost|' #localhost...
        r'\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})' # ...or ip
        r'(?::\d+)?' # optional port
        r'(?:/?|[/?]\S+)$', re.IGNORECASE)

url_head = re.compile(r'^(?:https?)://')  # 应该不支持HTTPS


def url_valid(url):
    return url_regex.match(url)

def checkfix_scheme(url):
  if url_head.match(url):
    return url
  else:
    return 'http://%s' % url

