from flask import Blueprint, render_template
from forms import UserRegistrationForm, UserLoginForm

"""
Handles endpoints relating to authentication
"""

auth = Blueprint("auth", __name__)


@auth.route("/login", methods=["GET", "POST"])
def loginUser():

    form = UserLoginForm()

    if form.validate_on_submit():  # if all fields valid
        print("success!")
    else:
        print("failure")

    return render_template("auth/login.html", title="Login", form=form)


@auth.route("/register", methods=["GET", "POST"])
def registerUser():
    form = UserRegistrationForm()

    if form.validate_on_submit():  # if all fields valid
        print("success!")
    else:
        print("failure")

    return render_template("auth/register.html", title="Register", form=form)
