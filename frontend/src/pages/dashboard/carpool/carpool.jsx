import React from "react";
import TabViews from "../../../components/tabs/tabViews";

import JoinedCarPools from "../carpool/tabs/joinedCarPools/joinedCarPools";
import CarPools from "../carpool/tabs/carpools/carpools";

const tabsData = [
	{ label: "Joined Car Pools", component: <JoinedCarPools /> },
	{ label: "My Car Pools", component: <CarPools /> },
];

function CarPool() {
	return <TabViews tabs={tabsData} />;
}

export default CarPool;
