from rest_framework import serializers
from .models import *

class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = '__all__'
    

class CarpoolSerializer(serializers.ModelSerializer):
    user = serializers.CharField(source='User')

    class Meta:
        model = Carpool
        fields = ['id', 'DepartureTime', 'ExpectedArrivalTime', 'Origin', 'DaysAvailable', 'Destination', 'SeatsAvailable', 'user', 'Notes']

    def create(self, validated_data):
        user_data = validated_data.pop('User')
        user = User.objects.get(name=user_data)
        carpool = Carpool.objects.create(User=user, **validated_data)
        return carpool

    def update(self, instance, validated_data):
        user_data = validated_data.pop('User')
        user = User.objects.get(name=user_data)
        instance.DepartureTime = validated_data.get('DepartureTime', instance.DepartureTime)
        instance.ExpectedArrivalTime = validated_data.get('ExpectedArrivalTime', instance.ExpectedArrivalTime)
        instance.Origin = validated_data.get('Origin', instance.Origin)
        instance.DaysAvailable = validated_data.get('DaysAvailable', instance.DaysAvailable)
        instance.Destination = validated_data.get('Destination', instance.Destination)
        instance.SeatsAvailable = validated_data.get('SeatsAvailable', instance.SeatsAvailable)
        instance.User = user
        instance.Notes = validated_data.get('Notes', instance.Notes)
        instance.save()
        return instance


class PassengerSerializer(serializers.ModelSerializer):
    user = serializers.CharField(source='User')
    carpool = serializers.CharField(source='Carpool')

    class Meta:
        model = Passenger
        fields = '__all__'

    def create(self, validated_data):
        user_data = validated_data.pop('User')
        user = User.objects.get(name=user_data)

        carpool_data = validated_data.pop('Carpool')
        carpool = Carpool.objects.get(name=carpool_data)
        
        passenger = Passenger.objects.create(User=user, Carpool=carpool)
        carpool.SeatsAvailable -= 1
        return passenger

    def update(self, instance, validated_data):
        instance.User = User.objects.get(id=validated_data['User'])
        instance.Carpool = Carpool.objects.get(id=validated_data['Carpool'])
        instance.save()
        return instance

    def delete(self, instance):
        instance.Carpool.SeatsAvailable += 1
        instance.Carpool.save()
        instance.delete()
        return instance