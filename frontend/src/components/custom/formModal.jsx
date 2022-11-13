import * as React from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import Grid from "@mui/material/Grid";

const style = {
	position: "absolute",
	maxHeight: "90%",
	top: "50%",
	left: "50%",
	transform: "translate(calc(-50% + 0.5px), calc(-50% + 0.5px))",
	maxWidth: "fit-content",
	minWidth: "500px",
	bgcolor: "background.paper",
	borderRadius: 2,
	boxShadow: 24,
	overflowY: "scroll",
	p: 4,
};
export default function FormModal({ children, open, setOpen, label }) {
	const handleClose = () => {
		setOpen(false);
	};
	return (
		<>
			<Modal
				open={open}
				onClose={handleClose}
				aria-labelledby="modal-modal-title"
				aria-describedby="modal-modal-description"
			>
				<Box sx={style}>
					<Grid container rowGap={2}>
						<Grid item xs={12}>
							<Typography id="modal-modal-title" variant="h6" component="h2">
								{label}
							</Typography>
						</Grid>
						{children}
					</Grid>
				</Box>
			</Modal>
		</>
	);
}
