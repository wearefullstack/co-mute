import axios from "axios";
//LOGIN USER
export const LoginUser = async (myForm) => {
	try {
		const res = await axios.post("/api/Login", myForm, {
			headers: { "Content-Type": "application/json" },
		});
		const data = res.data;
		return data;
	} catch (error) {
		console.error(error.response.data);
	}
};
