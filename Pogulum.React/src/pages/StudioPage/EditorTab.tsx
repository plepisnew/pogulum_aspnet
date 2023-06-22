import React, { useState } from "react";
import { Box, Stack, Typography } from "@mui/material";
import { EditClipCard } from "./ClipCard";
import { Clip, UIClip } from "../../globals";
import { VideoPreview } from "./VideoPreview";
import { VideoDownloader } from "./VideoDownloader";
import { Divider } from "./StyledComponents";

type Props = {
  draggedClip: UIClip | null;
  setDraggedClip: React.Dispatch<React.SetStateAction<UIClip | null>>;
  removeCartItem: (item: UIClip) => void;
};

export const EditorTab: React.FC<Props> = ({
  draggedClip,
  setDraggedClip,
  removeCartItem,
}) => {
  const [previewClip, setPreviewClip] = useState<Clip | null>(null);
  const [concatClips, setConcatClips] = useState<Clip[]>([]);

  return (
    <Box
      p={1}
      display="grid"
      width="100%"
      height="100%"
      gridTemplateColumns="3fr 1fr"
      gridTemplateRows="3fr 1fr"
    >
      <Box m={2} overflow="hidden" borderRadius="15px">
        <VideoPreview
          previewClip={previewClip}
          clipSelected={concatClips.length != 0}
        />
      </Box>
      <Box
        m={2}
        p={3}
        borderRadius="15px"
        sx={{ background: "rgb(20, 20, 20)", color: "white" }}
      >
        {previewClip && (
          <Stack direction="column" spacing={1} mb={3}>
            <Typography fontWeight={600} variant="h6">
              Previewed Clip:
            </Typography>
            <Divider />
            <Typography>
              <Typography component="span" fontWeight={500}>
                &#x2022; Duration:{" "}
              </Typography>
              {previewClip.duration} seconds{" "}
            </Typography>
            <Typography>
              <Typography component="span" fontWeight={500}>
                &#x2022; Views:{" "}
              </Typography>
              {previewClip.viewCount}
            </Typography>
            <Typography>
              <Typography component="span" fontWeight={500}>
                &#x2022; Game:{" "}
              </Typography>
              {previewClip.game.name}
            </Typography>
            <Typography>
              <Typography component="span" fontWeight={500}>
                &#x2022; Streamer:{" "}
              </Typography>
              {previewClip.broadcaster.name}
            </Typography>
          </Stack>
        )}
        {concatClips.length != 0 && (
          <Stack direction="column" spacing={1}>
            <Typography fontWeight={600} variant="h6">
              All Clips ({concatClips.length}):
            </Typography>
            <Divider />
            <Typography>
              &#x2022; Total watch time of{" "}
              {concatClips.reduce(
                (prev, curr) => prev + Math.floor(curr.duration),
                0
              )}{" "}
              seconds
            </Typography>
            <Typography>
              &#x2022; Total view count of{" "}
              {concatClips.reduce((prev, curr) => prev + curr.viewCount, 0)}
            </Typography>
          </Stack>
        )}
      </Box>
      <Box
        m={2}
        p={2}
        border="1px solid white"
        borderRadius="10px"
        sx={{
          background: "rgba(255, 255, 255, 0.1)",
          overflowX: "scroll",
        }}
        onDragOver={(e) => {
          e.stopPropagation();
          e.preventDefault();
        }}
        onDrop={() => {
          if (draggedClip) {
            removeCartItem(draggedClip);
            setConcatClips([...concatClips, draggedClip.clip]);
            setDraggedClip(null);
          }
        }}
      >
        {concatClips.length == 0 ? (
          <></>
        ) : (
          <Stack direction="row" height="100%" spacing={2} width="max-content">
            {concatClips.map((clip, index) => (
              <EditClipCard
                index={index}
                totalClips={concatClips.length}
                clip={clip}
                key={clip.id + index}
                onDelete={() =>
                  setConcatClips(concatClips.filter((_, i) => index != i))
                }
                onPreview={() => setPreviewClip(clip)}
                onMoveLeft={() =>
                  setConcatClips([
                    ...concatClips.slice(0, index - 1),
                    concatClips[index],
                    concatClips[index - 1],
                    ...concatClips.slice(index + 1),
                  ])
                }
                onMoveRight={() => {
                  setConcatClips([
                    ...concatClips.slice(0, index),
                    concatClips[index + 1],
                    concatClips[index],
                    ...concatClips.slice(index + 2),
                  ]);
                }}
              />
            ))}
          </Stack>
        )}
      </Box>
      <Stack m={2} spacing={2}>
        <VideoDownloader concatClips={concatClips} />
      </Stack>
    </Box>
  );
};
