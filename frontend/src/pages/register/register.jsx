import React, { useState, useEffect } from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Link as link } from "react-router-dom";
import * as yup from "yup";
import { useFormik } from "formik";
import { useMutation } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

import { MuiTelInput } from "mui-tel-input";

import { RegisterUser } from "../../controllers/user.api";

import { useDebounce } from "../../hooks/useDebounce";

const theme = createTheme();

function Copyright(props) {
	return (
		<Typography
			variant="body2"
			color="text.secondary"
			align="center"
			{...props}
		>
			{"Copyright © "}
			<Link color="inherit" href="/">
				CoMute
			</Link>{" "}
			{new Date().getFullYear()}
			{"."}
		</Typography>
	);
}

function Register() {
	const navigate = useNavigate();
	const [phone, setPhone] = useState("");

	const debouncedValue = useDebounce(phone, 300);

	//Mutate Register
	const { mutate: registerUser } = useMutation(RegisterUser, {
		onSuccess: (results) => {
			if (results.userId !== 0) {
				toast("Successfully Registered", {
					type: "success",
				});
				navigate("/login");
			} else {
				toast("User already registered", {
					type: "warning",
				});
			}
		},
		onError: (results) => {
			toast("Registration Failed", {
				type: "error",
			});
		},
	});

	const { values, errors, submitForm, handleChange } = useFormik({
		initialValues: {
			UserId: 0,
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
						marginTop: 8,
						display: "flex",
						flexDirection: "column",
						alignItems: "center",
					}}
				>
					<Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
						<LockOutlinedIcon />
					</Avatar>
					<Typography component="h1" variant="h5">
						Sign up
					</Typography>
					<Box component="form" sx={{ mt: 3 }}>
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
									value={values.Name}
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
									value={values.Surname}
									onChange={handleChange}
								/>
							</Grid>
							<Grid item xs={12}>
								<MuiTelInput
									aria-label="Phone"
									fullWidth
									size="small"
									defaultCountry="US"
									value={phone}
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
									value={values.Email}
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
									value={values.Password}
									onChange={handleChange}
								/>
							</Grid>
						</Grid>
						<Button
							onClick={submitForm}
							fullWidth
							variant="contained"
							sx={{ mt: 3, mb: 2 }}
						>
							Sign Up
						</Button>
						<Grid container justifyContent="flex-end">
							<Grid item>
								<Link component={link} to="/login" variant="body2">
									Already have an account? Sign in
								</Link>
							</Grid>
						</Grid>
					</Box>
				</Box>
				<Copyright sx={{ mt: 5 }} />
			</Container>
		</ThemeProvider>
	);
}
export default Register;
