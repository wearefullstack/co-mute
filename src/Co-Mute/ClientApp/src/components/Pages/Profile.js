import React, { useState, useEffect } from 'react'
import "../../styles/createTicket.scss"
import {StyledPageHeader} from '../Styled/Page.styled';

function Profile() {
    const [profileDto, setProfileDto] = useState({});  
    const [passwordShown, setPasswordShown] = useState(false);
    const [edit, setEdit] = useState(true);

    const query = "http://localhost:5196/api/User/UpdateUserDetails/" + localStorage.getItem('id');
    const token = JSON.parse(localStorage.getItem('token'));

    const handleinputChange = e => {
        setProfileDto({...profileDto, [e.target.name]: e.target.value});
        console.log(profileDto);
    }

    const togglePassword = () => {setPasswordShown(!passwordShown)};
    
    const toggleEdit = () => {setEdit(false)};

    const doUpdateDetails = async () =>{
      try{
        const response = await fetch(query, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json', 
            'Authorization': token
          },
          body: JSON.stringify(profileDto)
        });
  
        if(!response.ok)
        {
          let data = await response.json();
          throw new Error(data)
        }
  
      }
      catch(err)
      {
        console.log(err);
      }
    }
    
    
  return (
    <div className='CreateComtainer'>
    <StyledPageHeader text="My Profile"/>
      <div className='content'>
        <label>Name</label>
        <input onChange={handleinputChange} name="Name" type="text" />

        <label>Surname</label>
        <input onChange={handleinputChange} name="Surname" type="text" />

        <label>Phone No.</label>
        <input onChange={handleinputChange} name="Phone" type="text" />

        <label>Email</label>
        <input onChange={handleinputChange} name="Email" type="text" />

        <label>Password</label>
        <input onChange={handleinputChange} name="Password" type={passwordShown ? "text" : "password"} />
        <a onClick={togglePassword}>show</a>

        <button onClick={doUpdateDetails}>Save</button>
      </div>
    </div>
  )
}

export default Profile