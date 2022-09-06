import React, { useState } from 'react'
import '../../styles/myCommutes.scss'

function CarPoolTicket({data, Action}) {
    const [showPassengers, setShowPassengers] = useState(false);

    const query = "http://localhost:5196/api/CarPoolTickets/CancelJoinCarPoolTicket/"+data.id; 

    const showAppliedPassengers = () => {
        setShowPassengers(!showPassengers);
    }

    const doCancelTrip = async (id) =>{
        try{
          const response = await fetch(query, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json', 
              'Authorization': JSON.parse(localStorage.getItem('token'))
            },
            body: JSON.stringify({OwnerId: localStorage.getItem('id')})
          });
    
          if(!response.ok)
          {
            let data = await response.json();
            throw new Error(data)
          }
          Action(id)
        }
        catch(err)
        {
          console.log(err);
        }
      }

  return (
    <>
    <div className='Ticket' onClick={showAppliedPassengers}>
        <div className='ItemSpace'>
          <h4>From:</h4>
          <p> {data.origin}</p>
        </div>
        <div className='ItemSpace'>
          <h4>To: </h4> 
          <p>{data.destination}</p>
        </div>
        <div className='ItemSpace'>
          <h4>Departure time:</h4>
          <p> {data.departureTime}</p>
        </div>
        <div className='ItemSpace'>
          <h4>Expected Arrival:</h4>
          <p> {data.expectedArrivalTime}</p>
        </div>
        <div className='ItemSpace'>
          <h4>Seats available:</h4>
          <p className='y'> {data.availableSeats}</p>
        </div>
        <button onClick={() => doCancelTrip(data.id)}>cancel trip</button>
    </div>
    </>
  )
}

export default CarPoolTicket