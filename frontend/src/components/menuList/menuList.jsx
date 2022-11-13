import * as React from "react";
import List from "@mui/material/List";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import Collapse from "@mui/material/Collapse";
import ExpandLess from "@mui/icons-material/ExpandLess";
import ExpandMore from "@mui/icons-material/ExpandMore";
import { DefaultMenu } from "../menu/defaultMenu";
import ListItem from "@mui/material/ListItem";
import Typography  from "@mui/material/Typography";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

//icon imports
import DashboardIcon from "@mui/icons-material/Dashboard";
import TimeToLeaveIcon from "@mui/icons-material/TimeToLeave";
import PersonIcon from "@mui/icons-material/Person";

const icons = {
	Dashboard: <DashboardIcon sx={{ color: "#444545" }} />,
	TimeToLeave: <TimeToLeaveIcon sx={{ color: "#444545" }} />,
	Person: <PersonIcon sx={{ color: "#444545" }} />,
};

export default function NestedList({ drawerOpen, setDrawerState }) {
	const navigate = useNavigate();
	const [open, setOpen] = React.useState(true);
	const [selected, setSelected] = React.useState(true);

	const handleOpen = (label) => {
		// If the drawer is not open, open it when expanding menus
		!drawerOpen && setDrawerState(true);

		setOpen(open === label ? "" : label);
	};
	const handleSelected = (label) => {
		setSelected(label);
	};
	useEffect(() => {
		!drawerOpen && setOpen("");
	}, [drawerOpen]);
	return (
		<>
			{/**Standard List */}
			<List>
				{DefaultMenu.map((parent, parentIndex) => {
					return (
						<ListItem
							key={parent.label}
							disablePadding
							sx={{ display: "block" }}
						>
							<ListItemButton
								sx={{
									minHeight: 48,
									justifyContent: drawerOpen ? "initial" : "center",
									px: 2.5,
								}}
								selected={selected === parent.label}
								onClick={() => {
									if (parent.children.length === 0) {
										setSelected(parent.label);
									}

									parent.children.length > 0
										? handleOpen(parent.label)
										: navigate(parent.route, { state: parent.label });
								}}
							>
								<ListItemIcon
									sx={{
										minWidth: 0,
										mr: drawerOpen ? 3 : "auto",
										justifyContent: "center",
									}}
								>
									{icons[parent.iconName]}
								</ListItemIcon>
								<ListItemText sx={{ opacity: drawerOpen ? 1 : 0 }}>
									{" "}
									<Typography>{parent.label}</Typography>
								</ListItemText>
								{parent.children.length > 0 ? (
									drawerOpen ? (
										open === parent.label ? (
											<ExpandLess />
										) : (
											<ExpandMore />
										)
									) : (
										""
									)
								) : (
									""
								)}
							</ListItemButton>
							<Collapse in={open === parent.label} timeout="auto" unmountOnExit>
								<List component="div" disablePadding>
									{parent.children.map((child, childIndex) => {
										return (
											<ListItemButton
												key={child.label}
												selected={selected === parent.label + child.label}
												sx={{ pl: 4 }}
												onClick={() => {
													setSelected(parent.label + child.label);
													navigate(child.route, { state: child.label });
												}}
											>
												<ListItemText primary={child.label} />
											</ListItemButton>
										);
									})}
								</List>
							</Collapse>
						</ListItem>
					);
				})}
			</List>
		</>
	);
}
