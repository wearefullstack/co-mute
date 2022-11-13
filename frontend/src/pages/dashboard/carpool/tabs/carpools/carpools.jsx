import React, { useState } from "react";
import { DataGrid, GridToolbarQuickFilter } from "@mui/x-data-grid";
import { useQuery } from "@tanstack/react-query";
import PageContainer from "../../../../../components/custom/pageContainer";
import Grid from "@mui/material/Grid";
import Tooltip from "@mui/material/Tooltip";
import { useAuth } from "../../../../../auth/authorize";
import { MyCarPools } from "../../../../../controllers/carPool.api";
import moment from "moment";

function CustomToolBar() {
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
				<GridToolbarQuickFilter sx={{ color: "white" }} />
			</div>
		</div>
	);
}

function CarPools() {
	const auth = useAuth();
	const [id] = useState(auth.getId());
	const { data: myCarPools = [], isLoading } = useQuery(
		["myCarPools", { UserId: id }],
		MyCarPools
	);

	const columns = [
		{
			field: "origin",
			headerName: "Origin",
			flex: 1,
			editable: false,
			renderCell: ({ row }) => {
				return (
					<div>
						<strong>{row.origin}</strong>
					</div>
				);
			},
		},
		{
			field: "destination",
			headerName: "Destination",
			flex: 1,
			editable: false,
			renderCell: ({ row }) => {
				return (
					<div>
						<strong>{row.destination}</strong>
					</div>
				);
			},
		},
		{
			field: "arrivalTime",
			headerName: "Est. Time",
			flex: 1,
			editable: false,
			renderCell: ({ row }) => {
				return (
					<Tooltip placement="top" title="Expected Arrival Time">
						<div>{moment(row.expectedArrivalTime).format("lll")}</div>
					</Tooltip>
				);
			},
		},
		{
			field: "departureTime",
			headerName: "Dep. Time",
			flex: 1,
			editable: false,
			renderCell: ({ row }) => {
				return (
					<Tooltip placement="top" title="Departure Time">
						<div>{moment(row.departureTime).format("lll")}</div>
					</Tooltip>
				);
			},
		},
		{
			field: "daysAvailable",
			headerName: "Days Available",
			flex: 1,
			minWidth: 150,
			maxWidth: 250,
			editable: false,
			renderCell: ({ row }) => {
				return (
					<div>
						{row.daysAvailable[0].length < 8 ? (
							<div>{row.daysAvailable[0].split(",").join(" ")}</div>
						) : (
							<Tooltip
								placement="top"
								title={row.daysAvailable[0].split(",").join(" ")}
							>
								<div>
									{row.daysAvailable[0].split(",").join(" ").slice(0, 10)}...
								</div>
							</Tooltip>
						)}
					</div>
				);
			},
		},
		{
			field: "availableSeats",
			headerName: "Avail. Seats",
			flex: 1,
			editable: false,
			renderCell: ({ row }) => {
				return (
					<Tooltip placement="top" title="Available Seats">
						<div>{row.availableSeats}</div>
					</Tooltip>
				);
			},
		},
		{
			field: "notes",
			headerName: "notes",
			flex: 1,
			minWidth: 150,
			maxWidth: 200,
			editable: false,
			renderCell: ({ row }) => {
				return (
					<div>
						{row.notes.length < 15 ? (
							<div>{row.notes}</div>
						) : (
							<Tooltip placement="top" title={row.notes}>
								<div>{row.notes.slice(0, 15)}...</div>
							</Tooltip>
						)}
					</div>
				);
			},
		},
	];

	return (
		<PageContainer>
			<Grid item xs={12}>
				<DataGrid
					autoHeight
					rows={myCarPools}
					loading={isLoading}
					columns={columns}
					components={{
						Toolbar: CustomToolBar,
					}}
					getRowClassName={(params) =>
						params.indexRelativeToCurrentPage % 2 === 0 && "even"
					}
					getRowHeight={() => "auto"}
					getRowId={(row) => row.carPoolId}
					onCellEditStop={(_, e) => console.log(e.target)}
					initialState={{
						sorting: {
							sortModel: [
								{
									field: "origin",
									sort: "desc",
								},
							],
						},
					}}
				/>
			</Grid>
		</PageContainer>
	);
}

export default CarPools;
