import axios from "axios";

//CAR POOLS
export const CarPools = async () => {
	console.log("car pools is called");
	const res = await axios.get("/api/CarPool/all");
	const data = res.data;
	console.log(data);
	return data;
};

//GET SINGLE CAR POOL BY USER
export const CarPool = async ({ queryKey }) => {
	console.log("car pool is called");
	console.log(queryKey);
	// const { UserId } = queryKey[1];
	// const res = await axios.get(`/api/CarPool/User/${UserId}`);
	// const data = res.data;
	// return data;
};

//CREATE CAR POOL
export const CreateCarPool = async (myForm) => {
	try {
		const UserId = myForm.Owner;
		const res = await axios.post(`/api/CarPool/create/${UserId}`, myForm, {
			"Content-Type": "application/json",
		});
		const data = res.data;
		return data;
	} catch (error) {
		console.error(error.response.data);
	}
};
