import axios from "axios";

//Register User
export const RegisterUser = async (myForm) => {
	try {
		const UserId = myForm.UserId;
		const res = await axios.post(`/api/User/save/${UserId}`, myForm, {
			headers: { "Content-Type": "application/json" },
		});
		const data = res.data;
		return data;
	} catch (error) {
		console.error(error.response.data);
	}
};

//Current User
export const CurrentUser = async ({ queryKey }) => {
	const { UserId } = queryKey[1];
	const res = await axios.get(`/api/User/currentUser/${UserId}`);
	const data = res.data;
	return data;
};
