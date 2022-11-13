import axios from "axios";

//JOIN CAR POOL TABLE
export const JoinCarPools = async () => {
	console.log("Joined car pools called");
	// const res = await axios.get("/api/Join/table");
	// const data = res.data;
	// return data;
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
export const JoinCarPool = async ({ queryKey }) => {
	console.log("join car pool is called");
	console.log(queryKey);
	// const { UserId, CarPoolId } = queryKey[1];
	// console.log(UserId + " " + CarPoolId);
	// const res = await axios.post(`/api/Join/User/${UserId}/CarPool/${CarPoolId}`);
	// const data = res.data;
	// return data;
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
