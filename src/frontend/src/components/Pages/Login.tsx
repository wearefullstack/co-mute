import React, { useCallback, useState } from "react";
import '../../App.css'
import regLogo from '../../Images/register.svg';
import Labeled from "../Labeled";
import {  notification } from 'antd'
import { IError, stringExists } from "../../Utils";
import APIManager from "../../Managers/APIManager";
import UserManager from "../../Managers/UserManager";
import { useNavigate } from 'react-router-dom';
import Button from "../Button";
import Divider from "../Divider";

interface IForm {
    email: string,
    password: string
}

const EMPTY_FORM: IForm = {  email: "", password: "" }

export default
function Login(){
    const [ form, setForm] = useState<Partial<IForm>>(EMPTY_FORM);
    const [ errors, setErrors] = useState<IError<IForm>>({});
    const [ isLoading, setIsLoading] = useState<boolean>(false);
    const navigate = useNavigate();

    const login = ()=>{
        if(isLoading) return;

        const _errors = validateForm(form);
        if(!_errors){
            const { email,password} = form as IForm;
            setIsLoading(true);
            APIManager.getInstance().loginUser(email, password)
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
                <h2 className="form-title">CO-MUTE</h2>
                <h2 className="form-sub-title">Login</h2>
                <Labeled title={"Email *"} error={ errors.email }>
                    <input onChange={ onInputChange("email")} value={ form.email }/>
                </Labeled>
                <Labeled title={"Password *"} error={ errors.password }>
                    <input onChange={ onInputChange("password")} value={ form.password }/>
                </Labeled>
                <Divider/>
                <Button onClick={login} title="Login"></Button>
              
            </div>
        </div>
    );
}

function validateForm({ email, password }: Partial<IForm>): IError<IForm> | null {
    const errors: IError<IForm> = {};

    if(!stringExists(email)){
        errors["email"] = "required"
    }else if(!ValidateEmail(email)){
        errors["email"] = "invalid email address"
    }


    if(!stringExists(password)){
        errors["password"] = "required"
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




 