import React from "react";
import MenuIcon from "@mui/icons-material/Menu";
import IconButton from "@mui/material/IconButton";
import Stack from "@mui/material/Stack";
import Button from "@mui/material/Button";
import { Link } from "react-router-dom";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import "../../styles/home.css";

const theme = createTheme();

function LandingPageButton() {
	return (
		<Stack spacing={2} direction="row">
			<Button
				component={Link}
				to="/login"
				variant="contained"
				size="medium"
				sx={{ backgroundColor: "sandybrown" }}
			>
				Start Here
			</Button>
		</Stack>
	);
}

function LandingFrameContent() {
	const style = {
		margin: "auto",
		padding: "10% 35% 10% 15%",
		color: "white",
		position: "relative",
	};
	return (
		<div style={style}>
			<h1 color="white" style={{ fontSize: "40px", marginBottom: "10px" }}>
				Car Pooling
			</h1>
			<div
				style={{
					fontSize: "20px",
					background: "white",
					borderRadius: "5px",
					padding: "10px",
					color: "black",
					opacity: 0.85,
				}}
			>
				With ever rising fuel prices and increased traffic on our roads, the
				Co-Mute project was started in an effort to help alleviate some of these
				pressures. Car-pooling can cut on traveling and vehicle maintenance
				costs. It also enables more effective use of commuters’ time, as time
				spent driving could be spent more productively.
			</div>
			<br />
			<LandingPageButton />
		</div>
	);
}

function LandingFrame() {
	const style = {
		backgroundImage: `url("images/background.svg")`,
		backgroundSize: "cover",
		backgroundRepeat: "no-repeat",
		backgroundAttachment: "fixed",
		backgroundPosition: "center",
		backgroundColor: "#ccc",
		position: "relative",
		height: "100%",
		width: "100%",
	};
	return (
		<div style={style}>
			<div className="topnav" id="myTopnav">
				<Link to="/" className="active">
					co-mute
				</Link>
				<div className="left-links">
					<Link to="/login">Login</Link>
					<Link to="/register">Register</Link>
					<IconButton className="icon" onClick={handleToggle}>
						<MenuIcon />
					</IconButton>
				</div>
			</div>
			<LandingFrameContent />
		</div>
	);
}

function handleToggle() {
	var x = document.getElementById("myTopnav");
	if (x.className === "topnav") {
		x.className += " responsive";
	} else {
		x.className = "topnav";
	}
}

function Index() {
	return (
		<ThemeProvider theme={theme}>
			<LandingFrame />
		</ThemeProvider>
	);
}

export default Index;
