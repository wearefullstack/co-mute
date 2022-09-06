import { createGlobalStyle } from "styled-components"

export const lightMode = {
    backgroundColor: "#fff",
    fontColor: "black",
    headerColor: "rgba(127, 219, 157, 0.445)",
    navButtonColor: 'red'
}

export const darkMode = {
    backgroundColor: "#16161d",
    fontColor: "white",
    headerColor: "rgba(127, 219, 157, 0.445)",
    navButtonColor: 'black'
}

export const GlobalStyles = createGlobalStyle`
    body{
        margin: 0;
        padding: 0;
        background-color: ${props => props.theme.backgroundColor};
        color: ${props => props.theme.fontColor}
    }
`