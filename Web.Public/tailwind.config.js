const plugin = require('tailwindcss/plugin')


module.exports = {
  purge: ['./src/**/*.svelte', './public/*.html'],
  theme: {
    extend: {
      maxHeight: theme => ({
        "screen/2": "50vh",
        "screen/3": "calc(100vh / 3)",
        "screen/4": "calc(100vh / 4)",
        "screen/5": "calc(100vh / 5)",
      }),
      maxWidth: theme => ({
        "screen/2": "50vw",
        "screen/3": "calc(100vw / 3)",
        "screen/4": "calc(100vw / 4)",
        "screen/5": "calc(100vw / 5)",
      }),
    },
  },
  variants: { display: ["responsive", "hover", "focus",'focus-within','not-focus-within'] },
  plugins: [
    plugin(function({ addVariant, e }) {
      addVariant('not-focus-within', ({ modifySelectors, separator }) => {
        modifySelectors(({ className }) => {
          return `.${e(`not-focus-within${separator}${className}`)}:not(:focus-within
            )`
        })
      })
    })
  ],
};