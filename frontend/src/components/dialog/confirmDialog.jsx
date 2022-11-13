import React from "react";
import {
	Dialog,
	DialogTitle,
	DialogActions,
	DialogContent,
	Typography,
	Button,
	IconButton,
} from "@mui/material";
import ButtonGroup from "@mui/material/ButtonGroup";
import  WarningRoundedIcon from "@mui/icons-material/WarningRounded";

function ConfirmDialog(props) {
	const { confirmState, setConfirmState } = props;

	const dialogTypes = Object.freeze({
		LEAVE: "Leave",
	});

	const icon =
		confirmState.type.toLocaleLowerCase() === dialogTypes.LEAVE.toLocaleLowerCase() ? (
			<WarningRoundedIcon color="error" fontSize="large" />
		) : (
			""
		);

	const keyword =
		confirmState.type.toLocaleLowerCase() ===
		dialogTypes.LEAVE.toLocaleLowerCase()
			? dialogTypes.LEAVE
			: "";

	return (
		<Dialog open={confirmState.isOpen} style={{ padding: "16px" }}>
			<DialogTitle style={{ display: "flex", justifyContent: "center" }}>
				<IconButton disableRipple>{icon}</IconButton>
			</DialogTitle>
			<DialogContent style={{ textAlign: "center" }}>
				<Typography variant="h6">
					{keyword} {confirmState.name}?
				</Typography>
				<Typography variant="p">
					Are you sure you want to {keyword.toLowerCase()}{" "}
					<strong>{confirmState.name}</strong> ?
				</Typography>
				{confirmState.type.toLocaleLowerCase() ===
				dialogTypes.LEAVE.toLocaleLowerCase() ? (
					<Typography variant="subtitle2">
						This action cannot be undone.
					</Typography>
				) : (
					""
				)}
			</DialogContent>
			<DialogActions
				style={{
					justifyContent: "center",
					marginBottom: "10px",
				}}
			>
				<ButtonGroup
					variant="spaced"
					aria-label="outlined primary button group"
					sx={{ marginTop: 0 }}
				>
					<Button
						sx={{ color: "white" }}
						variant="contained"
						onClick={confirmState.onConfirm}
					>
						Yes
					</Button>
					<Button
						sx={{
							backgroundColor: "#ededed",
							color: "grey",
							border: "#BEBEBE",
						}}
						onClick={() => {
							setConfirmState({ ...confirmState, isOpen: false });
						}}
					>
						No
					</Button>
				</ButtonGroup>
			</DialogActions>
		</Dialog>
	);
}

export default ConfirmDialog;
