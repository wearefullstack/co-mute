import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

const Login = () => {
  // initialize the form data and error state
  const [data, setData] = useState({
    email: "",
    password: "",
  });
  const [error, setError] = useState("");

  // useNavigate hook from Reach Router to navigate to the dashboard page
  const navigate = useNavigate();

  // updates the form data when the form field values change
  const handleChange = (event) => {
    setData({ ...data, [event.target.name]: event.target.value });
  };

  // handleSubmit is called when the form is submitted
  const handleSubmit = (event) => {
    // prevent the default form submit behavior
    event.preventDefault();
    // retrieve the stored signup data from local storage
    const storedData = JSON.parse(localStorage.getItem("signupData"));
    // check if the stored data is available and is an array
    if (!storedData || !Array.isArray(storedData)) {
    // set an error message if the stored data is not available or is not an array
      setError("Incorrect details");
      return;
    }
    console.log(storedData)
    // check if the email and password entered match the stored signup data
    const isMatched = storedData.some(
      (user) => user.email === data.email && user.password === data.password
    );
    // if the email and password do not match, set an error message
    if (!isMatched) {
      setError("Incorrect details");
      return;
    }
    // if the email and password match, log a success message and navigate to the dashboard page
    console.log("Successfully logged in!");
    navigate("/dashboard/*");
  };

  return (
    <div className="flex items-center justify-center px-5 w-full max-w-6xl">
      <div className="w-full max-w-4xl mx-auto shadow-xl shadow-gray-400 flex flex-col rounded-3xl md:flex-row bg-white">
        <div className="w-full text-center py-12 rounded-l-3xl px-3 md:w-3/5">
          <form onSubmit={handleSubmit} className="space-y-7">
            <h1 className="font-bold text-3xl mb-12">Login to Your Account</h1>
            {error && <p className="text-red-500 font-bold">{error}</p>}
            <input
              type="email"
              placeholder="Email"
              name="email"
              onChange={handleChange}
              value={data.email}
              required
              className="block w-full max-w-sm mx-auto p-[15px] rounded-lg focus:outline-none bg-slate-200 placeholder:text-black"
            />
            <input
              type="password"
              placeholder="Password"
              name="password"
              onChange={handleChange}
              value={data.password}
              required
              className="block w-full max-w-sm mx-auto p-[15px] rounded-lg focus:outline-none bg-slate-200 placeholder:text-black"
            />
            <button
              type="submit"
              className="px-20 py-3 rounded-full bg-green-400 font-bold transition-all duration-200 ease-in-out hover:scale-110"
            >
              Submit
            </button>
          </form>
        </div>
        <div className="flex flex-col items-center justify-center w-full py-5 space-y-4 rounded-b-3xl bg-green-400 px-2 md:w-2/5 md:rounded-r-3xl md:rounded-bl-none">
          <h1 className="font-bold text-3xl">New Here?</h1>
          <Link to="/signup">
            <button
              type="button"
              className="py-3 px-16 bg-white rounded-2xl font-bold transition-all duration-200 ease-in-out hover:scale-110"
            >
              Sign Up
            </button>
          </Link>
        </div>
      </div>
    </div>
  );
};
export default Login;
