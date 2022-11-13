import AddCircle from "@mui/icons-material/AddCircleOutline";
import React from "react";
import { GridToolbarQuickFilter } from "@mui/x-data-grid";
import { Button } from "@mui/material/Button";

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
				padding: 5,
			}}
		>
			<div>
				<Button
					variant="contained"
					onClick={create}
					sx={{ border: "none", fontWeight: "bold" }}
					startIcon={<AddCircle />}
				>
					Add
				</Button>
			</div>
			<div>
				<GridToolbarQuickFilter sx={{ color: "white" }} />
			</div>
			<div></div>
		</div>
	);
}
export default CustomToolBar;
