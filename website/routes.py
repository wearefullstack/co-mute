from flask import Blueprint, redirect, render_template, flash, request, json, jsonify
from flask_login import current_user, login_required
from forms import UpdateUserProfileForm, CarpoolRegistrationForm
from db import db, connection, cursor
from datetime import datetime

"""
Endpoints for a logged in user
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

    # fetch all carpools that the user is a member of and owner field doesnt match the user_id and username of the owner
    cursor.execute(
        """
        SELECT c.id, c.departure_time, c.arrival_time,c.origin, c.destination, c.days_available, c.available_seats, u.name,c.notes,uc.date_joined
        FROM carpool as c
        INNER JOIN user_carpool as uc ON uc.carpool_id = c.id
        INNER JOIN user as u ON u.id = uc.user_id
        WHERE uc.user_id = %s AND c.owner != %s
    """
        % (user_id, user_id)
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


@routes.route("/created_carpools")
@login_required
def created_carpools():

    # fetch all carpools that the user created
    user_id = current_user.id

    # fetch all carpools where the user is the owner
    cursor.execute(
        """
        SELECT c.id, c.departure_time, c.arrival_time,c.origin,
        c.days_available, c.destination,c.available_seats, c.owner,c.notes,u.name,c.date_created
        FROM carpool as c
        INNER JOIN user_carpool as uc ON uc.carpool_id = c.id
        INNER JOIN user as u ON u.id = uc.user_id
        WHERE u.id = %s AND c.owner = %s
    """
        % (user_id, user_id)
    )

    raw_data = cursor.fetchall()

    # convert list of tuples to 2d array
    carpools_data = [list(carpool) for carpool in raw_data]

    return render_template(
        "created_carpools.html",
        title="Created carpools",
        data=carpools_data,
        user=current_user,
    )


@routes.route("/register_new_carpool", methods=["GET", "POST"])
@login_required
def register_new_carpool():

    form = CarpoolRegistrationForm()

    if form.validate_on_submit and request.method == "POST":

        days_selected = form.days_available.data

        # check if at least 1 day checkbox is selected
        if len(days_selected) > 0:

            day_map = {
                "Monday": "Mo",
                "Tuesday": "Tu",
                "Wednesday": "We",
                "Thursday": "Th",
                "Friday": "Fr",
                "Saturday": "Sa",
                "Sunday": "Su",
            }

            days_string = ""

            # build the days_string
            for day in days_selected:
                days_string += day_map[day]

            time_now = datetime.now().date()
            date_joined = time_now.strftime("%Y-%m-%d")

            # create new carpool
            cursor.execute(
                "INSERT INTO carpool (departure_time,arrival_time,origin,days_available,destination,available_seats,owner,notes,date_created) values "
                + "(?,?,?,?,?,?,?,?,?)",
                (
                    str(form.departure_time.data),
                    str(form.arrival_time.data),
                    form.origin.data,
                    days_string,
                    form.destination.data,
                    form.available_seats.data,
                    current_user.id,
                    form.notes.data,
                    date_joined,
                ),
            )

            connection.commit()

            # create connection for owner and carpool
            cursor.execute(
                "INSERT INTO user_carpool (user_id,carpool_id,date_joined) values "
                + "(?,?,?)",
                (
                    current_user.id,
                    cursor.lastrowid,
                    date_joined,
                ),
            )

            connection.commit()

            flash("Carpool joined successfully")
            return redirect("/created_carpools")
        else:
            flash("Please select at least one day", category="invalid")

    return render_template(
        "register_new_carpool.html",
        title="Register new car pool",
        form=form,
        user=current_user,
    )


@routes.route("/search_all")
@login_required
def search_all():

    # fetch all carpools
    cursor.execute(
        """
        SELECT * FROM carpool as c
        """
    )

    raw_data = cursor.fetchall()

    # convert list of tuples to 2d array
    carpools_data = [list(carpool) for carpool in raw_data]

    return render_template(
        "search_all.html",
        title="Search Carpools",
        data=carpools_data,
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

            flash("Profile successfully updated", category="valid")

            redirect("/joined_users")  # reload the page
        else:
            flash("Error updating profile", category="invalid")

    return render_template(
        "profile.html",
        title="Update Profile",
        form=form,
        user=current_user,
    )


@routes.route("/join_carpool", methods=["POST"])
@login_required
def join_carpool():

    # check if the user is already in the carpool
    data = json.loads(request.data)
    carpool_id = data["carpool_id"]
    available_seats = data["available_seats"]

    cursor.execute(
        """
        SELECT * FROM user_carpool
        WHERE user_id = %s AND carpool_id = %s
        """
        % (current_user.id, carpool_id)
    )

    raw_data = cursor.fetchall()

    # if the user is not in the carpool
    if len(raw_data) > 0:
        flash("You are already in the carpool", category="invalid")
    else:
        if available_seats > 0:
            time_now = datetime.now().date()
            date_joined = time_now.strftime("%Y-%m-%d")

            # create connection for user and carpool
            cursor.execute(
                "INSERT INTO user_carpool (user_id,carpool_id,date_joined) values "
                + "(?,?,?)",
                (
                    current_user.id,
                    carpool_id,
                    date_joined,
                ),
            )

            connection.commit()

            # decrement the carpool seat count
            cursor.execute(
                """
                UPDATE carpool
                SET available_seats = available_seats - 1
                WHERE id = %s
                """
                % carpool_id
            )

            connection.commit()

            flash("Successfully joined carpool", category="valid")
        else:
            flash("No seats available", category="invalid")

    return jsonify({})


@routes.route("/leave_carpool", methods=["POST"])
@login_required
def leave_carpool():
    carpool_id = json.loads(request.data)["carpool_id"]

    # Delete from user_carpool table where carpool_id = carpool_id and user_id = current_user.id
    cursor.execute(
        """
        DELETE FROM user_carpool
        WHERE carpool_id = %s AND user_id = %s
        """
        % (carpool_id, current_user.id)
    )

    # increment the carpool available_seats
    cursor.execute(
        """
        UPDATE carpool
        SET available_seats = available_seats + 1
        WHERE id = %s
        """
        % carpool_id
    )

    connection.commit()

    flash("You have successfully left the carpool", category="valid")

    return jsonify({})
