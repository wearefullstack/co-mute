import { useState } from "react";
import { useLayoutEffect } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../auth/authorize";

const PrivateRoutes = () => {
	const auth = useAuth();
	const [loggedIn, setLoggedIn] = useState(auth.isLoggedIn);
	useLayoutEffect(() => {
		setLoggedIn(loggedIn);
	}, [loggedIn]);

	return loggedIn ? <Outlet /> : <Navigate to="/login" />;
};

export default PrivateRoutes;
