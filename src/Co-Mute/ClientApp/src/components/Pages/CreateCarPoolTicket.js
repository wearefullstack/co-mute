import React, {useState} from 'react'
import { StyledPageHeader } from '../Styled/Page.styled';
import '../../styles/createTicket.scss'

function CreateCarPoolTicket() {
const [isLoading, setIsLoading] = useState(false);
const [ticketBody, setTicketBody] = useState({});

const token = localStorage.getItem('token');
const query = 'http://localhost:5196/api/CarPoolTickets/CreateCarPoolTicket/' + localStorage.getItem('id');

  const doCreateTicket = async() =>{
    setIsLoading(true);
    try{
      const response = await fetch(query, 
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json', 
            'Authorization': JSON.parse(token)
          },
          body: JSON.stringify(ticketBody)
        });
        
        
        if(!response.ok)
        {

          throw new Error(`${response.status}: ${response.message}`)
        }


        let data = await response.json();
    
      } 
      catch(err)
      {
         
      }
      finally{
        setIsLoading(false);
      }

  }

  const handleinputChange = e => {
    setTicketBody({...ticketBody, [e.target.name]: e.target.value});
    console.log(ticketBody);

  }

  return (
    <div>
      { isLoading?
        <h2>loading...</h2>:
        <div className='CreateComtainer'>
          <StyledPageHeader text="Create Car Pool Ticket"/>
          <div className="content">

            <label>Start Time: </label>
            <input className='textBox' onChange={handleinputChange} placeholder="e.g. yyyy-MM-ddThh:mm" type="text" name="departureTime"/>

            <label>Expected Arrival Time:</label>
            <input className='textBox' onChange={handleinputChange} placeholder="e.g. yyyy-MM-ddThh:mm" type="text" name="expectedArrivalTime"/>
            
            <label>From:</label>
            <input className='textBox' onChange={handleinputChange} placeholder="Starting Location" type="text" name="origin"/>
            
            <label>To:</label>
            <input className='textBox' onChange={handleinputChange} placeholder="Destination name" type="text" name="destination"/>
            
            <label>AvailableSeats:</label>
            <input className='textBox' onChange={handleinputChange} placeholder="amount of seats available" type="text" name="availableSeats"/>

            <label>Note:</label>
            <textarea onChange={handleinputChange}  placeholder="Other Users will see this" name="notes"/>
            <br/>
            <button onClick={doCreateTicket}>save</button>
          </div>
        </div>
      }
    </div>
  )
}

export default CreateCarPoolTicket