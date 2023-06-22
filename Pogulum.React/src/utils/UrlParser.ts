import { TwitchApi } from "../Api";
import {
  Broadcaster,
  Clip,
  Game,
  TwitchBroadcaster,
  TwitchClip,
  TwitchGame,
} from "../globals";

export const boxArtUrlToConcrete = (boxArtUrl: string, size: number = 200) => {
  return boxArtUrl.replace("{width}", `${size}`).replace("{height}", `${size}`);
};

export const twitchGameToGame = (twitchGame: TwitchGame): Game => {
  return {
    id: twitchGame.id,
    name: twitchGame.name,
    boxArtUrl: twitchGame.box_art_url,
    igdbId: twitchGame.igdb_id,
  };
};

export const twitchBroadcasterToBroadcaster = (
  twitchBroadcaster: TwitchBroadcaster
): Broadcaster => {
  return {
    id: twitchBroadcaster.id,
    name: twitchBroadcaster.display_name,
    description: twitchBroadcaster.description,
    viewCount: twitchBroadcaster.view_count,
    profileImageUrl: twitchBroadcaster.profile_image_url,
    offlineImageUrl: twitchBroadcaster.offline_image_url,
    createdAt: twitchBroadcaster.created_at,
  };
};

export const twitchClipToClip = async (
  twitchClip: TwitchClip
): Promise<Clip> => {
  const broadcaster = (
    await TwitchApi.get(`users?id=${twitchClip.broadcaster_id}`)
  ).data.data[0];
  const game = (await TwitchApi.get(`games?id=${twitchClip.game_id}`)).data
    .data[0];

  const clip: Clip = {
    id: twitchClip.id,
    url: twitchClip.url,
    viewCount: twitchClip.view_count,
    title: twitchClip.title,
    duration: twitchClip.duration,
    embedUrl: twitchClip.embed_url,
    createdAt: twitchClip.created_at,
    thumbnailUrl: twitchClip.thumbnail_url,
    broadcaster: twitchBroadcasterToBroadcaster(broadcaster),
    game: twitchGameToGame(game),
  };

  return clip;
};

export const gameNameToUrl = (name: string): string =>
  `https://www.twitch.tv/directory/game/${encodeURIComponent(name)}`;

export const streamerNameToUrl = (name: string): string => "";
