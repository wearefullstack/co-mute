import React, { useState, useEffect } from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import PersonIcon from "@mui/icons-material/Person";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import * as yup from "yup";
import { useFormik } from "formik";
import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

import { CurrentUser, RegisterUser } from "../../../controllers/user.api";
import { useAuth } from "../../../auth/authorize";

import { MuiTelInput } from "mui-tel-input";

const theme = createTheme();

function useDebounce(value, delay) {
	const [debouncedValue, setDebouncedValue] = useState(value);

	useEffect(() => {
		const timeoutId = setTimeout(() => {
			setDebouncedValue(value);
		}, delay);
		return () => clearTimeout(timeoutId);
	}, [value]);
	return debouncedValue;
}

function Profile() {
	const navigate = useNavigate();
	const auth = useAuth();
	const queryClient = useQueryClient();
	const [id] = useState(auth.getId());
	const [phone, setPhone] = useState("");

	const debouncedValue = useDebounce(phone, 300);
	//Current User query
	const { data: currentUser = [] } = useQuery(
		["currentUser", { UserId: id }],
		CurrentUser,
		{
			onSuccess: (data) => {
				setValues({
					UserId: data.userId ?? id,
					Name: data.name ?? "",
					Surname: data.surname ?? "",
					Phone: data.phone ?? "",
					Email: data.email ?? "",
					Role: data.role ?? "User",
					CreatedOn: data.createdOn ?? new Date().toJSON(),
				});
				setPhone(data.phone);
			},
			refetchOnWindowFocus: false,
			refetchIntervalInBackground: false,
		}
	);

	//Mutate Register
	const { mutate: registerUser } = useMutation(RegisterUser, {
		onSuccess: (results) => {
			queryClient.invalidateQueries("currentUser");
			toast("Successfully Updated Profile", {
				type: "success",
			});
			navigate("/dashboard/profile");
		},
		onError: (results) => {
			toast("Updating Profile Failed", {
				type: "error",
			});
		},
	});

	const { values, errors, submitForm, handleChange, setValues } = useFormik({
		initialValues: {
			UserId: id,
			Name: "",
			Surname: "",
			Phone: "",
			Email: "",
			Password: "",
			Role: "User",
			CreatedOn: new Date().toJSON(),
		},
		validationSchema: yup.object().shape({
			Name: yup.string().required("Required"),
			Surname: yup.string().required("Required"),
			Email: yup.string().email("Invalid email format").required("Required"),
			Password: yup.string().required("Required"),
		}),
		onSubmit: (values) => {
			registerUser(values);
		},
	});

	const handlePhoneInput = (value) => {
		setPhone(value);
	};

	useEffect(() => {
		values.Phone = debouncedValue;
	}, [debouncedValue]);

	return (
		<ThemeProvider theme={theme}>
			<Container component="main" maxWidth="xs">
				<CssBaseline />
				<Box
					sx={{
						marginTop: 0,
						display: "flex",
						flexDirection: "column",
						alignItems: "center",
					}}
				>
					<Avatar sx={{ m: 1, background: "lightgreen" }}>
						<PersonIcon />
					</Avatar>
					<Typography component="h1" variant="h5">
						Profile
					</Typography>
					<Box sx={{ mt: 3 }}>
						<Grid container spacing={2}>
							<Grid item xs={12} sm={6}>
								<TextField
									autoComplete="given-name"
									name="Name"
									required
									fullWidth
									id="Name"
									size="small"
									label="First Name"
									autoFocus
									error={!!errors.Name}
									helperText={errors.Name}
									value={values.Name ?? ""}
									onChange={handleChange}
								/>
							</Grid>
							<Grid item xs={12} sm={6}>
								<TextField
									required
									fullWidth
									id="Surname"
									label="Last Name"
									name="Surname"
									autoComplete="family-name"
									size="small"
									error={!!errors.Surname}
									helperText={errors.Surname}
									value={values.Surname ?? ""}
									onChange={handleChange}
								/>
							</Grid>
							<Grid item xs={12}>
								<MuiTelInput
									aria-label="Phone"
									fullWidth
									size="small"
									defaultCountry="US"
									value={phone ?? ""}
									onChange={handlePhoneInput}
								/>
							</Grid>
							<Grid item xs={12}>
								<TextField
									required
									fullWidth
									id="Email"
									label="Email Address"
									name="Email"
									autoComplete="Email"
									size="small"
									error={!!errors.Email}
									helperText={errors.Email}
									value={values.Email ?? ""}
									onChange={handleChange}
								/>
							</Grid>
							<Grid item xs={12}>
								<TextField
									required
									fullWidth
									name="Password"
									label="Password"
									type="password"
									id="Password"
									autoComplete="new-password"
									size="small"
									error={!!errors.Password}
									helperText={errors.Password}
									value={values.Password ?? ""}
									onChange={handleChange}
								/>
							</Grid>
						</Grid>
						<Button
							onClick={submitForm}
							fullWidth
							variant="contained"
							color="success"
							sx={{ mt: 3, mb: 2 }}
						>
							Update Profile
						</Button>
					</Box>
				</Box>
			</Container>
		</ThemeProvider>
	);
}
export default Profile;
