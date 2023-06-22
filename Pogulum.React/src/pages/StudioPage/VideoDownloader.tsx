import React, { useState } from "react";
import { Alert, Box, CircularProgress, Menu } from "@mui/material";
import DownloadIcon from "@mui/icons-material/Download";
import PlayArrowIcon from "@mui/icons-material/PlayArrow";
import ExpandLessIcon from "@mui/icons-material/ExpandLess";
import { Button, Divider } from "./StyledComponents";
import { Api } from "../../Api";
import { Clip } from "../../globals";
import Typography from "@mui/material/Typography";
import { useSnackbar } from "notistack";

type Props = {
  concatClips: Clip[];
};

const extensions: { title: string; format: string }[] = [
  {
    title: "MPEG-4",
    format: "mp4",
  },
  {
    title: "Audio Video Interleave",
    format: "avi",
  },
  {
    title: "Windows Media Video",
    format: "wmv",
  },
  {
    title: "Flash Video",
    format: "flv",
  },
  {
    title: "Matroska Video",
    format: "mkv",
  },
  {
    title: "QuickTime File Format",
    format: "mov",
  },
];

export const VideoDownloader: React.FC<Props> = ({ concatClips }) => {
  const [objectUrl, setObjectUrl] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [extension, setExtension] = useState<string>(extensions[0].format);

  const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);
  const formatOpen = Boolean(anchorEl);

  const concatenateClip = async () => {
    try {
      snackbar.enqueueSnackbar({
        message: "The Server has been notified and is hard at work",
        autoHideDuration: 3000,
        variant: "info",
      });
      setObjectUrl(null);
      setLoading(true);
      const clipUrls = concatClips.map(
        (clip) => clip.thumbnailUrl.split("-preview-")[0] + ".mp4"
      );
      const res = await Api.post(
        `/videos/${extension}?use_clip_ids=false`,
        clipUrls,
        {
          responseType: "blob",
        }
      );
      setObjectUrl(URL.createObjectURL(res.data));
    } catch (err) {
      snackbar.enqueueSnackbar({
        message: "Failed to concatenate clip :( Check console for trace",
        autoHideDuration: 3000,
        variant: "error",
      });
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const downloadClip = () => {
    if (objectUrl != null) {
      const anchor = document.createElement("a");
      anchor.href = objectUrl;
      anchor.download = `output.${extension}`;
      anchor.click();
    }
  };

  const snackbar = useSnackbar();

  return (
    <>
      <Menu
        open={formatOpen}
        onClose={() => {
          setAnchorEl(null);
        }}
        anchorEl={anchorEl}
        anchorOrigin={{
          horizontal: "right",
          vertical: "top",
        }}
        transformOrigin={{
          vertical: "bottom",
          horizontal: "right",
        }}
        sx={{
          "& .MuiList-root": {
            flexDirection: "column",
            alignItems: "stretch",
            gap: 0,
            paddingY: 2,
          },
          "& .MuiPaper-root": {
            background: "rgb(20, 20, 20)",
            color: "white",
            width: "300px",
          },
        }}
      >
        {extensions.map(({ title, format }, index) => (
          <div key={title}>
            <Typography
              p={2}
              sx={{
                "&:hover": {
                  backgroundColor: "rgb(50, 50, 50)",
                  transition: "background-color 200ms",
                },
                cursor: "pointer",
              }}
              onClick={() => {
                setExtension(format);
                setObjectUrl(null);
                setAnchorEl(null);
              }}
            >
              {title} (.{format})
            </Typography>
            {index != extensions.length - 1 && (
              <Divider color="rgb(80, 80, 80)" />
            )}
          </div>
        ))}
      </Menu>
      <Button
        icon={<ExpandLessIcon />}
        sx={{
          "& .MuiSvgIcon-root": {
            transition: "transform 300ms",
            transform: formatOpen ? "rotate(180deg)" : "rotate(0deg)",
          },
        }}
        onClick={(e) => {
          setAnchorEl(e.currentTarget);
        }}
      >
        Video Format: {extension}
      </Button>
      <Button
        icon={<PlayArrowIcon />}
        onClick={concatenateClip}
        disabled={loading}
      >
        Concatenate
        {loading && (
          <CircularProgress
            sx={{
              position: "absolute",
            }}
            size="25px"
          />
        )}
      </Button>
      <Button
        icon={<DownloadIcon />}
        onClick={downloadClip}
        disabled={objectUrl == null}
      >
        Download
      </Button>
    </>
  );
};
