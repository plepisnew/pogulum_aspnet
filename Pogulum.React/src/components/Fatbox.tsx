import { Box, BoxProps } from "@mui/material";

type Props = {
  children: React.ReactNode;
  centerX?: boolean;
  centerY?: boolean;
  center?: boolean;
};

export const Fatbox: React.FC<Props & BoxProps> = ({
  children,
  centerX,
  centerY,
  center,
  ...boxProps
}) => (
  <Box
    width="100%"
    height="100%"
    display="flex"
    flexDirection="column"
    justifyContent={center || centerX ? "center" : ""}
    alignItems={center || centerY ? "center" : ""}
  >
    {children}
  </Box>
);
