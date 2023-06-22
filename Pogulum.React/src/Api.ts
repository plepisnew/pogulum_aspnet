import axios from "axios";

export const Api = axios.create({
  baseURL: "https://localhost:7092/api",
});

export const TwitchApi = axios.create({
  baseURL: "https://api.twitch.tv/helix",
  headers: {
    "Client-Id": "0kvjan2jt8lf8qkhjolubt5ggih7ip",
    Authorization: "Bearer lw5w400gntfq0at35g9a6a18sid6pn",
  },
});
