import * as React from "react";
import Box from "@mui/material/Box";
import Skeleton from "@mui/material/Skeleton";
import Grid from "@mui/material/Grid";

export default function FormSkeleton() {
	return (
		<Box sx={{ width: "100%" }}>
			<Grid container rowGap={1}>
				<Grid item xs={12}>
					{" "}
					<Skeleton height={60} />
				</Grid>
				<Grid item xs={12}>
					{" "}
					<Skeleton height={60} />
				</Grid>
				<Grid item xs={12}>
					{" "}
					<Skeleton height={60} />
				</Grid>
				<Grid item xs={3}>
					<Skeleton height={60} />
				</Grid>{" "}
				<Grid item xs={1}></Grid>{" "}
				<Grid item xs={3}>
					<Skeleton height={60} />
				</Grid>
			</Grid>
		</Box>
	);
}
