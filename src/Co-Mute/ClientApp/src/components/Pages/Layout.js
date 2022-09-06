import React from 'react'
import { Container } from '../Container'
import {Outlet} from 'react-router-dom'

function Layout() {
  return (
    <Container>
        <Outlet />
    </Container>
  )
}

export default Layout