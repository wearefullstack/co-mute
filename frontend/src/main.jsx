import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import "./styles/index.css";

import { Authorize } from "./auth/authorize";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import AppTheme from "./providers/themeProvider";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { Interceptor } from "./helpers/interceptor";
Interceptor();

const theme = createTheme(AppTheme);

const queryClient = new QueryClient();
ReactDOM.createRoot(document.getElementById("root")).render(
	<Authorize>
		<ThemeProvider theme={theme}>
			<QueryClientProvider client={queryClient}>
				<ToastContainer />
				<App />
			</QueryClientProvider>
		</ThemeProvider>
	</Authorize>
);
