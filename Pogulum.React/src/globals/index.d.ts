export type Game = {
  id: string;
  name: string;
  boxArtUrl: string;
  igdbId?: string;
  popularity?: number;
};

export type TwitchGame = {
  id: string;
  name: string;
  box_art_url: string;
  igdb_id: string;
};

export type Broadcaster = {
  id: string;
  name: string;
  description: string;
  viewCount: number;
  profileImageUrl: string;
  offlineImageUrl: string;
  createdAt: string;
  popularity?: number;
};

export type TwitchBroadcaster = {
  id: string;
  display_name: string;
  description: string;
  view_count: number;
  profile_image_url: string;
  offline_image_url: string;
  created_at: string;
};

export type Clip = {
  id: string;
  url: string;
  embedUrl: string;
  broadcaster: Broadcaster;
  game: Game;
  title: string;
  viewCount: number;
  createdAt: string;
  thumbnailUrl: string;
  duration: number;
};

export type TwitchClip = {
  id: string;
  url: string;
  embed_url: string;
  broadcaster_id: string;
  game_id: string;
  title: string;
  view_count: number;
  created_at: string;
  thumbnail_url: string;
  duration: number;
};

export type UIClip = {
  clip: Clip;
  dupeIndex: number;
  open: boolean;
};
