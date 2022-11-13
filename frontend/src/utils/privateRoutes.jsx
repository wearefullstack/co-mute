import { useState } from "react";
import { useLayoutEffect } from "react";
import { Navigate, Outlet } from "react-router-dom";
import Layout from "../components/layout/layout";
import { useAuth } from "../auth/authorize";

const PrivateRoutes = () => {
	const auth = useAuth();
	const [loggedIn, setLoggedIn] = useState(auth.isLoggedIn);
	useLayoutEffect(() => {
		setLoggedIn(loggedIn);
	}, [loggedIn]);

	return loggedIn ? (
		<Layout>
			<Outlet />
		</Layout>
	) : (
		<Navigate to="/login" />
	);
};

export default PrivateRoutes;
