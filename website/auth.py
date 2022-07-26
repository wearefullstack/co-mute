from flask import Blueprint, render_template, flash
from forms import UserRegistrationForm, UserLoginForm
from models.user import User
from db import db


"""
Handles endpoints relating to authentication
"""

auth = Blueprint("auth", __name__)


@auth.route("/")
@auth.route("/login", methods=["GET", "POST"])
def loginUser():

    form = UserLoginForm()

    if form.validate_on_submit():  # if all fields valid
        print("success")
    else:
        print("failure")

    return render_template("auth/login.html", title="Login", form=form)


@auth.route("/register", methods=["GET", "POST"])
def registerUser():
    form = UserRegistrationForm()

    if form.validate_on_submit():  # if all fields valid
        # todo , encrypt password with bcrypt
        new_user = User(
            name=form.name.data,
            surname=form.email.data,
            phone=form.phone.data,
            email=form.email.data,
            password=form.password.data,
        )

        db.session.add(new_user)
        db.session.commit()

        flash("Success , your account has been created")
    else:
        print("failure")

    return render_template("auth/register.html", title="Register", form=form)
