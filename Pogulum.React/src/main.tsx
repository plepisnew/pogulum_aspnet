import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Navbar } from "./components/Navbar";
import { Page } from "./pages/Page";
import { StudioPage } from "./pages/StudioPage";
import "./globals/index.css";
import { ThemeProvider } from "@mui/material/styles";
import { theme } from "./theme";
import { SnackbarProvider } from "notistack";
import { CookieProvider } from "./utils/Cookies";
import { RedirectPage } from "./pages/RedirectPage";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <CookieProvider>
      <ThemeProvider theme={theme}>
        <SnackbarProvider>
          <Navbar />
          <Page>
            <Router>
              <Routes>
                <Route path="/studio" element={<StudioPage />} />
                <Route path="*" element={<RedirectPage />} />
              </Routes>
            </Router>
          </Page>
        </SnackbarProvider>
      </ThemeProvider>
    </CookieProvider>
  </React.StrictMode>
);
