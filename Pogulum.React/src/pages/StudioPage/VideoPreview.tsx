import React from "react";
import { Box, Typography } from "@mui/material";
import VisibilityIcon from "@mui/icons-material/Visibility";
import { Clip } from "../../globals";

type Props = {
  previewClip: Clip | null;
  clipSelected: boolean;
};

export const VideoPreview: React.FC<Props> = ({
  previewClip,
  clipSelected,
}) => {
  return previewClip ? (
    <iframe
      src={`${previewClip.embedUrl}&parent=${location.hostname}`}
      height="100%"
      width="100%"
      allowFullScreen
      style={{ border: "none" }}
    ></iframe>
  ) : (
    <Box
      width="100%"
      height="100%"
      sx={{ background: "black", color: "white" }}
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
    >
      <Typography variant="h3">Video Preview</Typography>
      <Typography variant="h4">
        {clipSelected ? (
          <>
            Press the <VisibilityIcon /> icon to see preview.
          </>
        ) : (
          <>Begin by dragging a clip below.</>
        )}
      </Typography>
    </Box>
  );
};
