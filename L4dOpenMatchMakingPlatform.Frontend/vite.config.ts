import {defineConfig} from "vite";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig(
    {
        build: {
            outDir: "../L4dOpenMatchMakingPlatform.Backend/wwwroot",
        },
        plugins: [
            vue(),
        ],
        server: {
            port: 7001,
            proxy: {
                "/Hunter1v1": {
                    target: "ws://localhost:7000",
                    changeOrigin: true,
                }
            }
        }
    },
);
