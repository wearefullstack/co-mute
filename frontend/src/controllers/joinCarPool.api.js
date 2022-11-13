import axios from "axios";

//JOINED CAR POOLS
export const JoinCarPools = async () => {
	console.log("Joined car pools called");
	const res = await axios.get("/api/JoinCarPool/all");
	const data = res.data;
	return data;
};

//GET JOINED CAR POOLS FOR CURRENT USER
export const JoinedCarPool = async ({ queryKey }) => {
	const { UserId } = queryKey[1];
	const res = await axios.get(`/api/JoinCarPool/user/${UserId}`);
	const data = res.data;
	return data;
};

//JOIN CAR POOL
export const JoinCarPool = async (queryKey) => {
	try {
		const UserId = queryKey.UserId;
		const CarPoolId = queryKey.CarPoolId;
		console.log(UserId + " " + CarPoolId);
		const res = await axios.post(
			`/api/JoinCarPool/carPool/${CarPoolId}/user/${UserId}`
		);
		const data = res.data;
		return data;
	} catch (error) {
		console.error(error.response.data);
	}
};

//LEAVE CAR POOL
export const LeaveCarPool = async (queryKey) => {
	const JoinId = queryKey.JoinId;
	const res = await axios.post(`/api/JoinCarPool/leave/${JoinId}`);
	const data = res.data;
	return data;
};
