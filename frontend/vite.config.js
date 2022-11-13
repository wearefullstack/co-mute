import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
	server: {
		port: 3000,
		proxy: {
			"/api": {
				target: "http://localhost:5000",
				changeOrigin: true,
				secure: false,
				ws: true,
			},
		},
	},
	esbuild: {
		loader: "jsx",
		include: /src\/.*\.jsx?$/,
		exclude: [],
	},
	optimizeDeps: {
		esbuildOptions: {
			plugins: [
				{
					name: "load-js-files-as-jsx",
					setup(build) {
						build.onLoad({ filter: /src\/.*\.js$/ }, async (args) => ({
							loader: "jsx",
							contents: await fs.readFile(args.path, "utf8"),
						}));
					},
				},
			],
		},
	},
	plugins: [react()],
});
