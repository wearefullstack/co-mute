from db import db
from datetime import datetime


# class UserCarpool(db.Model):

#     __tablename__ = "user_carpool"
#     carpool_id = db.Column(db.Integer, db.ForeignKey("carpool.id"), primary_key=True)
#     user_id = db.Column(db.Integer, db.ForeignKey("user.id"), primary_key=True)

#     # User must view all car-pool oppurtunities and whatdate they joined them on.
#     date_joined = db.Column(db.DateTime(timezone=True), default=datetime.now())

#     users = db.relationship("User", back_populates="carpools")
#     carpools = db.relationship("Carpool", back_populates="users")
