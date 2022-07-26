from flask import Flask
from auth import auth
from routes import routes
from db import db
from models.user_carpool import UserCarpool
from models.user import User
from models.carpool import Carpool


app = Flask(__name__)
app.config["SECRET_KEY"] = "8123812838123671236as"

# connects to a SQLite db
app.config["SQLALCHEMY_DATABASE_URI"] = "sqlite:///comute-database.db"
app.config["SQLALCHEMY_TRACK_MODIFICATIONS"] = False

# creates db with tables if doesnt exist
@app.before_first_request
def create_tables():  # creates all tables , unless they exist already
    db.create_all()


if __name__ == "__main__":
    db.init_app(app)

    app.register_blueprint(auth, url_prefix="/")
    app.register_blueprint(routes, url_prefix="/")

    app.run(port=5000, debug=True)
