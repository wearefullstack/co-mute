from django.contrib import admin
from .models import *

#Username: admin
#Password: admin

# Register your models here.
admin.site.register(User)
admin.site.register(Carpool)
admin.site.register(Passenger)