/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor,html,cshtml}"],
  theme: {
    container: {
      center: true,
      padding: {
        DEFAULT: '1rem',
        sm: '2rem',
        lg: '4rem',
        xl: '5rem',
        '2xl': '6rem',
      }
    },
    extend: {
      colors: {
        primary: {
          DEFAULT: '#00204A',
          50:  '#e5eaf0',
          100: '#ccd4e0',
          200: '#99a9c2',
          300: '#667ea3',
          400: '#335384',
          500: '#00204A',
          600: '#001c42',
          700: '#001539',
          800: '#000f31',
          900: '#000929',
        },
      }
    },
  },
  plugins: [],
}

