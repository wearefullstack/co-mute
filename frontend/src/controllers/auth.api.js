import axios from "axios";
//LOGIN USER
export const LoginUser = async (myForm) => {
	console.log("login user is called");
	console.log(myForm);
	try {
		const res = await axios.post("/api/Login", myForm, {
			headers: { "Content-Type": "application/json" },
		});
		const data = res.data;
		console.log(data);
		return data;
	} catch (error) {
		console.error(error.response.data);
	}
};
