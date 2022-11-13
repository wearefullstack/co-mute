import AddCircle from "@mui/icons-material/AddCircleOutline";
import React from "react";
import { GridToolbarQuickFilter } from "@mui/x-data-grid";
import Button from "@mui/material/Button";

function CustomToolBar(
	{ create, csvOptions, rows, printOptions, ColDef, label, ...other },
	props
) {
	return (
		<div
			style={{
				borderRadius: 2,
				width: "100%",
				height: "60px",
				display: "flex",
				justifyContent: "space-between",
				padding: 10,
			}}
		>
			<div sx={{ alignItem: "flex-start" }}>
				<GridToolbarQuickFilter sx={{ color: "white" }} />
			</div>
			<div>
				<Button
					color="success"
					variant="contained"
					onClick={create}
					sx={{
						border: "none",
						fontWeight: "bold",
						position: "relative",
						width: "auto",
					}}
					startIcon={<AddCircle />}
				>
					Add
				</Button>
			</div>
		</div>
	);
}
export default CustomToolBar;
