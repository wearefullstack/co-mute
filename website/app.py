from flask import Flask
from auth import auth
from routes import routes


app = Flask(__name__)
app.config["SECRET_KEY"] = "8123812838123671236as"


if __name__ == "__main__":
    
    app.register_blueprint(auth, url_prefix="/")
    app.register_blueprint(routes, url_prefix="/")

    app.run(port=5000, debug=True)
