# Create your views here.


from django.http import HttpResponse


def index(req):
    return HttpResponse("manages-index")
def addbook(req):
    return HttpResponse("add !!")
