import * as React from "react";
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
import { useAuth } from "../../auth/authorize";
import { useNavigate } from "react-router-dom";

import { LoginUser } from "../../controllers/auth.api";

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

const theme = createTheme();

export default function Login() {
	const auth = useAuth();
	const navigate = useNavigate();

	//Mutate Login User
	const { mutate: loginUser } = useMutation(LoginUser, {
		onSuccess: (results) => {
			auth.setToken(results.token);
			auth.setId(results.user.userId);
			auth.login(true);
			navigate("/dashboard");
		},
		onError: (results) => {
			toast("Login Failed", {
				type: "error",
			});
		},
	});

	const { values, errors, submitForm, handleChange } = useFormik({
		initialValues: {
			Email: "",
			Password: "",
		},
		validationSchema: yup.object().shape({
			Email: yup.string().email("Invalid email format").required("Required"),
			Password: yup.string().required("Required"),
		}),
		onSubmit: (values) => {
			loginUser(values);
		},
	});

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
						Sign in
					</Typography>
					<Box component="form" sx={{ mt: 1 }}>
						<TextField
							margin="normal"
							required
							fullWidth
							id="Email"
							label="Email Address"
							name="Email"
							autoComplete="Email"
							autoFocus
							size="small"
							error={!!errors.Email}
							helperText={errors.Email}
							value={values.Email}
							onChange={handleChange}
						/>
						<TextField
							margin="normal"
							required
							fullWidth
							name="Password"
							label="Password"
							type="password"
							id="Password"
							autoComplete="current-password"
							size="small"
							error={!!errors.Password}
							helperText={errors.Password}
							value={values.Password}
							onChange={handleChange}
						/>
						<Button
							onClick={submitForm}
							fullWidth
							variant="contained"
							sx={{ mt: 3, mb: 2 }}
						>
							Sign In
						</Button>
						<Grid container>
							<Grid item xs={12}>
								<Link component={link} to="/register" variant="body2">
									{"Don't have an account? Sign Up"}
								</Link>
							</Grid>
							<Grid item xs={12}>
								<Link component={link} to="/" variant="body2">
									{"Return Home"}
								</Link>
							</Grid>
						</Grid>
					</Box>
				</Box>
				<Copyright sx={{ mt: 8, mb: 4 }} />
			</Container>
		</ThemeProvider>
	);
}
