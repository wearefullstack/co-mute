import React, { useState } from 'react'
import '../../styles/myCommutes.scss'

function Passenger({data}) {

  const [showActionDetails, setShowActionDetails] = useState(false)

  const showActionDetail = () => {
      setShowActionDetails(!showActionDetails);
  }

  return (
    <div className='Ticket'>
      <p>{data.name}</p>
      <p>{data.surname}</p>
      <p>{data.phone}</p>
      <p>{data.email}</p>
      <p>{data.note}</p>
      <p>{data.status}</p>

    </div>
  )
}

export default Passenger