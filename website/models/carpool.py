from sqlalchemy import ForeignKey
from db import db
from datetime import datetime

class Carpool(db.Model):

    __tablename__ = "carpool"

    id = db.Column(db.Integer, primary_key=True)

    departure_time = db.Column(db.String(50))
    arrival_time = db.Column(db.String(50))
    origin = db.Column(db.String(30))  # starting area
    days_available = db.Column(db.String(30))
    destination = db.Column(db.String(30))  # ending area
    available_seats = db.Column(db.String(30))
    owner = db.Column(db.String(30),ForeignKey('user.id'))
    notes = db.Column(db.String(150))
    date_created = db.Column(db.String(50))

    users = db.relationship("UserCarpool", back_populates="carpools")
