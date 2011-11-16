from django.conf.urls.defaults import patterns, include, url
from django.contrib.staticfiles.urls import staticfiles_urlpatterns


# Uncomment the next two lines to enable the admin:
# from django.contrib import admin
# admin.autodiscover()

urlpatterns = patterns('',
    # Examples:
     url(r'^$', 'home.views.index', name='home'),
     url(r'^books/', include('books.urls')),
     url(r'^manages/', include('manages.urls')),

)

urlpatterns+=staticfiles_urlpatterns()
