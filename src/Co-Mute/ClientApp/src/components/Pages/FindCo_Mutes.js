import React from 'react'
import { useState } from 'react';
import JoinCarPoolTicket from '../Shared/JoinCarPoolTicket';
import {useNavigate} from 'react-router-dom';
import {StyledPageHeader} from '../Styled/Page.styled';
import '../../styles/findCo-mutes.scss';

function FindCo_Mutes() {
  const query = "http://localhost:5196/api/CarPoolTickets/FindCarPoolTickets/" + localStorage.getItem('id');
  const token = localStorage.getItem('token');
  const [searchText, setSearchText] = useState({});
  const [carPoolTickets, setCarPoolTickets] = useState([]);
  const navigate = useNavigate();

  const handleSearchText = (e) =>{
    setSearchText({[e.target.name]: e.target.value});
  }

  const findTickets = async () =>{
    try{
      const response = await fetch(query, 
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json', 
            'Authorization': JSON.parse(token)
          },
          body: JSON.stringify(searchText)
        });
        
        
        if(!response.ok)
        {

          throw new Error(`${response.status}: ${response.message}`)
        }


        let data = await response.json();
        setCarPoolTickets(data);
        console.log(carPoolTickets);
      } 
      catch(err)
      {
          console.log(err);
      }
  }

  const removeSearchItem = (id) =>
  {
    const newArr = carPoolTickets.filter(o => {
      return o.id !== id 
    })
   setCarPoolTickets(newArr)
  }
  return (
    <>
      <StyledPageHeader text="FindCo_Mutes"/> 
        <div className="FindContainer">
          <div className="searchArea">
            <h3>Search a destination</h3>
              <div className="searchBox">
                <input className='search' type="text" name="searchText" onChange={handleSearchText} />
                <button onClick={findTickets}>find</button>
              </div>                
          </div>
        </div>
        <div className="FindContainer">
        {
          carPoolTickets.map(ticket => <JoinCarPoolTicket key={ticket.id} data={ticket} Action={() => removeSearchItem(ticket.id)}/>)
        }
        </div>
    </>
  )
}

export default FindCo_Mutes