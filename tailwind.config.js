import fs from "fs";

/** @type {import('tailwindcss').Config} */

let theme = {};
try {
  const themePath = "./theme.json";

  if (fs.existsSync(themePath)) {
    theme = JSON.parse(fs.readFileSync(themePath, "utf-8"));
  }
} catch (err) {
  console.error('failed to parse custom styles', err)
}

// UAE Design System configuration merged with existing setup
const defaultTheme = {
  screens: {
    'sm': '640px',
    'md': '768px',
    'lg': '1024px',
    'xl': '1280px',
    '2xl': '1536px'
  },
  container: {
    padding: {
      DEFAULT: '0.625rem',
      md: '0.875rem',
      lg: '1.375rem',
      xl: '1.25rem'
    },
    center: true
  },
  fontFamily: {
    'roboto': ['\'Roboto\'', 'ui-sans-serif', 'system-ui', '-apple-system', 'BlinkMacSystemFont', '\'Segoe UI\'', '\'Helvetica Neue\'', 'sans-serif'],
    'inter': ['\'Inter\'', '\'Helvetica Neue\'', 'ui-sans-serif', 'system-ui', 'sans-serif'],
    'notokufi': ['\'Noto Kufi Arabic\'', 'ui-sans-serif', '\'Helvetica Neue\'', 'sans-serif'],
    'alexandria': ['\'Alexandria\'', '\'Helvetica Neue\'', 'ui-sans-serif', 'system-ui', 'sans-serif']
  },
  fontSize: {
    'xs':       ['0.75rem', { lineHeight: '1rem' }],
    'sm':       ['0.875rem', { lineHeight: '1.25rem' }],
    'base':     ['1rem', { lineHeight: '1.5rem' }],
    'lg':       ['1.125rem', { lineHeight: '1.5rem' }],
    'xl':       ['1.25rem', { lineHeight: '1.75rem' }],
    '2xl':      ['1.5rem', { lineHeight: '2rem' }],
    '3xl':      ['1.875rem', { lineHeight: '2.25rem' }],
    'h6':       ['1.25rem', { lineHeight: '1.75rem' }],
    'h5':       ['1.625rem', { lineHeight: '2.125rem' }],
    'h4':       ['2rem', { lineHeight: '2.375rem' }],
    'h3':       ['2.5rem', { lineHeight: '1.2' }],
    'h2':       ['3rem', { lineHeight: '1.2' }],
    'h1':       ['3.875rem', { lineHeight: '1.1' }],
    'display':  ['4.75rem', { lineHeight: '1.1' }]
  },
  extend: {
    screens: {
      coarse: { raw: "(pointer: coarse)" },
      fine: { raw: "(pointer: fine)" },
      pwa: { raw: "(display-mode: standalone)" },
    },
    colors: {
      inherit: "inherit",
      current: "currentColor",
      transparent: "transparent",
      // UAE Design System colors
      aegold: {
        50: "#F9F7ED",
        100: "#F2ECCF",
        200: "#E6D7A2",
        300: "#D7BC6D",
        400: "#CBA344",
        450: "#CBA343",
        500: "#B68A35",
        600: "#92722A",
        700: "#7C5E24",
        800: "#6C4527",
        900: "#5D3B26",
        950: "#361E12"
      },
      aered: {
        50: "#FEF2F2",
        100: "#FDE4E3",
        200: "#FDCDCB",
        300: "#FAAAA7",
        400: "#F47A75",
        500: "#EA4F49",
        600: "#D83731",
        700: "#B52520",
        800: "#95231F",
        900: "#7C2320",
        950: "#430E0C"
      },
      aegreen: {
        50: "#F3FAF4",
        100: "#E4F4E7",
        200: "#CAE8CF",
        300: "#A0D5AB",
        400: "#6FB97F",
        500: "#4A9D5C",
        600: "#3F8E50",
        700: "#2F663C",
        800: "#2A5133",
        900: "#24432B",
        950: "#0F2415"
      },
      aeblack: {
        50: "#F7F7F7",
        100: "#E1E3E5",
        200: "#C3C6CB",
        300: "#9EA2A9",
        400: "#797E86",
        500: "#5F646D",
        600: "#4B4F58",
        700: "#3E4046",
        800: "#232528",
        900: "#1B1D21",
        950: "#0E0F12"
      },
      whitely: {
        50: "#FFFFFF",
        100: "#FCFCFC",
        200: "#F7F7F7",
        300: "#F2F2F2",
        400: "#EDEDED",
        500: "#E8E8E8"
      },
      camel: {
        50: "#FFFBEB",
        100: "#FDF4C8",
        200: "#FBE68C",
        300: "#FAD44F",
        400: "#F8C027",
        500: "#F29F0E",
        600: "#D67909",
        700: "#B2550B",
        800: "#904110",
        900: "#773610",
        950: "#441B04"
      },
      slate: {
        50: "#F8FAFC",
        100: "#F1F5F9",
        200: "#E2E8F0",
        300: "#CBD5E1",
        400: "#94A3B8",
        500: "#64748B",
        600: "#475569",
        700: "#334155",
        800: "#1E293B",
        900: "#0F172A",
        950: "#020617"
      },
      fuchsia: {
        50: "#FDF4FF",
        100: "#FAE8FF",
        200: "#F5D0FE",
        300: "#F0ABFC",
        400: "#E879F9",
        500: "#D946EF",
        600: "#C026D3",
        700: "#A21CAF",
        800: "#86198F",
        900: "#701A75",
        950: "#4A044E"
      },
      techblue: {
        50: "#E7F5FF",
        100: "#D3EDFF",
        200: "#B0DBFF",
        300: "#81C1FF",
        400: "#4F98FF",
        500: "#286CFF",
        600: "#043DFF",
        700: "#003CFF",
        800: "#002DC2",
        900: "#0B32A4",
        950: "#071C5F"
      },
      seablue: {
        50: "#EFFAFF",
        100: "#DEF3FF",
        200: "#B6EAFF",
        300: "#75DBFF",
        400: "#2CCAFF",
        500: "#00ABEB",
        600: "#0090D4",
        700: "#0073AB",
        800: "#00608D",
        900: "#065074",
        950: "#04334D"
      },
      desert: {
        50: "#FEF5EE",
        100: "#FCE9D8",
        200: "#F9CFAF",
        300: "#F5AC7C",
        400: "#EF8048",
        500: "#EB5F24",
        600: "#E54B1D",
        700: "#B73417",
        800: "#922B1A",
        900: "#762518",
        950: "#3F100B"
      },
      primary: {
        50: '#F9F7ED',
        100: '#F2ECCF',
        200: '#E6D7A2',
        300: '#D7BC6D',
        400: '#CBA344',
        500: '#B68A35',
        550: '#B68A36',
        600: '#92722A',
        700: '#7C5E24',
        800: '#6C4527',
        900: '#5D3B26',
        950: '#361E12'
      },
      secondary: {
        50: '#F7F7F7',
        100: '#E1E3E5',
        200: '#C3C6CB',
        300: '#9EA2A9',
        400: '#797E86',
        500: '#5F646D',
        600: '#4B4F58',
        700: '#3E4046',
        800: '#232528',
        900: '#1B1D21',
        950: '#0E0F12'
      },
      'primary-support': {
        50: "#FFFBEB",
        100: "#FDF4C8",
        200: "#FBE68C",
        300: "#FAD44F",
        400: "#F8C027"
      },
      'secondary-support': {
        50: "#EFFAFF",
        100: "#DEF3FF",
        200: "#B6EAFF",
        300: "#75DBFF",
        400: "#2CCAFF"
      },
      'aered-support': {
        50: "#FEF2F2",
        100: "#FDE4E3",
        200: "#FDCDCB",
        300: "#FAAAA7",
        400: "#F47A75",
        500: "#EA4F49"
      },
      // Keep existing color system for compatibility
      neutral: {
        1: "var(--color-neutral-1)",
        2: "var(--color-neutral-2)",
        3: "var(--color-neutral-3)",
        4: "var(--color-neutral-4)",
        5: "var(--color-neutral-5)",
        6: "var(--color-neutral-6)",
        7: "var(--color-neutral-7)",
        8: "var(--color-neutral-8)",
        9: "var(--color-neutral-9)",
        10: "var(--color-neutral-10)",
        11: "var(--color-neutral-11)",
        12: "var(--color-neutral-12)",
        a1: "var(--color-neutral-a1)",
        a2: "var(--color-neutral-a2)",
        a3: "var(--color-neutral-a3)",
        a4: "var(--color-neutral-a4)",
        a5: "var(--color-neutral-a5)",
        a6: "var(--color-neutral-a6)",
        a7: "var(--color-neutral-a7)",
        a8: "var(--color-neutral-a8)",
        a9: "var(--color-neutral-a9)",
        a10: "var(--color-neutral-a10)",
        a11: "var(--color-neutral-a11)",
        a12: "var(--color-neutral-a12)",
        contrast: "var(--color-neutral-contrast)",
      },
      accent: {
        1: "var(--color-accent-1)",
        2: "var(--color-accent-2)",
        3: "var(--color-accent-3)",
        4: "var(--color-accent-4)",
        5: "var(--color-accent-5)",
        6: "var(--color-accent-6)",
        7: "var(--color-accent-7)",
        8: "var(--color-accent-8)",
        9: "var(--color-accent-9)",
        10: "var(--color-accent-10)",
        11: "var(--color-accent-11)",
        12: "var(--color-accent-12)",
        contrast: "var(--color-accent-contrast)",
      },
      "accent-secondary": {
        1: "var(--color-accent-secondary-1)",
        2: "var(--color-accent-secondary-2)",
        3: "var(--color-accent-secondary-3)",
        4: "var(--color-accent-secondary-4)",
        5: "var(--color-accent-secondary-5)",
        6: "var(--color-accent-secondary-6)",
        7: "var(--color-accent-secondary-7)",
        8: "var(--color-accent-secondary-8)",
        9: "var(--color-accent-secondary-9)",
        10: "var(--color-accent-secondary-10)",
        11: "var(--color-accent-secondary-11)",
        12: "var(--color-accent-secondary-12)",
        contrast: "var(--color-accent-secondary-contrast)",
      },
      fg: {
        DEFAULT: "var(--color-fg)",
        secondary: "var(--color-fg-secondary)",
      },
      bg: {
        DEFAULT: "var(--color-bg)",
        inset: "var(--color-bg-inset)",
        overlay: "var(--color-bg-overlay)",
      },
      "focus-ring": "var(--color-focus-ring)",
    },
    spacing: {
      '4.5': '1.125rem',
      '13': '3.25rem',
      // Keep existing spacing system
      px: "var(--size-px)",
      0: "var(--size-0)",
      0.5: "var(--size-0-5)",
      1: "var(--size-1)",
      1.5: "var(--size-1-5)",
      2: "var(--size-2)",
      2.5: "var(--size-2-5)",
      3: "var(--size-3)",
      3.5: "var(--size-3-5)",
      4: "var(--size-4)",
      5: "var(--size-5)",
      6: "var(--size-6)",
      7: "var(--size-7)",
      8: "var(--size-8)",
      9: "var(--size-9)",
      10: "var(--size-10)",
      11: "var(--size-11)",
      12: "var(--size-12)",
      14: "var(--size-14)",
      16: "var(--size-16)",
      20: "var(--size-20)",
      24: "var(--size-24)",
      28: "var(--size-28)",
      32: "var(--size-32)",
      36: "var(--size-36)",
      40: "var(--size-40)",
      44: "var(--size-44)",
      48: "var(--size-48)",
      52: "var(--size-52)",
      56: "var(--size-56)",
      60: "var(--size-60)",
      64: "var(--size-64)",
      72: "var(--size-72)",
      80: "var(--size-80)",
      96: "var(--size-96)",
    },
    borderRadius: {
      sm: "var(--radius-sm)",
      md: "var(--radius-md)",
      lg: "var(--radius-lg)",
      xl: "var(--radius-xl)",
      "2xl": "var(--radius-2xl)",
      full: "var(--radius-full)",
    },
    boxShadow: {
      'button': '0px 0px 0px 6px',
    },
    height: {
      '4.5': '1.125rem',
      '13': '3.25rem'
    },
    width: {
      '4.5': '1.125rem',
      '13': '3.25rem'
    },
    keyframes: {
      hide: {
        from: { opacity: 1 },
        to: { opacity: 0 },
      },
      slideIn: {
        from: { transform: 'translateX(calc(100% + 1rem))' },
        to: { transform: 'translateX(0)' },
      },
      swipeOut: {
        from: { transform: 'translateX(var(--radix-toast-swipe-end-x))' },
        to: { transform: 'translateX(calc(100% + 1rem))' },
      },
    },
    animation: {
      hide: 'hide 100ms ease-in',
      slideIn: 'slideIn 150ms cubic-bezier(0.16, 1, 0.3, 1)',
      swipeOut: 'swipeOut 100ms ease-out',
    },
  },
  darkMode: ["selector", '[data-appearance="dark"]'],
}

export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: { ...defaultTheme, ...theme },
};