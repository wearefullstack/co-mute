import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import '../../styles/login.scss'

function Register() {
    const baseUrl = process.env.API_URL;
    console.log(baseUrl);
    const [registerDto, setRegisterDto] = useState({});
    const navigate = useNavigate();

    const handleinputChange = e => {
        setRegisterDto({...registerDto, [e.target.name]: e.target.value});
        console.log(registerDto);
    }

    const SignUp = async () => {

      const response = await fetch('http://localhost:5196/api/User/RegisterNewUser', {
        method: 'POST',
        body: JSON.stringify(registerDto),
        headers : { 
          'Content-Type': 'application/json',
          'Accept': 'application/json'
         }
      });
  
      if(!response.ok)
      {
        throw new Error(response.status);
      }
      console.log(response.json());
      navigate("/");
    }
  
        
  return (
    <div className='Page'>
      <div className='Card'>
        <h1 className='Header'>Ready to Co-Mute</h1>
        <h5>Register now</h5>
        <label className='Label'>Name</label>
        <input className='TextBox' onChange={handleinputChange} name="Name" type="text" required="true"/>

        <label className='Label'>Surname</label>
        <input className='TextBox' onChange={handleinputChange} name="Surname" type="text" required="true"/>

        <label className='Label'>Phone No.</label>
        <input className='TextBox' onChange={handleinputChange} name="Phone" type="text"/>

        <label className='Label'>Email</label>
        <input className='TextBox' onChange={handleinputChange} name="Email" type="text" required="true"/>

        <label className='Label'>Password</label>
        <input className='TextBox' onChange={handleinputChange} name="Password" type="text" required="true"/>
        <br/>
        <button className='Button' onClick={SignUp}>Register</button>
        <br/>
        <Link className='Link' to="/">Sign in to Co-Mute</Link>
      </div>
    </div>
  )
}

export default Register