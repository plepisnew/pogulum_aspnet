import { Box } from "@mui/system";
import React from "react";

type Props = {
  children: React.ReactNode;
};

export const Page: React.FC<Props> = ({ children }) => {
  return (
    <Box
      sx={{
        height: "100vh",
        paddingTop: "60px",
      }}
    >
      {children}
    </Box>
  );
};
