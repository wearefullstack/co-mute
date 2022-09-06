import React, { useState } from 'react'

function JoinCarPoolTicket({data, Action}) {

  const query = "http://localhost:5196/api/CarPoolTickets/JoinCarPoolTicket/"+data.id;
  const token = JSON.parse(localStorage.getItem('token'));  

  const [ startApply, setSartApply] = useState(false);
  const [applyPassengerNote, setApplyPassengerNote] = useState({OwnerId: localStorage.getItem('id')});

  const toggleConfirm = () => {
    setSartApply(!startApply);
  }

  const doTicketApplication = async (id) =>{
    try{
      const response = await fetch(query, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json', 
          'Authorization': token
        },
        body: JSON.stringify(applyPassengerNote)
      });

      if(!response.ok)
      {
        let data = await response.json();
        throw new Error(data)
      }
      Action(id);
    }
    catch(err)
    {
      console.log(err);
    }
  }

  const handleChange = (e) => {
    setApplyPassengerNote({...applyPassengerNote, [e.target.name]: e.target.value});  
    console.log(applyPassengerNote);
  }

  return (

        <div className='TicketDetail'>
            <h4>{data.fullName}</h4><label className=''>{data.status}</label>
            
            <p>Start Time: {data.departureTime}</p> <p>Expected Arrival Time: {data.expectedArrivalTime}</p>
            <p>From: {data.origin}</p> <p>To: {data.destination}</p>
            <p>Seats Available: {data.availableSeats}</p> <p>Days Available: {data.daysAvailable}</p>
            <p>Note: {data.note}</p>
            <button onClick={toggleConfirm}>{startApply? "Cancel" : "Apply"}</button>
            {
              startApply?
              <div>
                <p>Add Note</p>
                <input type="textArea" name="PassengerNote" onChange={handleChange}/>
                <button onClick={doTicketApplication}>Confirm</button>
              </div> :
              null
            }
        </div>

  )
}

export default JoinCarPoolTicket