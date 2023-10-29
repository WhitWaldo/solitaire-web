const colors = require('tailwindcss/colors');

module.exports = {
	content: [
		'./**/*.html',
		'./**/*.razor'
	],
	theme: {
		colors: {
			transparent: 'transparent',
			current: 'currentColor'
		},
		extend: {},
	},
	variants: {
		extend: {
			opacity: ['disabled']
		},
	},
	plugins: [
		require('@tailwindcss/forms'),
		require('@tailwindcss/aspect-ratio'),
		require('@tailwindcss/typography'),
		require('tailwindcss-patterns')
	],
}