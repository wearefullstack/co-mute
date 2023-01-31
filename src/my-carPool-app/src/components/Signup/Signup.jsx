import React, { useState, useNavigate } from "react";
import { Link } from "react-router-dom";

function Signup() {
  // Set useState to store input data
  const [data, setData] = useState({
    name: "",
    surname: "",
    phone: "",
    email: "",
    password: "",
  });

  // Handle user input and updates the specific key-value pair in the "data" object.
  const handleChange = (e) => {
    setData({ ...data, [e.target.name]: e.target.value });
  };

  const handleSubmit = () => {
    // retrieve the existing signup data from local storage
    let existingData = JSON.parse(localStorage.getItem("signupData"));
    // check if existing data is an array, if not set it to an empty array
    if (!Array.isArray(existingData)) {
      existingData = [];
    }

    // check if an email already exists in the signup data
    if (data.email && existingData.find((d) => d.email === data.email)) {
      // display an alert if the email already exists
      alert("Email already exists");
      return;
    }
    // add the new signup data to the existing signup data
    existingData.push(data);
    // save the updated signup data in local storage
    localStorage.setItem("signupData", JSON.stringify(existingData));
    // reset the form data
    setData({ name: "", surname: "", phone: "", email: "", password: "" });
  };

  return (
    <div className="flex items-center justify-center px-5 w-full max-w-6xl">
      <div className="w-full max-w-4xl mx-auto shadow-xl shadow-gray-400 flex flex-col-reverse rounded-3xl md:flex-row bg-white">
        <div className="flex flex-col items-center justify-center w-full py-5 space-y-8 rounded-b-3xl bg-green-400 px-2 md:w-2/5 md:rounded-l-3xl md:rounded-br-none">
          <h1 className="font-bold text-3xl">Welcome Back</h1>
          <Link to="/login">
            <button
              type="button"
              className="py-3 px-16 bg-white rounded-2xl font-bold "
            >
              Sign In
            </button>
          </Link>
        </div>
        <div className="w-full text-center py-12 rounded-l-3xl px-3 md:w-3/5 md:rounded-t-3xl">
          <form className="space-y-5">
            <h1 className="font-bold text-3xl mb-12">Create Account</h1>
            <input
              type="text"
              placeholder="Name"
              name="name"
              value={data.name}
              onChange={handleChange}
              required
              className="block w-full max-w-sm mx-auto p-[15px] rounded-lg focus:outline-none bg-slate-200 placeholder:text-black"
            />
            <input
              type="text"
              placeholder="Surname"
              name="surname"
              value={data.surname}
              onChange={handleChange}
              required
              className="block w-full max-w-sm mx-auto p-[15px] rounded-lg focus:outline-none bg-slate-200 placeholder:text-black"
            />
            <input
              type="text"
              placeholder="Phone"
              name="phone"
              value={data.phone}
              onChange={handleChange}
              className="block w-full max-w-sm mx-auto p-[15px] rounded-lg focus:outline-none bg-slate-200 placeholder:text-black"
            />
            <input
              type="email"
              placeholder="Email"
              name="email"
              value={data.email}
              onChange={handleChange}
              required
              className="block w-full max-w-sm mx-auto p-[15px] rounded-lg focus:outline-none bg-slate-200 placeholder:text-black"
            />
            <input
              type="password"
              placeholder="Password"
              name="password"
              value={data.password}
              onChange={handleChange}
              required
              className="block w-full max-w-sm mx-auto p-[15px]  rounded-lg focus:outline-none bg-slate-200 placeholder:text-black"
            />
            <button
              type="submit"
              className="px-20 py-3 rounded-full bg-green-400 font-bold "
              onClick={handleSubmit}
            >
              Submit
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default Signup;
