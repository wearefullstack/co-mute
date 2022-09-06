import React, {useEffect, useState} from 'react';
import {StyledPageHeader} from '../Styled/Page.styled';
import CarPoolTicketDetails from '../Shared/CarPoolTicketDetails';
import CarPoolTicket from '../Shared/CarPoolTicket';
import '../../styles/myCommutes.scss'

function MyCo_Mutes() {

  const [myCommutes, setMyCommutes] = useState([]);
  const [registeredCommutes, setRegisteredCommutes] = useState([]);

  const createdCarCoolTicket = 'http://localhost:5196/api/CarPoolTickets/GetCarPoolTicketDetailsById/'+localStorage.getItem('id');
  const registeredlinks = 'http://localhost:5196/api/CarPoolTickets/GetRegisteredCarPoolTicketsByUserId/'+localStorage.getItem('id');
  
  useEffect(() => {
  }, [myCommutes, registeredCommutes])
  

  useEffect(() => {
    const token = localStorage.getItem('token');
    fetch(createdCarCoolTicket , {
      method: 'GET',
      headers : { 
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': JSON.parse(token)
       }
    }).then(res => {return res.json()}).then(data => setMyCommutes(data)); 

    fetch(registeredlinks , {
      method: 'GET',
      headers : { 
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': JSON.parse(token)
       }
    }).then(res => {return res.json()}).then(data => setRegisteredCommutes(data));
  }, [])
  
  const removeRegisterItem = (id) =>
  {
    const newArr = registeredCommutes.filter(o => {
      return o.id !== id 
    })
   setRegisteredCommutes(newArr)
  }
  return (
    <div className="CommuteContainer">
        <StyledPageHeader text="My Co-Mutes" />
          <section className="SectionCard">
            {
              registeredCommutes === [] ?
              <p>load</p>:
              registeredCommutes.map(regticket => {
        
              return <CarPoolTicket key={regticket.id} data={regticket} Action={() => removeRegisterItem(regticket.id)}/>})
            }
          </section>
          <br/>
        <StyledPageHeader text="Registered Co-Mutes" />
        <section className="SectionCard">
          {
            myCommutes === [] ?
            <p>load</p> :
              myCommutes.map(ticket => {
                
              return <CarPoolTicketDetails key={ticket.cid} CarPoolTicketDetails={ticket}/>})
             
              
          }
          
        </section>
    </div>
  )
}

export default MyCo_Mutes