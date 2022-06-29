from django.db import models

# Create your models here.
class User(models.Model):
    name = models.CharField(max_length=50)
    surname = models.CharField(max_length=50)
    phone = models.CharField(max_length=10, default='')
    email = models.EmailField(max_length=50)
    password = models.CharField(max_length=8)
    
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)

    def __str__(self):
        return self.name + ' ' + self.surname

class Carpool(models.Model):
    DepartureTime = models.DateTimeField()
    ExpectedArrivalTime = models.DateTimeField()
    Origin = models.CharField(max_length=50)
    DaysAvailable = models.PositiveIntegerField()
    Destination = models.CharField(max_length=50)
    SeatsAvailable = models.PositiveIntegerField()
    User = models.ForeignKey(User, on_delete=models.CASCADE)
    Notes = models.CharField(max_length=50)

    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)

    def __str__(self):
        return self.Origin + ' ---> ' + self.Destination

class Passenger(models.Model):
    User = models.ForeignKey(User, on_delete=models.CASCADE)
    Carpool = models.ForeignKey(Carpool, on_delete=models.CASCADE)

    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)

    def __str__(self):
        return self.User.name + ' ' + self.User.surname + ' === ' + self.Carpool.Destination + ' ---> ' + self.Carpool.Origin

    # def save(self):
    #     if self.User.id == self.Carpool.User.id:
    #         raise Exception("You can't join your own carpool")
    #     else:
    #         self.Carpool.SeatsAvailable -= 1
    #         self.Carpool.save()
    #         print(f"Available seats {self.Carpool.SeatsAvailable}")
    #         super().save()
    #         print(f"{self.User.name} Successfully joined carpool")

    # def delete(self):
    #     self.Carpool.SeatsAvailable += 1
    #     self.Carpool.save()
    #     print(f"Available seats {self.Carpool.SeatsAvailable}")
    #     super().delete()
    #     print(f"{self.User.name} Successfully left carpool")