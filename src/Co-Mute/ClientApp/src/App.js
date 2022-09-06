import React, { useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import {LoginContext} from './components/Context/LoginContext';
import RequireAuth from './components/Pages/RequireAuth';
import Login from './components/Pages/Login';
import Register from './components/Pages/Register';
import MyCo_Mutes from './components/Pages/MyCo_Mutes';
import NotFound from './components/Pages/NotFound';
import FindCo_Mutes from './components/Pages/FindCo_Mutes';
import Profile from './components/Pages/Profile';
import CreateCarPoolTicket from './components/Pages/CreateCarPoolTicket';


function App() {
  const [loggedIn, setLoggedIn] = useState(false);

  return (
   
    <LoginContext.Provider value={{setLoggedIn, loggedIn}}>   
           
      <Routes>
        <Route path="/">
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />

            {/*protected routes*/}
            <Route element={<RequireAuth />}>    
              <Route exact path="/" element={<MyCo_Mutes />} />
              <Route path="/addTicket" element={<CreateCarPoolTicket />} />
              <Route path="findCo-mutes" element={<FindCo_Mutes />}/>
              <Route path="profile" element={<Profile />}/>
            </Route>

          {/* catch Route*/}
          <Route path='*' element={<NotFound />} />
        </Route>
      </Routes>
    </LoginContext.Provider>
  )
}

export default App
