import React, { useState, useEffect } from 'react';
import Passenger from './Passenger';
import '../../styles/myCommutes.scss'

function CarPoolTicketDetails({CarPoolTicketDetails}) {
    const [isEdit, setIsEdit] = useState(false);
    const [editDetails, setEditDetails] = useState({});
    const [isLoading, setIsLoading] = useState(false);
    const [status, setStatus] = useState(CarPoolTicketDetails.status)
    const [err, seterr] = useState('');
    
    const deletelink = "http://localhost:5196/api/CarPoolTickets/CancelCreatedCarPoolTicketID/" + CarPoolTicketDetails.cid;
    const updatelink = "http://localhost:5196/api/CarPoolTickets/UpdateCarPoolTicketDetails/" + CarPoolTicketDetails.cid;
    const token = localStorage.getItem('token');
    
    useEffect(() => {
      setEditDetails(CarPoolTicketDetails);
    }, [])
    

    const CancelTicket = async () => {
      try{
        setIsLoading(true);
        console.log(deletelink);
        const response = await fetch(deletelink, 
          {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json', 
              'Authorization': JSON.parse(token)
            },
            body: JSON.stringify({userId: localStorage.getItem('id')})
          });
          
          setStatus('Cancelled');
          if(!response.ok)
          {
            throw new Error(`${response.status}: ${response.message}`)
          }

        } 
        catch(err)
        {

          setIsLoading(false);
        }
      
      finally{
        setIsLoading(false);
      }
    }

    const UpdateDetails = async () => {
      try{
        setIsLoading(true);
        const token = localStorage.getItem('token');
        const response = await fetch(updatelink, 
          {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json', 
              'Authorization': JSON.parse(token)
            },
            body: JSON.stringify(editDetails)
          });
          
          
          if(!response.ok)
          {
            throw new Error(`${response.status}: ${response.message}`)
          }
        } 
        catch(error)
        {
            seterr(error);
        }
        finally{
          setIsLoading(false);
        }
    }

    const setEdit = () => {
      setIsEdit(!isEdit)
    }

    const handleinputChange = e => {
      
      setEditDetails({...editDetails, [e.target.name]: e.target.value});

    }

  return (
    <div>
        {isLoading && <h2>loading</h2>}
        {
          CarPoolTicketDetails &&
          
        <div className='TicketDetail'>
        <button onClick={CancelTicket}>Cancel this ticket</button>
        <button onClick={setEdit}>{isEdit? "cancel edit" : "Update Details"}</button>
        <label className='statuslabel'>{status}</label>
        <label className='label'>{CarPoolTicketDetails.creationDate}</label>
        <br/>
        <label>From</label>
        <input className='textBox' type="text" name="origin" onChange={handleinputChange} defaultValue={CarPoolTicketDetails.origin} readOnly={!isEdit}/>

        <label>To</label>
        <input className='textBox' type="text" name="destination" onChange={handleinputChange} defaultValue={CarPoolTicketDetails.destination} readOnly={!isEdit}/>

        <label>Start Time</label>
        <input className='textBox' type="text" name="departureTime" onChange={handleinputChange} defaultValue={CarPoolTicketDetails.departureTime} readOnly={!isEdit}/>

        <label>End time</label>
        <input className='textBox' type="text" name="expectedArrivalTime" onChange={handleinputChange} defaultValue={CarPoolTicketDetails.expectedArrivalTime} readOnly={!isEdit}/>

        <label>SeatsAvailable</label>
        <input className='textBox' type="text" name="availableSeats" onChange={handleinputChange} defaultValue={CarPoolTicketDetails.availableSeats} readOnly={!isEdit}/>

        <label>Note</label>
        <textarea className='textArea'  name="notes" onChange={handleinputChange} defaultValue={CarPoolTicketDetails.notes} readOnly={!isEdit}/>
        <div>
        {
          isEdit ?
          <button onClick={UpdateDetails}>save</button> :
          null
        }
          <h4>List of passengers</h4>
        {
             CarPoolTicketDetails.passengers.length > 0  ?
                 CarPoolTicketDetails.passengers.map((passengerItem) => <Passenger data={passengerItem}/>):
                 <p>no passengers yet</p>
             
        }
        
        </div>
        </div>
}
    </div>
  )
}

export default CarPoolTicketDetails