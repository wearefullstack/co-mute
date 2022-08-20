import React, { useState, useContext } from 'react';
import { Routes, Route, Link, Navigate } from "react-router-dom";
import UserContextProvider , {UserContext} from './UserContext';


import {useNavigate} from 'react-router-dom';


function MyForm() {
  const [origin, setOrigin] = useState('');
  const [departureTime, setDepartureTime] = useState('');
  const [destination, setDestination] = useState('');
  const [ExpectedArrivalTime, setExpectedArrivalTime] = useState('');
  const [availableSeats, setavailableSeats] = useState('');
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  const userc= useContext(UserContext)

  const handleSubmit = (event) => {

    event.preventDefault();     
    console.log("I am ",event);

    const data = { firstname: firstName, lastname: lastName,
         email: email, password: password };

         userc.setUser(data);

         console.log(data);
//         http://localhost:8080/api/

    fetch('http://localhost:8080/createpool', {
    method: 'POST', // or 'PUT'
    headers: {
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
    })
    .then((response) => response.json())
    .then((data) => {
    console.log('Success:', data);
    })
    .catch((error) => {
    console.error('Error:', error);
    });

    setFirstName("");
    setLastName("");
    setEmail("");
    setPassword("");

    alert(`Hello ${firstName} ${lastName} account created successfully`);
    navigate('/carpool');


  }

  return (
    <><UserContextProvider value={[user, setUser]} ></UserContextProvider><form onSubmit={handleSubmit}>
      <label>Origin </label>
      <input
        onChange={e => setFirstName(e.target.value)}
        value={firstName} /><br />
      <label>Destination </label>
      <input
        onChange={e => setLastName(e.target.value)}
        value={lastName} /><br />
      <label>departureTime </label>
      <input
        onChange={e => setEmail(e.target.value)}
        value={email} /><br />
        <label>available seats </label>
      <input
        onChange={e => setEmail(e.target.value)}
        value={email} /><br />
      <label>ArrivalTime </label>
      <input
        onChange={e => setPassword(e.target.value)}
        value={password} /><br />
      <input type="submit" value="Press me" />
      <input type="cancel" value="Cancel me" />
    </form></>
    
  );
}
export default MyForm;