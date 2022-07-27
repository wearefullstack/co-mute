from flask import Blueprint, render_template, redirect
from flask_login import current_user, login_required

"""
Endpoints relating to logged in user routes
"""

routes = Blueprint("routes", __name__)


@routes.route("/joined_carpools")
@login_required
def joined_carpools():
    return render_template(
        "joined_carpools.html", title="Joined car pools", user=current_user
    )


@routes.route("/view_all")
@login_required
def view_all():
    return render_template(
        "view_all.html", title="All created car pools", user=current_user
    )


@routes.route("/profile")
@login_required
def profile():
    return render_template("profile.html", title="Profile", user=current_user)


@routes.route("/edit_profile")
@login_required
def edit_profile():
    return render_template("edit_profile.html", title="Edit Profile", user=current_user)
