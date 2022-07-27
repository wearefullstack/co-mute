from flask import Blueprint, redirect, render_template, flash, request
from flask_login import current_user, login_required
from forms import UpdateUserProfileForm, CarpoolRegistrationForm
from models.carpool import Carpool
from models.user_carpool import UserCarpool
from models.user import User
from db import db
import datetime

"""
Endpoints relating to logged in user routes
"""

routes = Blueprint("routes", __name__)

# convert the string date_joined into a datetime object
def convertDate(strDate):
    format = "%Y-%m-%d %H:%M:%S.%f"
    convertedDate = datetime.datetime.strptime(strDate, format)
    return convertedDate


@routes.route("/joined_carpools")
@login_required
def joined_carpools():

    user = User.query.filter_by(id=current_user.id).first()
    user_carpools = UserCarpool.query.filter_by(user_id=user.id).all()

    """
    [
        Date Joined
        Starting Location
        Ending Location
        Starting Time
        Ending Time
        Seats 
        Car Owner  
    ]
    """

    carpools = []
    # # get list of joined carpools
    # for carpool in user_carpools:
    #     converted_date = convertDate(str(carpool.date_joined))
    #     carpools.append([carpool, str(converted_date.date())])

    # for relationship in user_carpools:
    #     carpools.append([relationship.carpools.id,relationship.carpools.departure_time,relationship.carpools.departure_time,])

    print(carpools)

    # print(carpools)

    # for i in user_carpools:
    #     converted_date = convertDate(str(i.date_joined))
    #     carpools.append(str(converted_date.date()))

    # print(carpools)

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

        # add new carpool and retrieve id
        db.session.add(new_carpool)
        db.session.commit()

        # fetch user details from db
        user = User.query.filter_by(id=current_user.id).first()

        # for linking the owner to the carpool in the joining table (UserCarpool)
        user_carpool = UserCarpool(carpool_id=new_carpool.id, user_id=user.id)

        # add link of user to carpool
        db.session.add(user_carpool)
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
