import React, { createContext, useContext, useState, useEffect } from "react";

const AuthContext = createContext(null);

export const Authorize = ({ children }) => {
	const [isLoggedIn, setIsLoggedIn] = useState(false);

	const setToken = (token) => {
		sessionStorage.setItem("token", token);
	};

	const setId = (id) => {
		sessionStorage.setItem("id", id);
	};

	const getId = () => {
		return Number(sessionStorage.getItem("id"));
	};

	const getToken = () => {
		return sessionStorage.getItem("token");
	};

	const removeToken = () => {
		return sessionStorage.removeItem("token");
	};

	const removeId = () => {
		return sessionStorage.removeItem("id");
	};

	const login = (value) => {
		setIsLoggedIn(value);
	};

	const logout = () => {
		removeToken();
		removeId();
		setIsLoggedIn(false);
		window.location.href = "/login";
	};

	useEffect(() => {
		const token = getToken();
		const id = getId();
		if (token !== null) {
			setIsLoggedIn(true);
			setToken(token);
			setId(id);
		}
	}, [isLoggedIn]);

	return (
		<AuthContext.Provider
			value={{
				isLoggedIn,
				login,
				setToken,
				getToken,
				setId,
				getId,
				logout,
			}}
		>
			{children}
		</AuthContext.Provider>
	);
};

export const useAuth = () => {
	return useContext(AuthContext);
};
