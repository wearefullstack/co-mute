from db import db


class UserCarpool(db.Model):

    __tablename__ = "user_carpool"
    # id
    # user_id
    # carpool_id
    # dateJoined
    carpool_id = db.Column(db.Integer, db.ForeignKey("carpool.id"), primary_key=True)
    user_id = db.Column(db.Integer, db.ForeignKey("user.id"), primary_key=True)
    users = db.relationship("User", back_populates="carpools")
    carpools = db.relationship("Carpool", back_populates="users")
