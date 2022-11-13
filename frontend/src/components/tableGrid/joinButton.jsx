import React from "react";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import GroupAddIcon from "@mui/icons-material/GroupAdd";

function JoinButton({ join }) {
	return (
		<IconButton
			onClick={() => {
				join();
			}}
		>
			<Tooltip title={"Join"} placement="top">
				<GroupAddIcon sx={{ color: "#2196f3" }} />
			</Tooltip>
		</IconButton>
	);
}

export default JoinButton;
