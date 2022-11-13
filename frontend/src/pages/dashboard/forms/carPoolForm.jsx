import React, { useState, useEffect } from "react";
import ButtonGroup from "@mui/material/ButtonGroup";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import { useFormik } from "formik";
import { toast } from "react-toastify";
import * as yup from "yup";

import dayjs from "dayjs";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";

import { useAuth } from "../../../auth/authorize";
import { useDebounce } from "../../../hooks/useDebounce";
import { CreateCarPool } from "../../../controllers/carPool.api";

import Box from "@mui/material/Box";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import Chip from "@mui/material/Chip";
import Typography from "@mui/material/Typography";

import { useNavigate } from "react-router-dom";

const days = [
	"Monday",
	"Tuesday",
	"Wednesday",
	"Thursday",
	"Friday",
	"Saturday",
	"Sunday",
];

function CarPoolForm({ setOpen }) {
	const navigate = useNavigate();
	const queryClient = useQueryClient();
	const auth = useAuth();
	const [id] = useState(auth.getId());
	const [deptTime, setDeptTime] = useState(
		dayjs(new Date().toJSON()).add(15, "minutes").toJSON()
	);
	const [expTime, setExpTime] = useState(dayjs(new Date().toJSON()));
	//Debounce Time
	const debouncedDeptTime = useDebounce(deptTime, 300);
	const debouncedExpTime = useDebounce(expTime, 300);

	const [msg, setMsg] = useState("");
	//Mutate Create Car Pool Opportunity
	const { mutate: createCarPool } = useMutation(CreateCarPool, {
		onSuccess: (results) => {
			queryClient.invalidateQueries("carPools");
			if (results.carPoolId !== 0) {
				toast("Added New Car Pool", { type: "success" });
				navigate("/dashboard/carpool");
			} else {
				toast("Car Pool Overlaps or Exists", { type: "warning" });
			}
			setOpen(false);
		},
		onError: (results) => {
			toast("Failed to Add Car Pool", {
				type: "error",
			});
		},
	});

	const { values, errors, submitForm, handleChange } = useFormik({
		initialValues: {
			Owner: id,
			ExpectedArrivalTime: new Date().toJSON(),
			DepartureTime: dayjs(new Date().toJSON()).add(10, "minutes").toJSON(),
			Origin: "",
			DaysAvailable: [],
			Destination: "",
			AvailableSeats: 0,
			Notes: "",
		},
		validationSchema: yup.object({
			DepartureTime: yup.string().required("Required"),
			ExpectedArrivalTime: yup.string().required("Required"),
			Origin: yup.string().required("Required"),
			Destination: yup.string().required("Required"),
			DaysAvailable: yup.array().required("Required"),
			AvailableSeats: yup.number().moreThan(0, "Cannot be 0, at least 1"),
		}),
		onSubmit: (values) => {
			if (values.DaysAvailable.length === 0) {
				setMsg("Required. Select 1 or more days");
			} else {
				createCarPool(values);
			}
		},
	});

	useEffect(() => {
		values.DepartureTime = dayjs(debouncedDeptTime).toJSON();
	}, [debouncedDeptTime]);

	useEffect(() => {
		values.ExpectedArrivalTime = dayjs(debouncedExpTime).toJSON();
	}, [debouncedExpTime]);

	useEffect(() => {
		setMsg("");
	}, [values.DaysAvailable]);

	return (
		<Grid container rowGap={2}>
			<Grid item xs={12}>
				<TextField
					autoFocus
					margin="dense"
					id="Origin"
					name="Origin"
					label="Origin"
					type="name"
					fullWidth
					size="small"
					error={!!errors.Origin}
					helperText={errors.Origin}
					value={values.Origin}
					onChange={handleChange}
				/>
			</Grid>
			<Grid item xs={12}>
				<TextField
					margin="dense"
					id="Destination"
					name="Destination"
					label="Destination"
					type="name"
					fullWidth
					size="small"
					error={!!errors.Destination}
					helperText={errors.Destination}
					value={values.Destination}
					onChange={handleChange}
				/>
			</Grid>
			<Grid item xs={12}>
				<LocalizationProvider dateAdapter={AdapterDayjs}>
					<DateTimePicker
						renderInput={(props) => (
							<TextField fullWidth size="small" {...props} />
						)}
						label="Expected Arrival Time"
						value={expTime}
						onChange={(newValue) => {
							setExpTime(newValue);
						}}
					/>
				</LocalizationProvider>
			</Grid>
			<Grid item xs={12}>
				<LocalizationProvider dateAdapter={AdapterDayjs}>
					<DateTimePicker
						renderInput={(props) => (
							<TextField fullWidth size="small" {...props} />
						)}
						label="Departure Time"
						minDateTime={expTime}
						value={deptTime}
						onChange={(newValue) => {
							setDeptTime(newValue);
						}}
					/>
				</LocalizationProvider>
			</Grid>

			<Grid item xs={12}>
				<FormControl fullWidth>
					<InputLabel size="small" id="days-label">
						Days Available : Select (1) or more days
					</InputLabel>
					<Select
						required
						size="small"
						id="DaysAvailable"
						name="DaysAvailable"
						labelId="days-label"
						label="Days Available : Select (1) or more days"
						multiple
						value={values.DaysAvailable}
						onChange={handleChange}
						renderValue={(selected) => (
							<Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
								{selected.map((value) => {
									return <Chip size="small" key={value} label={value} />;
								})}
							</Box>
						)}
					>
						{days.map((day) => (
							<MenuItem key={day} value={day}>
								{day}
							</MenuItem>
						))}
					</Select>
				</FormControl>
				{msg !== "" ? (
					<Typography variant="caption" sx={{ color: "red", marginTop: "5px" }}>
						{msg}
					</Typography>
				) : (
					""
				)}
			</Grid>

			<Grid item xs={12}>
				<TextField
					margin="dense"
					id="AvailableSeats"
					name="AvailableSeats"
					label="AvailableSeats"
					fullWidth
					size="small"
					error={!!errors.AvailableSeats}
					helperText={errors.AvailableSeats}
					value={values.AvailableSeats}
					onChange={handleChange}
				/>
			</Grid>

			<Grid item xs={12}>
				<TextField
					margin="dense"
					id="Notes"
					name="Notes"
					label="Notes"
					fullWidth
					size="small"
					value={values.Notes}
					onChange={handleChange}
				/>
			</Grid>
			<Grid item xs={12}>
				<ButtonGroup
					variant="spaced"
					aria-label="outlined primary button group"
				>
					<Button
						sx={{ color: "white" }}
						variant="contained"
						onClick={submitForm}
					>
						Save
					</Button>
					<Button
						sx={{
							backgroundColor: "#ededed",
							color: "grey",
							border: "#BEBEBE",
						}}
						onClick={() => {
							setOpen(false);
						}}
					>
						Cancel
					</Button>
				</ButtonGroup>
			</Grid>
		</Grid>
	);
}

export default CarPoolForm;
