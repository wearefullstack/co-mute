import React, { useCallback, useState } from "react";
import '../../App.css'
import regLogo from '../../Images/register.svg';
import Labeled from "../Labeled";
import { Button, notification } from 'antd'
import { IError, stringExists } from "../../Utils";
import APIManager from "../../Managers/APIManager";
import UserManager from "../../Managers/UserManager";
import { useNavigate } from 'react-router-dom';

interface IForm {
    name: string,
    surname: string,
    email: string,
    phone: string
    password: string,
    confirm_password: string
}

const EMPTY_FORM: IForm = { name: "", surname: "", phone: "", email: "", password: "", confirm_password: "" }

export default
function Register(){
    const [ form, setForm] = useState<Partial<IForm>>(EMPTY_FORM);
    const [ errors, setErrors] = useState<IError<IForm>>({});
    const [ isLoading, setIsLoading] = useState<boolean>(false);
    const navigate = useNavigate();

    const register = ()=>{
        console.log(":reg")
        if(isLoading) return;

        const _errors = validateForm(form);
        if(!_errors){
            
            setIsLoading(true);
            APIManager.getInstance().registerUser(form as IForm)
            .then(({ result }: any) => {
                UserManager.getInstance().setActiveUser(result);
                navigate("/")
            })
            .catch((error: any)=> {
                console.log("e", error);
                notification.error({
                    message: error.message,
                    description: "",
                    placement: "top",
                    duration: 0
                  });
            })
            .finally(() => setIsLoading(false));


            setErrors({});
        }else{
            setErrors(_errors);
        }
    };

    function onInputChange(name: keyof IForm){
        return (event: any) => {
            setForm((_form) => ({..._form, [name]: event.target.value }));
        }
    }

    return (
        <div className="main-container">
            <div className="form">
                <img className="form-icon" src={ regLogo }/>
                <h2 className="form-title">Register</h2>
                <Labeled title={"Name *"} error={ errors.name }>
                    <input onChange={ onInputChange("name")} value={ form.name }/>
                </Labeled>
                <Labeled title={"Surname *"} error={ errors.surname }>
                    <input onChange={ onInputChange("surname")} value={ form.surname }/>
                </Labeled>
                <Labeled title={"Email *"} error={ errors.email }>
                    <input onChange={ onInputChange("email")} value={ form.email }/>
                </Labeled>
                <Labeled title={"Phone"} error={ errors.phone }>
                    <input onChange={ onInputChange("phone")} value={ form.phone }/>
                </Labeled>
                <Labeled title={"Password *"} error={ errors.password }>
                    <input onChange={ onInputChange("password")} value={ form.password }/>
                </Labeled>
                <Labeled title={"Confirm Password *"} error={ errors.confirm_password }>
                    <input onChange={ onInputChange("confirm_password")} value={ form.confirm_password }/>
                </Labeled>
                <h2 onClick={register }>click me</h2>
                <Button onClick={register} style={{ padding: 8, margin: 16, alignSelf: "end"}} type="primary"  loading>
                    Register
                 </Button>
            </div>
        </div>
    );
}

function validateForm({ name, surname, email, phone, password, confirm_password }: Partial<IForm>): IError<IForm> | null {
    const errors: IError<IForm> = {};

    if(!stringExists(name)){
        errors["name"] = "required"
    }

    if(!stringExists(surname)){
        errors["surname"] = "required"
    }

    if(!stringExists(email)){
        errors["email"] = "required"
    }else if(!ValidateEmail(email)){
        errors["email"] = "invalid email address"
    }


    if(!stringExists(password)){
        errors["password"] = "required"
    }

    if(!stringExists(confirm_password)){
        errors["confirm_password"] = "required"
    }else if(confirm_password !== password){
        errors["confirm_password"] = "passwords don't match"
    }

    return (Object.keys(errors).length === 0) ? null : errors;
}

function ValidateEmail(value: any) {
    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
  
    if (value.match(validRegex)) {
      return true;
    } else {
      return false; 
    }
  
  }




 