from db import db
from datetime import datetime


class Carpool(db.Model):

    __tablename__ = "carpool"

    id = db.Column(db.Integer, primary_key=True)
    departure_time = db.Column(db.String(30))
    arrival_time = db.Column(db.String(30))
    origin = db.Column(db.String(30))
    days_available = db.Column(db.String(30))
    destination = db.Column(db.String(30))
    arrival_seats = db.Column(db.String(30))
    owner = db.Column(db.String(30))
    notes = db.Column(db.String(30))

    # required when viewing car pools
    # "Logged in users should be view all carpools and the date they were created"
    date_created = db.Column(db.DateTime(timezone=True), default=datetime.now())

    # users = db.relationship("UserCarpool", back_populates="carpools")
    # carpools = db.relationship("UserCarpool", back_populates="users")
