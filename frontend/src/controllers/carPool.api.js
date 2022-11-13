import axios from "axios";

//CAR POOLS
export const CarPools = async () => {
	const res = await axios.get("/api/CarPool/all");
	const data = res.data;
	return data;
};

//GET SINGLE CAR POOL BY USER
export const MyCarPools = async ({ queryKey }) => {
	const { UserId } = queryKey[1];
	const res = await axios.get(`/api/CarPool/User/${UserId}`);
	const data = res.data;
	return data;
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
