import React, { useContext } from 'react'
import { ThemeContext } from '../Context/ThemeContext';
import { LoginContext } from '../Context/LoginContext'
import { StyledNavLink, StyledNavButton } from '../Styled/Navbar.styled';


function Navbar({className}) {
    const logged = useContext(LoginContext);
    const theme = useContext(ThemeContext);

    const SetTheme= () => {
      theme.setLightTheme(!theme.lightTheme);
    };

    const SignOut = () =>{        
        logged.setLoggedIn(false);
    }

  return (
    <nav className={className}>
        <StyledNavLink path="/" text="My Co-Mutes"/>
        <StyledNavLink path="/findCo-mutes" text="Find Co-Mutes"/>
        <StyledNavLink path="/addTicket" text="Create Ticket"/>
        <StyledNavLink path="/profile" text="My Profile"/>
        <StyledNavButton Onclick={SetTheme} text="mode"/>
        <StyledNavButton Onclick={SignOut} text="Sign Out"/>
    </nav>
  )
}

export default Navbar