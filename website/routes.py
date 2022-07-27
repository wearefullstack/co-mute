from flask import Blueprint, redirect, render_template, flash, request
from flask_login import current_user, login_required
from forms import UpdateUserProfileForm, CarpoolRegistrationForm
from models.carpool import Carpool
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


@routes.route("register_new_carpool", methods=["GET", "POST"])
@login_required
def register_new_carpool():

    form = CarpoolRegistrationForm()

    if form.validate_on_submit and request.method == "POST":
        print('hello')
        new_carpool = Carpool(
            departure_time=str(form.departure_time.data),
            arrival_time=str(form.arrival_time.data),
            origin=form.origin.data,
            days_available=form.days_available.data,
            destination=form.destination.data,
            available_seats=form.available_seats.data,
            owner=current_user.id,
            notes=form.notes.data,
        )

        db.session.add(new_carpool)
        db.session.commit()

        flash("Carpool registered")
        return redirect("/joined_carpools")

    return render_template(
        "register_new_carpool.html",
        title="Register new car pool",
        form=form,
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
        # fill in form details , leave out password
        form.name.data = current_user.name
        form.surname.data = current_user.surname
        form.phone.data = current_user.phone
        form.email.data = current_user.email

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
