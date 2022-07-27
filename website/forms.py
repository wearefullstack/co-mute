from flask_login import current_user
from flask_wtf import FlaskForm
from wtforms import (
    SubmitField,
    StringField,
    PasswordField,
    EmailField,
    IntegerField,
    TimeField,
)
from wtforms.validators import DataRequired, Email, Length, ValidationError, NumberRange
from models.user import User


class UserRegistrationForm(FlaskForm):

    name = StringField("Name", validators=[DataRequired(), Length(min=2, max=30)])
    surname = StringField("Surname", validators=[DataRequired(), Length(min=2, max=30)])
    phone = StringField(
        "Phone Number", validators=[DataRequired(), Length(min=10, max=10)]
    )
    email = StringField("Email", validators=[DataRequired(), Email()])
    password = PasswordField(
        "Password", validators=[DataRequired(), Length(min=2, max=30)]
    )
    submit = SubmitField("Register")

    # assigned to the email field , by identifying a variable name after _
    def validate_email(self, email):
        user = User.query.filter_by(email=email.data).first()
        if user:
            raise ValidationError("Email already exists")


class UserLoginForm(FlaskForm):
    email = EmailField("Email", validators=[DataRequired(), Email()])
    password = PasswordField("Password", validators=[DataRequired()])
    submit = SubmitField("Login")


class UpdateUserProfileForm(FlaskForm):
    name = StringField("Name", validators=[DataRequired(), Length(min=2, max=30)])
    surname = StringField("Surname", validators=[DataRequired(), Length(min=2, max=30)])
    phone = StringField(
        "Phone Number", validators=[DataRequired(), Length(min=10, max=10)]
    )
    email = StringField("Email", validators=[DataRequired(), Email()])
    password = PasswordField(
        "Password", validators=[DataRequired(), Length(min=2, max=30)]
    )
    submit = SubmitField("Update")

    def validate_email(self, email):

        if current_user.email != email.data:
            user = User.query.filter_by(email=email.data).first()
            if user:
                raise ValidationError("Email already exists")


class CarpoolRegistrationForm(FlaskForm):

    departure_time = TimeField("Departure Time", validators=[DataRequired()])
    arrival_time = TimeField("Expected Arrival", validators=[DataRequired()])
    origin = StringField("Origin", validators=[DataRequired()])
    days_available = StringField(
        "Days Available", validators=[DataRequired()]
    )  # M,T,W,Th,F,Sa,Su

    destination = StringField("Destination", validators=[DataRequired()])
    available_seats = IntegerField(
        "Available Seats", validators=[DataRequired(), NumberRange(min=2)]
    )
    notes = StringField("Notes", validators=[DataRequired()])
    submit = SubmitField("Register Carpool")

    # def validate_time(self, departure_time, arrival_time):
    #     # if departure_time ->  arrival_time clash with any other carpool the owner has joined
    #     pass
