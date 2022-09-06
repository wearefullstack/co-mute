import { useContext, useState } from "react";
import { useLocation, Navigate, Outlet } from "react-router-dom";
import { LoginContext } from "../Context/LoginContext";
import {ThemeProvider} from 'styled-components';
import { ThemeContext } from "../Context/ThemeContext";
import { darkMode, lightMode, GlobalStyles } from "../Styled/theme.style";
import { StyledNavbar } from "../Styled/Navbar.styled";


const RequireAuth = () =>
{
    const logged = useContext(LoginContext);
    const location = useLocation();
    const [lightTheme, setLightTheme] = useState(false);

    return(
        logged.loggedIn
        ?
        <>
            <ThemeProvider theme={lightTheme ? lightMode : darkMode}>
                <GlobalStyles />
                <ThemeContext.Provider value={{setLightTheme, lightTheme}}> 
                    <StyledNavbar />
                    <Outlet />
                </ThemeContext.Provider>
            </ThemeProvider>
        </>
        : <Navigate to="/login" state={{from: location}} replace />
    );
}

export default RequireAuth;