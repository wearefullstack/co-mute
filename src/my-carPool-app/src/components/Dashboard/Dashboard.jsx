import React, { useState } from "react";
import { Link } from "react-router-dom";
import Register from './Register'
import Profile from './Profile'
import Opportunities from "./Opportunities";

function Dashboard() {
  const [show, setShow] = useState("");

  function handleClick(name) {
    setShow(name);
  }

  return (
    <div className="shadow-xl shadow-gray-400 w-full max-w-3xl text-black mx-5 h-full overflow-y-scroll bg-white rounded-xl
    flex flex-col md:flex-row">
      <div className="h-full mb-5 border-b-2">
        <ul className="py-7 h-full text-center w-full min-w-[250px] list-none border-r-2">
          <li className="py-2 border-b-2">
            <a className="hover:cursor-pointer hover:bg-green-500 rounded-full py-2 px-6 hover:text-white active:text-lg" onClick={() => handleClick("Profile")}>Profile</a>
          </li>
          <li className="py-2 border-b-2">
            <a className="hover:cursor-pointer hover:bg-green-500 rounded-full py-2 px-6 hover:text-white active:text-lg" onClick={() => handleClick("Register")}>Register Oppertunity</a>
          </li>
          <li className="py-2 mb-8">
            <a className="hover:cursor-pointer  hover:bg-green-500 rounded-full py-2 px-6 hover:text-white active:text-lg" onClick={() => handleClick("Opportunities")}>Opportunities</a>
          </li>
          <li className="pt-3">
            <Link className="bg-green-400 py-2 px-4 rounded-full hover:bg-green-600 hover:text-white active:text-lg" to="/signup">Logout</Link>
          </li>
        </ul>
      </div>

      <div className="w-full flex justify-center items-center mb-3 px-3">
          {show === "Profile" && <Profile />}
          {show === "Register" && <Register />}
          {show === "Oppertunities" && <Opportunities />}
      </div>
    </div>
  );
}

export default Dashboard;
