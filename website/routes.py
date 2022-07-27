from flask import Blueprint, redirect, render_template, flash, request
from flask_login import current_user, login_required
from forms import UpdateUserProfileForm
from db import db


"""
Endpoints relating to logged in user routes
"""

routes = Blueprint("routes", __name__)


@routes.route("/joined_carpools")
@login_required
def joined_carpools():
    return render_template(
        "joined_carpools.html",
        title="Joined car pools",
        user=current_user,
    )


@routes.route("/view_all")
@login_required
def view_all():
    return render_template(
        "view_all.html",
        title="All created car pools",
        user=current_user,
    )


@routes.route("/profile", methods=["GET", "POST"])
@login_required
def profile():
    form = UpdateUserProfileForm()

    if request.method == "GET":
        print('helllo')
        # fill in form details , leave out password
        form.name.data = current_user.name
        form.surname.data = current_user.surname
        form.phone.data = current_user.phone
        form.email.data = current_user.email

        redirect("/joined_users") 

    if request.method == "POST":
        if form.validate_on_submit():  # if all fields valid

            # update session current_user variable details
            current_user.name = form.name.data
            current_user.surname = form.surname.data
            current_user.phone = form.phone.data
            current_user.email = form.email.data
            current_user.password = form.password.data
            db.session.commit()

            flash("Profile successfully updated")

            redirect("/joined_users")  # reload the page
        else:
            print("error updating profile!")

    return render_template(
        "profile.html",
        title="Update Profile",
        form=form,
        user=current_user,
    )
