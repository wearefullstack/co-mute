import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./styles/App.css";

import PrivateRoutes from "./utils/privateRoutes";

import Error from "./pages/error/error";
import Home from "./pages/home/index";
import Login from "./pages/login/login";
import Register from "./pages/register/register";
import Dashboard from "./pages/dashboard/dashboard";
import Carpool from "./pages/dashboard/carpool/carpool";
import Profile from "./pages/dashboard/profile/profile";

function App() {
	return (
		<Router>
			<Routes>
				<Route element={<PrivateRoutes />}>
					<Route path="/dashboard" element={<Dashboard />} />
					<Route path="/dashboard/carpool" element={<Carpool />} />
					<Route path="/dashboard/profile" element={<Profile />} />
				</Route>
				<Route path="*" element={<Error />} />
				<Route path="/" element={<Home />} />
				<Route path="/login" element={<Login />} />
				<Route path="/register" element={<Register />} />
			</Routes>
		</Router>
	);
}

export default App;