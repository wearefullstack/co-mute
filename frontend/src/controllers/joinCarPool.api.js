import axios from "axios";

//JOIN CAR POOL TABLE
export const JoinCarPools = async () => {
	console.log("Joined car pools called");
	const res = await axios.get("/api/JoinCarPool/all");
	const data = res.data;
	return data;
};

//GET CURRENT USER FOR JOINED CAR POOLS
export const JoinedCarPool = async ({ queryKey }) => {
	console.log("joined car pool is called");
	console.log(queryKey);
	// const { UserId } = queryKey[1];
	// const res = await axios.get(`/api/Join/${UserId}`);
	// const data = res.data;
	// return data;
};

//JOIN CAR POOL
export const JoinCarPool = async (queryKey) => {
	console.log("join car pool is called");
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
export const LeaveCarPool = async ({ queryKey }) => {
	console.log("leave car pool is called");
	console.log(queryKey);
	// const { JoinId } = queryKey[1];
	// const res = await axios.delete(`/api/Join/${JoinId}`);
	// const data = res.data;
	// return data;
};
