from django.conf.urls.defaults import patterns, include, url
from InterviewProject import settings
from InterviewProject.home.views import main

# Uncomment the next two lines to enable the admin:
# from django.contrib import admin
# admin.autodiscover()

urlpatterns = patterns('',
    # Examples:
    # url(r'^$', 'InterviewProject.views.home', name='home'),
    # url(r'^InterviewProject/', include('InterviewProject.foo.urls')),

    # Uncomment the admin/doc line below to enable admin documentation:
    # url(r'^admin/doc/', include('django.contrib.admindocs.urls')),

    # Uncomment the next line to enable the admin:
    # url(r'^admin/', include(admin.site.urls)),
    (r'^$',main),
    (r'^testone/',include('InterviewProject.testone.urls')),
    (r'^testtwo/',include('InterviewProject.testtwo.urls')),
    (r'^static/(?P<path>.*)$','django.views.static.serve',{'document_root':settings.MEDIA_ROOT,'show_indexes':False}), 
)
