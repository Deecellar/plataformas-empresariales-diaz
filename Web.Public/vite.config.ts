import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import { fileURLToPath, URL } from "url";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [svelte()],
  resolve: {
    alias: {
      "App": fileURLToPath(new URL("./src/App", import.meta.url)),
      "Views": fileURLToPath(new URL("./src/lib", import.meta.url)),
      "AppViews": fileURLToPath(new URL("./src/App/Common/Views", import.meta.url)),
    },
  }
})
