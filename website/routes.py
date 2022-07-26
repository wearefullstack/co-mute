from flask import Blueprint, render_template

"""
Endpoints relating to logged in user routes
"""

routes = Blueprint("routes", __name__)


@routes.route('/test')
def test():
    return 'test'
