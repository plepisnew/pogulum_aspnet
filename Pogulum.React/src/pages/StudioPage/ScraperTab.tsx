import { Box, Stack } from "@mui/material";
import React, { useState } from "react";
import { Broadcaster, Game, TwitchClip, UIClip } from "../../globals";
import { Button, Divider } from "./StyledComponents";
import { GameFinder } from "./GameFinder";
import { StreamerFinder } from "./StreamerFinder";
import { ClipPreview } from "./ClipPreview";
import { TwitchApi } from "../../Api";
import CircularProgress from "@mui/material/CircularProgress";
import { useCookies } from "../../utils/Cookies";

type Props = {
  addCartItem: (clip: UIClip) => void;
  presetGames: Game[];
  presetBroadcasters: Broadcaster[];
};

const MAX_CLIPS = 100;
const CLIPS_PER_PAGE = 25;

export const ScraperTab: React.FC<Props> = ({
  presetGames,
  presetBroadcasters,
  addCartItem,
}) => {
  const { cookies } = useCookies();

  const [game, setGame] = useState<Game | null>(null);
  const [streamer, setStreamer] = useState<Broadcaster | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const [clips, setClips] = useState<TwitchClip[]>([]);
  const [storedClips, setStoredClips] = useState<TwitchClip[]>([]);

  const [page, setPage] = useState<number>(0);
  const [cursor, setCursor] = useState<string>("");

  const findClips = async () => {
    try {
      const newPage = 1;
      setLoading(true);
      setPage(newPage);
      setClips([]);
      setStoredClips([]);

      const params = new URLSearchParams();
      if (game) params.append("game_id", game.id);
      if (streamer) params.append("broadcaster_id", streamer.id);
      params.append("first", MAX_CLIPS.toString());
      const res = await TwitchApi.get(`clips?${params.toString()}`);

      setPage(1);
      setCursor(res.data.pagination.cursor);
      const newClips = res.data.data as TwitchClip[];
      setStoredClips(newClips);
      setClips(
        newClips.slice((newPage - 1) * CLIPS_PER_PAGE, newPage * CLIPS_PER_PAGE)
      );
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const onScrollRight = async () => {
    try {
      const newPage = page + 1;
      setPage(newPage);
      if (newPage * CLIPS_PER_PAGE > storedClips.length) {
        const params = new URLSearchParams();
        if (game) params.append("game_id", game.id);
        if (streamer) params.append("broadcaster_id", streamer.id);
        params.append("first", MAX_CLIPS.toString());
        params.append("after", cursor);

        const res = await TwitchApi.get(`clips?${params.toString()}`);
        setStoredClips([...storedClips, ...(res.data.data as TwitchClip[])]);
        setCursor(res.data.pagination.cursor);
      }
      setClips(
        storedClips.slice(
          (newPage - 1) * CLIPS_PER_PAGE,
          newPage * CLIPS_PER_PAGE
        )
      );
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const onScrollLeft = () => {
    const newPage = page - 1;
    setPage(newPage);
    setClips(
      storedClips.slice(
        (newPage - 1) * CLIPS_PER_PAGE,
        newPage * CLIPS_PER_PAGE
      )
    );
  };

  return (
    <Box display="flex" height="100%" p={4} gap={4}>
      <Stack width="50%" spacing={2} divider={<Divider />}>
        <GameFinder game={game} setGame={setGame} options={presetGames} />
        <StreamerFinder
          streamer={streamer}
          setStreamer={setStreamer}
          options={presetBroadcasters}
        />
        <Button
          disabled={loading || !(streamer || game)}
          onClick={findClips}
          sx={{ position: "relative" }}
        >
          Find clips
          {loading && (
            <CircularProgress size="25px" sx={{ position: "absolute" }} />
          )}
        </Button>
      </Stack>
      <Stack width="50%" spacing={4}>
        <ClipPreview
          clips={clips}
          page={page}
          onScrollLeft={onScrollLeft}
          onScrollRight={onScrollRight}
          addCartItem={addCartItem}
          clipsLoading={loading}
        />
      </Stack>
    </Box>
  );
};
