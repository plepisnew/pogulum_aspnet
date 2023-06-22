import { Box, BoxProps } from "@mui/material";
import React from "react";

type Props = {
  width?: number | string;
  height?: number | string;
} & BoxProps;

export const Spacer: React.FC<Props> = ({ width, height, ...boxProps }) => {
  return (
    <Box {...boxProps} width={width ?? "100%"} height={height ?? "100%"}></Box>
  );
};
