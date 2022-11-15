import React from 'react';
import logo from './logo.svg';
import './App.css';
import Register from './components/Pages/Register';
import "antd/dist/antd.css";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link,
  useParams
} from "react-router-dom";
import Home from './components/Home';
import Splash from './components/Pages/Splash';
import Login from './components/Pages/Login';



function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path='/login' element={ <Login/> }></Route>
          <Route path='/register' element={ <Register/> }></Route>
          <Route path='/' element={ <Splash/> }></Route>
          <Route path='/:tab' element={ <Home/> }></Route>
        </Routes>
      
    </div>
    </Router>
  );
}

export default App;
