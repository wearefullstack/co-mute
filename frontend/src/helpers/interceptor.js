import axios from "axios";

const getToken = () => {
	return sessionStorage.getItem("token");
};

export function Interceptor() {
	//Request interceptor
	axios.interceptors.request.use(
		(config) => {
			const token = getToken();
			if (token) {
				config.headers["Authorization"] = "Bearer " + token;
			}
			config.headers["Content-Type"] = "application/json";
			return config;
		},
		(error) => {
			Promise.reject(error);
		}
	);

	//Response interceptor
	axios.interceptors.response.use(
		(response) => {
			// block to handle success case
			return response;
		},
		function (error) {
			// block to handle error case
			const originalRequest = error.config;

			if (
				error.response.status === 401 &&
				originalRequest.url === "http://dummydomain.com/auth/token"
			) {
				// Added this condition to avoid infinite loop
				// Redirect to any unauthorised route to avoid infinite loop...
				return Promise.reject(error);
			}

			if (error.response.status === 401 && !originalRequest._retry) {
				// Code inside this block will refresh the auth token
				originalRequest._retry = true;
				const refreshToken = "xxxxxxx"; // Write the logic or call here the function which is having the login to refresh the token.
				return axios
					.post("/auth/token", {
						refresh_token: refreshToken,
					})
					.then((res) => {
						if (res.status === 201) {
							sessionStorage.setItem("token", res.data);
							axios.defaults.headers.common["Authorization"] =
								"Bearer " + sessionStorage.getItem("token");
							return axios(originalRequest);
						}
					});
			}
			return Promise.reject(error);
		}
	);
}
