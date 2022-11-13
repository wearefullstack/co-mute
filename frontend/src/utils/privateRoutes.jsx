import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../auth/authorize";

const PrivateRoutes=()=> {
	const auth = useAuth();
	return auth.isLoggedIn ? <Outlet /> : <Navigate to="/login" />;
}

export default PrivateRoutes;
