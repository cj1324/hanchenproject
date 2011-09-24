from django.conf.urls.defaults import patterns
from  NovelTracking.admin.views import test,login
# Uncomment the next two lines to enable the admin:
# from django.contrib import admin
# admin.autodiscover()

urlpatterns = patterns('',
      (r'^$',test),
      (r'^login/$',login),
)
