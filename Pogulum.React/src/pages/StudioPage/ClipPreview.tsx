import { Box, Stack, Typography } from "@mui/material";
import React, { useState } from "react";
import { IconButton, TextField } from "./StyledComponents";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";
import { Clip, Game, TwitchClip, UIClip } from "../../globals";
import { ScrapeClipCard } from "./ClipCard";
import CircularProgress from "@mui/material/CircularProgress";
import { twitchClipToClip } from "../../utils/UrlParser";

type Props = {
  clipsLoading: boolean;
  page: number;
  clips: TwitchClip[];
  onScrollLeft: () => void;
  onScrollRight: () => void;
  addCartItem: (clip: UIClip) => void;
};

export const ClipPreview: React.FC<Props> = ({
  clipsLoading,
  page,
  clips,
  onScrollLeft,
  onScrollRight,
  addCartItem,
}) => {
  const [loading, setLoading] = useState<boolean>(false);

  const leftDisabled = page <= 1 || loading || clipsLoading;
  const rightDisabled = loading || clipsLoading;

  const scrollLeft = async () => {
    if (leftDisabled) return;
    onScrollLeft();
  };

  const scrollRight = async () => {
    if (rightDisabled) return;
    setLoading(true);
    await onScrollRight();
    setLoading(false);
  };

  const [title, setTitle] = useState<string>("");

  return (
    <>
      <Stack direction="row" spacing={2}>
        <TextField
          placeholder="Filter by title..."
          value={title}
          onChange={(e) => setTitle(e.currentTarget.value)}
          sx={{ flex: 1 }}
        />
        <Stack direction="row">
          <IconButton onClick={scrollLeft} disabled={leftDisabled} left>
            <ChevronLeftIcon />
          </IconButton>
          <Typography
            px={2}
            width="80px"
            sx={{
              backgroundColor: "rgba(0, 0, 0, 0.4)",
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              fontSize: "0.875rem",
            }}
          >
            Page {page}
          </Typography>
          <IconButton onClick={scrollRight} disabled={rightDisabled} right>
            <ChevronRightIcon />
          </IconButton>
        </Stack>
      </Stack>
      <Box
        p={2}
        border="1px solid white"
        height="100%"
        borderRadius="10px"
        overflow="scroll"
        sx={{
          background: "rgba(255, 255, 255, 0.1)",
        }}
        data-scroll="false"
      >
        {loading || clipsLoading ? (
          <Box
            width="100%"
            height="100%"
            display="flex"
            alignItems="center"
            justifyContent="center"
          >
            <CircularProgress size="10%" sx={{ margin: "auto" }} />
          </Box>
        ) : clips.length == 0 ? (
          <Stack height="100%" justifyContent="center" textAlign="center">
            <Typography variant="h3">Found clips go here</Typography>
            <Typography variant="h4">
              Begin by selecting a game and/or streamer
            </Typography>
          </Stack>
        ) : (
          <Stack direction="column" spacing={2}>
            {clips
              .filter((clip) => clip.title.includes(title))
              .map((clip, index) => (
                <ScrapeClipCard
                  clip={clip}
                  key={clip.id}
                  select={async () => {
                    addCartItem({
                      clip: await twitchClipToClip(clip),
                      dupeIndex: index,
                      open: false,
                    });
                  }}
                />
              ))}
          </Stack>
        )}
      </Box>
    </>
  );
};
