import React from "react";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import Box from "@mui/material/Box";
import { createTheme, ThemeProvider, styled } from "@mui/material/styles";

const Item = styled(Paper)(({ theme }) => ({
	...theme.typography.body2,
	textAlign: "center",
	color: theme.palette.text.secondary,
	height: "100%",
	lineHeight: "60px",
	width: "100%",
}));

const lightTheme = createTheme({ palette: { mode: "light" } });

function PageContainer({ children }) {
	return (
		<Grid container rowSpacing={2}>
			<ThemeProvider theme={lightTheme}>
				<Box
					sx={{
						p: 2,
						bgcolor: "background.default",
						display: "grid",
						gridTemplateColumns: { md: "1fr" },
						gap: 2,
						width: "100%",
					}}
				>
					<Item elevation={8}>{children}</Item>
				</Box>
			</ThemeProvider>
		</Grid>
	);
}

export default PageContainer;
