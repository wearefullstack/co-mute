from django.urls import path
from . import views

urlpatterns = [
    path('', views.apiOverview, name='api-overview'),
    path('user-list/', views.userList, name='user-list'),
    path('user-create/', views.createUser, name='user-create'),
    path('carpool-create/', views.createCarpool, name='carpool-create'),
    path('carpool-join/', views.joinCarpool, name='carpool-join'),
    path('carpool-search-origin/<str:origin>/', views.searchCarpoolByOrigin, name='carpool-search-origin'),
    path('carpool-view-user/<str:user>/', views.viewCarpoolByUser, name='carpool-view-user'),
    path('carpool-view-all-user/<str:user>/', views.viewJoinedCarpoolByUser, name='carpool-view-all'),
    
    path('user-update/<int:pk>/', views.updateUser, name='user-update'),
]