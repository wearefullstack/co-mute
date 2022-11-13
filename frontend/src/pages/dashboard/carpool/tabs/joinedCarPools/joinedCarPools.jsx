import React, { useState } from "react";
import { DataGrid, GridToolbarQuickFilter } from "@mui/x-data-grid";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import PageContainer from "../../../../../components/custom/pageContainer";
import { Grid, IconButton, Tooltip } from "@mui/material";

import LogoutIcon from "@mui/icons-material/Logout";
import { toast } from "react-toastify";
import moment from "moment";
import { useAuth } from "../../../../../auth/authorize";

import ConfirmDialog from "../../../../../components/dialog/confirmDialog";

import {
	JoinedCarPool,
	LeaveCarPool,
} from "../../../../../controllers/joinCarPool.api";

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

function JoinedCarPools() {
	const auth = useAuth();
	const [id] = useState(auth.getId());
	const queryClient = useQueryClient();

	const [confirmState, setConfirmState] = useState({
		isOpen: false,
		name: "",
		type: "",
	});

	const { data: joinedCarPools = [], isLoading } = useQuery(
		["joinedCarPools", { UserId: id }],
		JoinedCarPool,
		{
			refetchOnWindowFocus: false,
			refetchIntervalInBackground: false,
		}
	);

	//Mutate Car Pool Leave
	const { mutate: leaveCarPool } = useMutation(LeaveCarPool, {
		onSuccess: (d) => {
			queryClient.invalidateQueries("joinedCarPools");
			setConfirmState({ ...confirmState, isOpen: false });
			toast("Exited Car Pool Opportunity", { type: "success" });
		},
		onError: (d) => {
			toast("Something went wrong!!", { type: "error" });
		},
	});

	const columns = [
		{
			field: "action",
			headerName: "",
			width: 100,
			valueGetter: ({ row }) => {
				return row.joinId;
			},
			renderCell: ({ row }) => {
				return (
					<div
						className="d-flex justify-content-between align-items-center"
						style={{ cursor: "pointer" }}
					>
						<Tooltip placement="top" title="Leave Car Pool Opportunity">
							<IconButton
								color="primary"
								fontSize="small"
								onClick={() => {
									console.log(row.joinId);
									setConfirmState({
										isOpen: true,
										name:
											row.carPools[0].origin +
											" to " +
											row.carPools[0].destination,
										type: "leave",
										onConfirm: () => {
											leaveCarPool({
												JoinId: row.joinId,
											});
										},
									});
								}}
							>
								<LogoutIcon color={"error"} />
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
						<strong>{row.carPools[0].origin}</strong>
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
						<strong>{row.carPools[0].destination}</strong>
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
						<div>
							{moment(row.carPools[0].expectedArrivalTime).format("lll")}
						</div>
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
						<div>{moment(row.carPools[0].departureTime).format("lll")}</div>
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
						{row.carPools[0].daysAvailable.length < 8 ? (
							<div>{row.carPools[0].daysAvailable.split(",").join(" ")}</div>
						) : (
							<Tooltip
								placement="top"
								title={row.carPools[0].daysAvailable.split(",").join(" ")}
							>
								<div>
									{row.carPools[0].daysAvailable
										.split(",")
										.join(" ")
										.slice(0, 10)}
									...
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
						<div>{row.carPools[0].availableSeats}</div>
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
						{row.carPools[0].notes.length < 15 ? (
							<div>{row.carPools[0].notes}</div>
						) : (
							<Tooltip placement="top" title={row.carPools[0].notes}>
								<div>{row.carPools[0].notes.slice(0, 15)}...</div>
							</Tooltip>
						)}
					</div>
				);
			},
		},
		{
			field: "joinedOn",
			headerName: "Joined On",
			flex: 1,
			editable: false,
			renderCell: ({ row }) => {
				return (
					<Tooltip placement="top" title="Joined On">
						<div>
							<strong>{moment(row.joinedOn).format("lll")}</strong>
						</div>
					</Tooltip>
				);
			},
		},
	];

	return (
		<PageContainer>
			<Grid item xs={12}>
				<DataGrid
					autoHeight
					rows={joinedCarPools}
					loading={isLoading}
					columns={columns}
					components={{
						Toolbar: CustomToolBar,
					}}
					getRowClassName={(params) =>
						params.indexRelativeToCurrentPage % 2 === 0 && "even"
					}
					getRowHeight={() => "auto"}
					getRowId={(row) => row.joinId}
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
			<ConfirmDialog
				confirmState={confirmState}
				setConfirmState={setConfirmState}
			/>
		</PageContainer>
	);
}

export default JoinedCarPools;
