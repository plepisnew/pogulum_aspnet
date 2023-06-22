import {
  Color,
  createTheme,
  PaletteColor,
  responsiveFontSizes,
  Theme,
} from "@mui/material";

declare module "@mui/material/styles" {
  interface Palette {
    buttonWhite: PaletteColor;
  }

  interface PaletteOptions {
    buttonWhite: PaletteColor;
  }
}

const { breakpoints } = createTheme();

export const theme: Theme = responsiveFontSizes(
  createTheme({
    spacing: (factor: number) => `${0.25 * factor}rem`,
    typography: {
      allVariants: {
        fontFamily: "Montserrat",
      },
    },
    palette: {
      buttonWhite: {
        contrastText: "black",
        main: "white",
        light: "white",
        dark: "black",
      },
    },
  })
);
