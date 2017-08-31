from django.conf.urls import url

from . import views

urlpatterns = [
    url(r'^$', views.index, name='index'),
	url(r'^(?P<id_obra>[0-9]+)/$', views.ver_obra, name='ver_obra')
]