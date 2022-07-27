from db import db
from datetime import datetime


class UserCarpool(db.Model):

    __tablename__ = "user_carpool"
    carpool_id = db.Column(db.Integer, db.ForeignKey("carpool.id"), primary_key=True)
    user_id = db.Column(db.Integer, db.ForeignKey("user.id"), primary_key=True)

    # auto created , when a user joins a car pool
    date_joined = db.Column(db.DateTime(timezone=True), default=datetime.now())

    users = db.relationship("User", back_populates="carpools")
    carpools = db.relationship("Carpool", back_populates="users")

    def __repr__(self):
        return f"<UserCarpool carpool_id={self.carpool_id} user_id={self.user_id} date_joined={self.date_joined} >"
