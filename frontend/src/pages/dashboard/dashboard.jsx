import React, { useState } from "react";
import { DataGrid } from "@mui/x-data-grid";

import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import { Grid, IconButton, Tooltip, Typography } from "@mui/material";

import CustomToolBar from "../../components/tableGrid/customToolBar";
import PageContainer from "../../components/custom/pageContainer";
import FormModal from "../../components/custom/formModal";

import { toast } from "react-toastify";
import GroupAddIcon from "@mui/icons-material/GroupAdd";

import CarPoolForm from "./forms/carPoolForm";
import { CarPools } from "../../controllers/carPool.api";
import { JoinCarPool } from "../../controllers/joinCarPool.api";

import { useAuth } from "../../auth/authorize";

import moment from "moment";
import { useNavigate } from "react-router-dom";

function Dashboard() {
	const auth = useAuth();
	const navigate = useNavigate();
	const [id] = useState(auth.getId());
	const [open, setOpen] = useState(false);
	const queryClient = useQueryClient();

	// carPools query
	const { data: carPools = [], isLoading } = useQuery(["carPools"], CarPools, {
		refetchIntervalInBackground: false,
	});

	//Mutate Car Pool Joining
	const { mutate: joinCarPool } = useMutation(JoinCarPool, {
		onSuccess: (results) => {
			queryClient.invalidateQueries("carPools");
			if (results.joinId !== 0) {
				toast("Joined Car Pool Opportunity", { type: "success" });
				navigate("/dashboard/carpool");
			} else {
				toast("Already Joined Car Pool Opportunity Or Closed", {
					type: "warning",
				});
			}
		},
		onError: (results) => {
			toast("Something went wrong!!", { type: "error" });
		},
	});

	const columns = [
		{
			field: "action",
			headerName: "",
			width: 100,
			valueGetter: ({ row }) => {
				return row.carPoolId;
			},
			renderCell: ({ row }) => {
				return (
					<div
						className="d-flex justify-content-between align-items-center"
						style={{ cursor: "pointer" }}
					>
						<Tooltip placement="top" title="Join Car Pool Opportunity">
							<IconButton
								color="primary"
								fontSize="small"
								onClick={() => {
									joinCarPool({ UserId: id, CarPoolId: row.carPoolId });
								}}
							>
								<GroupAddIcon color={"success"} />
							</IconButton>
						</Tooltip>
					</div>
				);
			},
		},

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
			field: "owner",
			headerName: "Owner",
			editable: false,
			flex: 1,
			renderCell: ({ row }) => {
				return (
					<div>
						{row.user.name} {row.user.surname}
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
			<FormModal
				setOpen={setOpen}
				open={open}
				label="Create Car Pool Opportunity"
			>
				<CarPoolForm setOpen={setOpen} />
			</FormModal>
			<Typography
				variant="h5"
				sx={{
					color: "darkslategray",
					fontWeight: 500,
					fontSize: "1.25rem",
					letterSpacing: "2px",
					textAlign: "left",
					padding: "10px",
					boxShadow: "1px 1px #999",
					height: "60px",
					marginBlock: "20px",
					background: "#c8f7c8",
					borderRadius: "2px",
					margin: "5px",
				}}
			>
				Car Pools
			</Typography>
			<Grid item xs={12}>
				<DataGrid
					autoWidth
					autoHeight
					rows={carPools}
					loading={isLoading}
					columns={columns}
					components={{
						Toolbar: CustomToolBar,
					}}
					componentsProps={{
						toolbar: {
							create: () => {
								setOpen(true);
							},
						},
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
									field: "action",
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

export default Dashboard;
