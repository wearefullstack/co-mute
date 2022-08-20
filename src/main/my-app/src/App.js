// App.js
import * as React from "react";
import { Routes, Route, Link } from "react-router-dom";
import CarList from "./CarList";
import MyForm from "./CreateCoMuterForm";
//
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import UserContextProvider from './UserContext'
// import LoginPage from "./LoginComponent";


function App() {
  return (
    <div className="App">
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6">
              Co-Mute.webapp
          </Typography>   
        </Toolbar>
      </AppBar>
      
      
      <UserContextProvider>
        <Routes>
        {/* <Route path="login" element={<LoginPage />} /> */}
        <Route path="carpool" element={<CarList />} />
        <Route path="about" element={<About />} />
        <Route path="create" element={<MyForm />} />
        <Route path="home" element={<Home />} />
        <Route path="/" element={<Home />} />
      </Routes>
        
      </UserContextProvider>

    </div>
  );
}

function Home() {
  return (
    <>
      <main>
        <h2>Welcome to the homepage!</h2>
      </main>
      <nav>
        <Link to="/about">About</Link>
        <br></br>
        <Link to="/create">Create</Link>

      </nav>
    </>
  );
}

function About() {
  return (
    <>
      <main>
        <h2>Who are we?</h2>
        <p>
          That feels like an existential question, don't you
          think?
        </p>
      </main>
      <nav>
        <Link to="/">Home</Link>
      </nav>
    </>
  );
}

export default App;