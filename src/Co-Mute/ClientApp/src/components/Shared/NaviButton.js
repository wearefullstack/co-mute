import React from 'react'

function NaviButton({Onclick, className, text}) {
  return (
    <button onClick={Onclick} className={className}>{text}</button>
  )
}

export default NaviButton