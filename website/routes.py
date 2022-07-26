from flask import Blueprint, render_template

"""
Endpoints relating to logged in user routes
"""

routes = Blueprint("routes", __name__)


@routes.route("/joined_carpools")
def joined_carpools():
    return render_template("joined_carpools.html", title="Joined car pools")


@routes.route("/view_all")
def view_all():
    return render_template("view_all.html", title="All created car pools")


@routes.route("/profile")
def profile():
    return render_template("profile.html", title="Profile")


@routes.route("/edit_profile")
def edit_profile():
    return render_template("edit_profile.html", title="Edit Profile")
