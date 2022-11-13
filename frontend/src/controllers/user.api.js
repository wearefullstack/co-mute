import axios from "axios";

//Register User
export const RegisterUser = async (myForm) => {
	console.log("register user is called");
	console.log(myForm);
	// const UserId = myForm.UserId;
	// try {
	// 	const res = await axios.post(`/api/Users/${UserId}`, myForm, {
	// 		headers: { "Content-Type": "application/json" },
	// 	});
	// 	const data = res.data;
	// 	return data;
	// } catch (error) {
	// 	console.error(error.response.data);
	// }
};

//Current User
export const CurrentUser = async ({ queryKey }) => {
	console.log("current user is called");
	console.log(queryKey);
	// const { UserId } = queryKey[1];
	// const res = await axios.get(`/api/Users/${UserId}`);
	// const data = res.data;
	// return data;
};

//All Users
export const AllUsers = async () => {
	console.log("allUser is called");
	// const res = await axios.get("/api/Users");
	// const data = res.data;
	// return data;
};
