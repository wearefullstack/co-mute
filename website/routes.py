from flask import Blueprint, redirect, render_template, flash, request, json, jsonify
from flask_login import current_user, login_required
from forms import UpdateUserProfileForm, CarpoolRegistrationForm, LeaveCarPoolForm
from models.carpool import Carpool
from models.user_carpool import UserCarpool
from models.user import User
from db import db, connection, cursor
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

    # fetch all carpools that the user is a member of

    user_id = current_user.id

    cursor.execute(
        """
        SELECT c.id ,c.departure_time, c.arrival_time,c.origin,
        c.days_available,c.destination,c.available_seats,c.owner,c.notes
        FROM carpool as c
        INNER JOIN user_carpool as uc ON uc.carpool_id = c.id
        INNER JOIN user as u ON u.id = uc.user_id
        WHERE u.id = %s
    """
        % user_id
    )

    raw_data = cursor.fetchall()

    # convert list of tuples to 2d array
    carpools_data = [list(carpool) for carpool in raw_data]

    return render_template(
        "joined_carpools.html",
        title="Joined car pools",
        data=carpools_data,
        user=current_user,
    )


@routes.route("/leave_carpool", methods=["POST"])
@login_required
def leave_carpool():
    carpool_id = json.loads(request.data)['carpool_id']

    # Delete from user_carpool table where carpool_id = carpool_id and user_id = current_user.id
    cursor.execute(
        """
        DELETE FROM user_carpool
        WHERE carpool_id = %s AND user_id = %s
    """
        % (carpool_id, current_user.id)
    )
    
    connection.commit()

    flash('You have successfully left the carpool')

    return jsonify({})


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
