import React from 'react'

function RegisteredCarPoolTickets({data}) {
  return (
    <div>
        <p>{data.ownerName}</p>
        <p>{data.phone}</p>
        <p>{data.email}</p>
        <p>{data.origin}</p>
        <p>{data.destination}</p>
        <p>{data.departureTime}</p>
        <p>{data.expectedArrivalTime}</p>
        <p>{data.seatsAvailable}</p>
    </div>
  )
}

export default RegisteredCarPoolTickets