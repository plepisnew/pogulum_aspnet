import React, { useState } from "react";
import {
  Box,
  IconButton,
  Link,
  Stack,
  SxProps,
  Typography,
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import ControlPointDuplicateIcon from "@mui/icons-material/ControlPointDuplicate";
import VisibilityIcon from "@mui/icons-material/Visibility";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";
import KeyboardArrowLeftIcon from "@mui/icons-material/KeyboardArrowLeft";
import { Button, Tooltip } from "./StyledComponents";
import { Image } from "../../components/Image";
import { Clip, TwitchClip, UIClip } from "../../globals";
import { secondsToTwitchTimeStamp } from "../../utils/Time";

import VideogameAssetIcon from "@mui/icons-material/VideogameAsset";
import VideoCameraBackIcon from "@mui/icons-material/VideoCameraBack";
import PersonIcon from "@mui/icons-material/Person";
import { useCookies } from "../../utils/Cookies";
import { getId, parseJwt } from "../../utils/JwtParser";
import { Api } from "../../Api";

type ClipCardProps = {
  item: UIClip;
  onRemove: () => void;
  onDuplicate: () => void;
  open: boolean;
  handleOpen: () => void;
  stuck?: boolean;
  setDraggedClip: React.Dispatch<React.SetStateAction<UIClip | null>>;
};

export const ClipCard: React.FC<ClipCardProps> = ({
  item,
  onRemove,
  onDuplicate,
  open,
  handleOpen,
  stuck,
  setDraggedClip,
}) => {
  const { clip } = item;
  const [dragging, setDragging] = useState(false);

  const { cookies } = useCookies();
  const user = parseJwt(cookies["sid"]);

  return (
    <Box
      width="100%"
      position="relative"
      borderRadius="10px"
      overflow="hidden"
      border="2px solid black"
      onDragStart={() => {
        setDraggedClip(item);
        setDragging(true);
      }}
      onDragEnd={() => {
        setDragging(false);
        setDraggedClip(null);
      }}
      sx={{ opacity: dragging ? 0.5 : 1 }}
    >
      <Image
        src={clip.thumbnailUrl}
        alt={clip.title}
        width="100%"
        display="block"
        onClick={handleOpen}
        pointer
      />
      {!stuck && (
        <Box
          maxHeight={open ? "150px" : 0}
          width="100%"
          color="white"
          overflow="scroll"
          data-scroll={false}
          sx={{
            transition: "max-height 350ms",
            background: "rgb(20, 20, 20)",
          }}
        >
          <Box
            p={2}
            sx={{
              "& .MuiTypography-root": {
                fontSize: "0.875rem !important",
              },
            }}
          >
            <Typography>
              <Link
                href={clip.url}
                target="_blank"
                color="inherit"
                fontWeight={600}
              >
                {clip.title}
              </Link>{" "}
              &#x2022; {clip.viewCount} views
            </Typography>
            <Typography mb={1}>
              <Link
                href={`https://twitch.tv/${clip.broadcaster.name}`}
                target="_blank"
                color="inherit"
                fontWeight={600}
              >
                {clip.broadcaster.name}
              </Link>{" "}
              playing{" "}
              <Link
                href={`https://twitch.tv/directory/game/${encodeURIComponent(
                  clip.game.name
                )}`}
                target="_blank"
                color="inherit"
                fontWeight={600}
              >
                {clip.game.name}
              </Link>
            </Typography>
            <Stack direction="row" display="flex" spacing={2}>
              <Button
                sx={{ flex: 1, p: 0 }}
                icon={<DeleteIcon />}
                onClick={onRemove}
              >
                Delete
              </Button>
              <Button
                sx={{ flex: 1, p: 0 }}
                icon={<ControlPointDuplicateIcon />}
                onClick={onDuplicate}
              >
                Dupe
              </Button>
            </Stack>
          </Box>
        </Box>
      )}
      <Stack
        position="absolute"
        direction="row"
        top={5}
        right={5}
        spacing={1}
        alignItems="center"
      >
        {[
          {
            Icon: (params: any) => <PersonIcon {...params} />,
            tooltip: `Add ${clip.broadcaster.name} to favorites`,
            action: async () =>
              await Api.post("/activites", {
                userId: getId(user),
                subjectId: clip.broadcaster.id,
                activityType: 1,
              }),
          },
          {
            Icon: (params: any) => <VideogameAssetIcon {...params} />,
            tooltip: `Add ${clip.game.name} to favorites`,
            action: async () =>
              await Api.post("/activites", {
                userId: getId(user),
                subjectId: clip.game.id,
                activityType: 0,
              }),
          },
          {
            Icon: (params: any) => <VideoCameraBackIcon {...params} />,
            tooltip: `Add Clip to favorites`,
            action: async () =>
              await Api.post("/activites", {
                userId: getId(user),
                subjectId: clip.id,
                activityType: 2,
              }),
          },
        ].map(({ Icon, tooltip, action }) => (
          <Tooltip title={tooltip} key={tooltip} arrow>
            <IconButton
              sx={{
                backgroundColor: "rgba(0, 0, 0, 0.7)",
                color: "white",
                fontSize: "0.875rem",
                "&:hover": {
                  backgroundColor: "rgba(0, 0, 0, 0.6)",
                },
              }}
              onClick={action}
            >
              <Icon sx={{ fontSize: "inherit" }} />
            </IconButton>
          </Tooltip>
        ))}
        <Typography
          sx={{
            color: "white",
            background: "rgba(0, 0, 0, 0.7)",
            borderRadius: "8px",
            px: 1,
          }}
        >
          {secondsToTwitchTimeStamp(clip.duration)}
        </Typography>
      </Stack>
    </Box>
  );
};

type EditClipCardProps = {
  clip: Clip;
  onPreview: () => void;
  onDelete: () => void;
  onMoveLeft: () => void;
  onMoveRight: () => void;
  index: number;
  totalClips: number;
};

const buttonStyles: SxProps = {
  backgroundColor: "rgba(0, 0, 0, 0.7)",
  color: "white",
  borderRadius: "8px",
  "&:hover": {
    backgroundColor: "rgba(0, 0, 0, 0.6)",
  },
};

export const EditClipCard: React.FC<EditClipCardProps> = ({
  clip,
  onPreview,
  onDelete,
  onMoveLeft,
  onMoveRight,
  index,
  totalClips,
}) => {
  return (
    <Box
      height="100%"
      position="relative"
      overflow="hidden"
      borderRadius="10px"
      border="2px solid black"
      width="fit-content"
    >
      <Image
        src={clip.thumbnailUrl}
        alt={clip.title}
        sx={{ objectFit: "cover" }}
        display="inline-block"
        width="200px"
        height="100%"
        pointer
      />
      {index != 0 && (
        <IconButton
          sx={{
            position: "absolute",
            top: 5,
            left: 5,
            ...buttonStyles,
          }}
          onClick={onMoveLeft}
        >
          <KeyboardArrowLeftIcon sx={{ fontSize: "14px" }} />
        </IconButton>
      )}
      {index != totalClips - 1 && (
        <IconButton
          sx={{
            position: "absolute",
            top: 5,
            right: 5,
            ...buttonStyles,
          }}
          onClick={onMoveRight}
        >
          <ChevronRightIcon sx={{ fontSize: "14px" }} />
        </IconButton>
      )}
      <Stack
        direction="row"
        display="flex"
        position="absolute"
        bottom={5}
        right={5}
        spacing={1}
        alignItems="flex-end"
      >
        <IconButton sx={buttonStyles} onClick={onDelete}>
          <DeleteIcon sx={{ fontSize: "14px" }} />
        </IconButton>
        <IconButton sx={buttonStyles} onClick={onPreview}>
          <VisibilityIcon sx={{ fontSize: "14px" }} />
        </IconButton>
        <Typography
          sx={{
            color: "white",
            backgroundColor: "rgba(0, 0, 0, 0.7)",
            borderRadius: "8px",
            px: 1,
          }}
        >
          {secondsToTwitchTimeStamp(clip.duration)}
        </Typography>
      </Stack>
    </Box>
  );
};

export const ScrapeClipCard: React.FC<{
  clip: TwitchClip;
  select: () => void;
}> = ({ clip, select }) => {
  return (
    <Stack
      direction="row"
      height="90px"
      borderRadius="10px"
      overflow="hidden"
      boxShadow="0 0 4px rgba(0, 0, 0, 0.4)"
      sx={{
        backgroundColor: "rgb(40, 40, 40)",
      }}
    >
      <Link
        height="100%"
        href={clip.url}
        target="_blank"
        sx={{ cursor: "pointer", position: "relative" }}
      >
        <Image
          src={clip.thumbnail_url}
          alt={clip.title}
          borderRight="2px solid rgb(20, 20, 20)"
          height="100%"
        />
        <Typography
          sx={{
            color: "white",
            background: "rgba(0, 0, 0, 0.7)",
            position: "absolute",
            top: 5,
            right: 5,
            borderRadius: "8px",
            px: 1,
            fontSize: "0.75rem",
          }}
        >
          {secondsToTwitchTimeStamp(clip.duration)}
        </Typography>
      </Link>
      <Stack
        direction="row"
        p={2}
        justifyContent="space-between"
        width="100%"
        overflow="scroll"
        data-scroll={false}
      >
        <Stack direction="column" spacing={2}>
          <Typography>
            <Typography component="span" fontWeight={600}>
              "{clip.title}"
            </Typography>
          </Typography>
          <Typography fontSize="0.8125rem" color="rgb(200, 200, 200)">
            {clip.view_count} views
          </Typography>
        </Stack>

        <Stack direction="column" justifyContent="space-evenly">
          <IconButton onClick={select}>
            <ControlPointDuplicateIcon sx={{ color: "#64DD17" }} />
          </IconButton>
        </Stack>
      </Stack>
    </Stack>
  );
};
