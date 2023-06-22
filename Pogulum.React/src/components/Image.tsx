import { Box, BoxProps } from "@mui/material";
import React from "react";

type Props = {
  src: string;
  alt: string;
  pointer?: boolean;
} & BoxProps;

export const Image: React.FC<Props> = ({ src, alt, pointer, ...boxProps }) => {
  return (
    <Box
      component="img"
      src={src}
      alt={alt}
      {...boxProps}
      sx={{
        cursor: pointer ? "pointer" : "",
        ...boxProps.sx,
      }}
    />
  );
};
