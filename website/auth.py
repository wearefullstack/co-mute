from inspect import currentframe
from flask import Blueprint, redirect, render_template, flash, url_for
from flask_login import current_user, login_required, logout_user, login_user
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
        user = User.query.filter_by(
            email=form.email.data, password=form.password.data
        ).first()

        if user:
            login_user(user, remember=True)  # set active logged in user for flask-login
            flash('Successfully logged in')
            return redirect("/joined_carpools")

    return render_template(
        "auth/login.html", title="Login", form=form, user=current_user
    )


@auth.route("/register", methods=["GET", "POST"])
def registerUser():
    form = UserRegistrationForm()

    # if all fields valid
    if form.validate_on_submit():

        new_user = User(
            name=form.name.data,
            surname=form.email.data,
            phone=form.phone.data,
            email=form.email.data,
            password=form.password.data,
        )

        db.session.add(new_user)
        db.session.commit()

        # log in new created user

        user = User.query.filter_by(
            email=form.email.data, password=form.password.data
        ).first()

        login_user(user, remember=True)
        flash("Account successfully created")
        redirect("/joined_carpools")

    return render_template(
        "auth/register.html", title="Register", form=form, user=current_user
    )


@login_required
@auth.route("/logout")
def logout():
    logout_user()
    return redirect("/login")
