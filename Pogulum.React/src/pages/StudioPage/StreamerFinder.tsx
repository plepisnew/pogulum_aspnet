import { CircularProgress, Grid, Stack } from "@mui/material";
import React, { useState } from "react";
import { TwitchApi } from "../../Api";
import { Broadcaster, TwitchBroadcaster } from "../../globals";
import { twitchBroadcasterToBroadcaster } from "../../utils/UrlParser";
import { Button, Autocomplete, Tooltip } from "./StyledComponents";
import { Image } from "../../components/Image";
import { useSnackbar } from "notistack";
import { topBroadcasters } from "./top";

type Props = {
  streamer: Broadcaster | null;
  setStreamer: React.Dispatch<React.SetStateAction<Broadcaster | null>>;
  options: Broadcaster[];
};

export const StreamerFinder: React.FC<Props> = ({
  streamer,
  setStreamer,
  options,
}) => {
  const [loading, setLoading] = useState<boolean>(false);
  const [input, setInput] = useState<string>("");

  const snackbar = useSnackbar();

  const fetch = async () => {
    try {
      setStreamer(null);
      setLoading(true);
      const res = await TwitchApi.get(`/users?login=${input}`);
      setStreamer(
        twitchBroadcasterToBroadcaster(res.data.data[0] as TwitchBroadcaster)
      );
    } catch (err) {
      console.error(err);
      snackbar.enqueueSnackbar({
        message:
          input.length == 0
            ? "Please enter a streamer name!"
            : `Unable to find a streamer named '${input}'`,
        autoHideDuration: 3000,
        variant: "error",
      });
    } finally {
      setLoading(false);
    }
  };

  const clear = () => {
    setInput("");
    setStreamer(null);
  };

  return (
    <Stack
      direction="column"
      spacing={2}
      height="40%"
      flex={1}
      data-scroll={false}
    >
      <Stack direction="row" spacing={2}>
        <Autocomplete
          options={options}
          input={input}
          setInput={setInput}
          success={!!streamer}
          placeholder="Enter a Streamer's name"
        />
        <Button onClick={clear} disabled={loading}>
          Clear
        </Button>
        <Button
          disabled={loading}
          onClick={fetch}
          sx={{ position: "relative" }}
        >
          {loading && (
            <CircularProgress sx={{ position: "absolute" }} size="25px" />
          )}
          Find
        </Button>
      </Stack>

      <Grid
        container
        p={1}
        border="1px solid white"
        borderRadius="10px"
        overflow="scroll"
        sx={{
          background: "rgba(255, 255, 255, 0.1)",
        }}
        data-scroll="false"
      >
        {topBroadcasters.map(({ title, src }) => (
          <Grid item xs={3} sm={3} md={3} lg={3} key={title} p={1}>
            <Image
              src={src}
              alt={title}
              width="100%"
              pointer
              borderRadius="10px"
              boxShadow="0 0 3px rgba(0, 0, 0, 0.7)"
              onClick={() => {
                setStreamer(null);
                setInput(title);
              }}
              sx={{
                opacity: 0.8,
                "&:active": { boxShadow: "none" },
                "&:hover": { opacity: 1 },
                transition: "opacity 300ms",
              }}
            />
          </Grid>
        ))}
      </Grid>
    </Stack>
  );
};
