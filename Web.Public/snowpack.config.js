module.exports = {
    "extends": "@snowpack/app-scripts-svelte",
    mount: {
        public: '/',
        src: '/_dist_',
      },
    devOptions: {},
    installOptions: {
        namedExports: ["svelte-spa-router"]
    },
    plugins: [
        "@snowpack/plugin-postcss",
        "@snowpack/plugin-dotenv",
        "@snowpack/plugin-optimize",
        /*"@snowpack/plugin-webpack",*/
        [
            '@snowpack/plugin-run-script',
            {cmd: 'svelte-check --output human', watch: '$1 --watch', output: 'stream'},
        ],
    ]
}