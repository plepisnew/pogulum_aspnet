import { CircularProgress, Grid, Stack } from "@mui/material";
import React, { useState } from "react";
import { TwitchApi } from "../../Api";
import { Game, TwitchGame } from "../../globals";
import { Button, TextField, Autocomplete } from "./StyledComponents";
import { Image } from "../../components/Image";
import { boxArtUrlToConcrete, twitchGameToGame } from "../../utils/UrlParser";
import { useSnackbar } from "notistack";
import { topGames } from "./top";

type Props = {
  game: Game | null;
  setGame: React.Dispatch<React.SetStateAction<Game | null>>;
  options: Game[];
};

export const GameFinder: React.FC<Props> = ({ game, setGame, options }) => {
  const [loading, setLoading] = useState<boolean>(false);
  const [input, setInput] = useState<string>("");

  const snackbar = useSnackbar();

  const fetch = async () => {
    try {
      setGame(null);
      setLoading(true);
      const res = await TwitchApi.get(`games?name=${input}`);
      setGame(twitchGameToGame(res.data.data[0] as TwitchGame));
    } catch (err) {
      console.error(err);
      snackbar.enqueueSnackbar({
        message:
          input.length == 0
            ? "Please enter a game name!"
            : `Unable to find a game named '${input}'`,
        autoHideDuration: 3000,
        variant: "error",
      });
    } finally {
      setLoading(false);
    }
  };

  const clear = () => {
    setInput("");
    setGame(null);
  };

  return (
    <Stack
      direction="column"
      spacing={2}
      height="40%"
      flex={1}
      data-scroll={false}
    >
      <Stack direction="row" spacing={2} mb={2}>
        <Autocomplete
          options={options}
          input={input}
          setInput={setInput}
          success={!!game}
          placeholder="Enter a Game name"
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
        {topGames.map(({ title, src }) => (
          <Grid item xs={3} sm={3} md={3} lg={3} key={title} p={1}>
            <Image
              src={boxArtUrlToConcrete(src, 200)}
              alt={title}
              width="100%"
              pointer
              borderRadius="10px"
              boxShadow="0 0 3px rgba(0, 0, 0, 0.7)"
              onClick={() => {
                setGame(null);
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
