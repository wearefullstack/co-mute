from tokenize import String
from flask_wtf import FlaskForm
from wtforms import SubmitField, StringField, PasswordField, EmailField
from wtforms.validators import DataRequired, Email, Length, ValidationError
from models.user import User


class UserRegistrationForm(FlaskForm):

    name = StringField("Name", validators=[DataRequired(), Length(min=2, max=30)])
    surname = StringField("Surname", validators=[DataRequired(), Length(min=2, max=30)])
    phone = StringField("Phone Number", validators=[DataRequired()])
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

    # todo:
    # improved validation on data inputed
    # check if email already exists in db


class UserLoginForm(FlaskForm):
    email = EmailField("Email", validators=[DataRequired(), Email()])
    password = PasswordField("Password", validators=[DataRequired()])
    submit = SubmitField("Login")
