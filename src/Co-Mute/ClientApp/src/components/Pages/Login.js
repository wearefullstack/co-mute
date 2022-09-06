import React, { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { LoginContext } from '../Context/LoginContext';
import '../../styles/login.scss'


function Login() {
  const [passwordShown, setPasswordShown] = useState(false);
  const [loginDto, setloginDto] = useState({});
  const loggedInStatus = useContext(LoginContext);
  const navigate = useNavigate();

  const togglePassword = () => {setPasswordShown(!passwordShown)};

  const handleinputChange = e => {
    setloginDto({...loginDto, [e.target.name]: e.target.value});

  }

  const SignIn = async () => {

    const response = await fetch('http://localhost:5196/api/User/LoginUser', {
      method: 'POST',
      body: JSON.stringify(loginDto),
      headers : { 
        'Content-Type': 'application/json',
        'Accept': 'application/json'
       }
    });

    if(!response.ok)
    {
      throw new Error(response.status);
    }
    let data = await response.json();
    await localStorage.setItem('id', JSON.stringify(data.userId));
    await localStorage.setItem('token', JSON.stringify(`bearer ${data.token}`));
    loggedInStatus.setLoggedIn(true);
    navigate("/");
  }

  return (
    

    <div className='Page'>
      <div className='Card'>
        <h1 className='Header'> Co-Mute</h1>
        <label className='Label'>Email</label>
        <input className='TextBox' onChange={handleinputChange} name="Email" type="email" required/>

        <label className='Label'>Password</label>
        <div className='Password'>
          <input className='paswordInput' onChange={handleinputChange} name="Password" type={passwordShown ? "text" : "password"} required/>
          <button className='Button' onClick={togglePassword}>show</button>
        </div>
          
        <br/>
        <button className='Button' onClick={SignIn}>Sign in</button>
        <br/>
        <Link className='Link' to="/Register">Register to Co-Mute</Link>
      </div>
    </div>
    
  )
}

export default Login