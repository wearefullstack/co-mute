from django.shortcuts import render
from rest_framework import status

from rest_framework.decorators import api_view
from rest_framework.response import Response

from .models import *
from .serializers import *

# Create your views here.
@api_view(['GET'])
def apiOverview(request):
    api_urls = {
        'user-list/': '/api/user-list/',
        'user-create/': '/api/user-create/',
        'carpool-create/': '/api/carpool-create/',
        'carpool-join/': '/api/carpool-join/',
        'carpool-search-origin/': '/api/carpool-search-origin/<str:origin>/',
        'carpool-view-user/': '/api/carpool-view-user/<str:user>/',
        'carpool-view-all-user/': '/api/carpool-view-all-user/<str:user>/',
        
        'user-update/': '/api/user-update/<int:pk>/',
    }
    return Response(api_urls)

#Get all Users
@api_view(['GET'])
def userList(request):
    users = User.objects.all()
    serializer = UserSerializer(users, many=True, context={'request': request})
    return Response(serializer.data)


# Create User
@api_view(['POST'])
def createUser(request):
    if request.method == 'POST':
        serializer = UserSerializer(data=request.data)
        if serializer.is_valid(raise_exception=True):
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

# Create Carpool
@api_view(['POST'])
def createCarpool(request):
    if request.method == 'POST':
        serializer = CarpoolSerializer(data=request.data)
        if serializer.is_valid(raise_exception=True):
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

#Join Carpool
@api_view(['POST'])
def joinCarpool(request):
    if request.method == 'POST':
        serializer = PassengerSerializer(data=request.data)
        if serializer.is_valid(raise_exception=True):
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

#Search Carpool by Origin
@api_view(['GET'])
def searchCarpoolByOrigin(request, origin):
    carpools = Carpool.objects.filter(Origin=origin)
    serializer = CarpoolSerializer(carpools, many=True, context={'request': request})
    return Response(serializer.data)

#View All Carpools by User
@api_view(['GET'])
def viewCarpoolByUser(request, user):
    carpools = Carpool.objects.filter(User=user)
    serializer = CarpoolSerializer(carpools, many=True, context={'request': request})
    return Response(serializer.data)

#View All Joined Carpools by User
@api_view(['GET'])
def viewJoinedCarpoolByUser(request, user):
    passengers = Passenger.objects.filter(User=user)
    serializer = PassengerSerializer(passengers, many=True, context={'request': request})
    return Response(serializer.data)

#Login a user by email and password
# @api_view(['POST'])
# def loginUser(request):
#     if request.method == 'POST':
#         serializer = UserSerializer(data=request.data)
#         if serializer.is_valid(raise_exception=True):
#             serializer.save()
#             return Response(serializer.data, status=status.HTTP_201_CREATED)
#         return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

#Update User details by id
@api_view(['POST'])
def updateUser(request, pk):
    user = User.objects.get(pk=pk)
    serializer = UserSerializer(instance=user, data=request.data)
    if serializer.is_valid(raise_exception=True):
        serializer.save()
        return Response(serializer.data)
    return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)