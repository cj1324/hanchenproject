#!usr/bin/python
#-*- coding:utf-8 -*-
# Django settings for NovelTracking project.

from django.conf.urls.defaults import patterns, include, url
from django.views.generic.simple import direct_to_template
from NovelTracking import settings;

# Uncomment the next two lines to enable the admin:
# from django.contrib import admin
# admin.autodiscover()

urlpatterns = patterns('',
    # Example:
    # (r'^NovelTracking/', include('NovelTracking.foo.urls')),

    # Uncomment the admin/doc line below to enable admin documentation:
    # (r'^admin/doc/', include('django.contrib.admindocs.urls')),

    # Uncomment the next line to enable the admin:
        (r'^admin/', include('NovelTracking.admin.urls')),
        (r'^robots\.txt$',direct_to_template,{'template': 'robots.txt', 'mimetype': 'text/plain'}),
   #显示目录列表  debgu!!!!
        (r'^static/(?P<path>.*)$','django.views.static.serve',{'document_root':settings.MEDIA_ROOT,'show_indexes':True}),
)
