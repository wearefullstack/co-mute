import React from 'react'
import { NavLink } from 'react-router-dom'


function NaviLink({path, text, className}) {
  return (
    <NavLink exact to = {path} className={className}>{text}</NavLink>
  )
}

export default NaviLink