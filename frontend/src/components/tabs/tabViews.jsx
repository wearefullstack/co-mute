import * as React from "react";
import PropTypes from "prop-types";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";

function TabPanel(props) {
	const { children, value, index, ...other } = props;

	const childrenWithProps = React.Children.map(children, (child) => {
		// Checking isValidElement is the safe way and avoids a
		// typescript error too.
		if (React.isValidElement(child)) {
			return React.cloneElement(child, other);
		}
		return child;
	});

	return (
		<div
			role="tabpanel"
			hidden={value !== index}
			id={`simple-tabpanel-${index}`}
			aria-labelledby={`simple-tab-${index}`}
			{...other}
		>
			{value === index && (
				<Box sx={{ p: 3 }}>
					<Typography>{childrenWithProps}</Typography>
				</Box>
			)}
		</div>
	);
}

TabPanel.propTypes = {
	children: PropTypes.node,
	index: PropTypes.number.isRequired,
	value: PropTypes.number.isRequired,
};

function a11yProps(index, props) {
	return {
		id: `simple-tab-${index}`,
		"aria-controls": `simple-tabpanel-${index}`,
		...props,
	};
}

export default function TabViews(props) {
	const [value, setValue] = React.useState(0);

	const handleChange = (event, newValue) => {
		setValue(newValue);
	};

	return (
		<Box sx={{ width: "100%" }}>
			<Box
				sx={{
					borderBottom: 1,
					borderColor: "divider",
					margin: "0px 24px 0px 24px",
				}}
			>
				<Tabs
					value={value}
					onChange={handleChange}
					aria-label="basic tabs example"
				>
					{props.tabs.map((tab, index) => (
						<Tab label={tab.label} {...a11yProps(index)} key={index} />
					))}
				</Tabs>
			</Box>

			{props.tabs.map((tab, index) => (
				<TabPanel value={value} index={index} key={index} {...props}>
					{tab.component}
				</TabPanel>
			))}
		</Box>
	);
}
