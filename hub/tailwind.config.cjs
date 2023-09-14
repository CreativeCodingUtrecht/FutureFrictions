// import typography from '@tailwindcss/typography';
import daisyui from 'daisyui';

/** @type {import('tailwindcss').Config} */
export default {
	content: ['./src/**/*.{html,js,svelte,ts}'],
	purge: ['./src/**/*.html', './src/**/*.svelte'],
	theme: {
		extend: {}
	},
	plugins: [
    daisyui, 
    // typography
  ],
	daisyui: {
		themes: ["light"],
		// themes: true,
		base: true,	
	}
};
