from django.shortcuts import render
from django.http import HttpResponse
from django.template import loader
from .models import *


def index(request):
	latest_obra_list = Obra.objects.order_by('-id')[:5]
	template = loader.get_template('SGOapp/index.html')
	context = {
        'latest_obra_list': latest_obra_list,
    }
	return HttpResponse(template.render(context, request))

def ver_obra(request, id_obra):
	return HttpResponse("You're looking at obra %s." % id_obra)