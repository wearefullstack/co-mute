import styled from "styled-components";
import Navbar from "../Shared/Navbar";
import NaviButton from "../Shared/NaviButton";
import NaviLink from "../Shared/NaviLink";

export const StyledNavbar = styled(Navbar)`
    background-color: #16161d;
    height: 10%;
    display: flex;
    justify-content: flex-end;    

   
        
    
`;

export const StyledNavLink = styled(NaviLink)`
        color: rgba(127, 219, 157, 0.445);
        font-family: sans-serif;
        text-decoration: none;
      
        padding: 0.5em;
        &:hover{
           color:  rgba(127, 219, 157, 0.845);
        }
        &:active{
            color:  rgba(127, 219, 157, 0.845);
        }
`

export const StyledNavButton = styled(NaviButton)`
    color: ${props => props.theme.navButtonColor};
    border-radius: 25px;
    border-style: none;
    height:100%;
    padding: 0.5em;
    align-self: center;
    margin-left: .5em;
`