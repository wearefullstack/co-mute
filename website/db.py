from flask_sqlalchemy import SQLAlchemy

db = SQLAlchemy()


import sqlite3

connection = sqlite3.connect("comute-database.db", check_same_thread=False)
cursor = connection.cursor()
