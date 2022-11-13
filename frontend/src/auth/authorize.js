import React, { createContext, useContext, useState, useEffect } from "react";

const AuthContext = createContext(null);

export const Authorize = ({ children }) => {
	const [isLoggedIn, setIsLoggedIn] = useState(false);

	const setToken = (token) => {
		localStorage.setItem("token", token);
	};

	const setId = (id) => {
		localStorage.setItem("id", id);
	};

	const getId = () => {
		return Number(localStorage.getItem("id"));
	};

	const getToken = () => {
		return localStorage.getItem("token");
	};

	const removeToken = () => {
		return localStorage.removeItem("token");
	};

	const removeId = () => {
		return localStorage.removeItem("id");
	};

	const login = (value) => {
		removeToken();
		removeId();
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
