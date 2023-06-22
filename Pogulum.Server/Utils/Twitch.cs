using System.Text.Json.Nodes;
using Microsoft.AspNetCore.WebUtilities;
using Pogulum.Data.Models;
using Pogulum.Server.Exceptions;

namespace Pogulum.Server.Utils;

public class Twitch
{
    public static readonly string BASE_URL = "https://api.twitch.tv/helix";

    public static readonly string GAMES_URL = BASE_URL + "/games";

    public static readonly string BROADCASTERS_URL = BASE_URL + "/users";

    public static readonly string CLIPS_URL = BASE_URL + "/clips";

    private readonly string AUTH_HEADER = "Bearer lw5w400gntfq0at35g9a6a18sid6pn";

    private readonly string CLIENT_ID = "0kvjan2jt8lf8qkhjolubt5ggih7ip";

    public async Task<Game> GetGameFromId(string id)
    {
        try
        {
            var param = new Dictionary<string, string>() { { nameof(id), id } };
            var url = new Uri(QueryHelpers.AddQueryString(GAMES_URL, param));

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Authorization", "Bearer lw5w400gntfq0at35g9a6a18sid6pn");
            request.Headers.Add("Client-Id", "0kvjan2jt8lf8qkhjolubt5ggih7ip");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            JsonNode root = JsonNode.Parse(await response.Content.ReadAsStringAsync())!;
            JsonNode data = root["data"]!.AsArray()[0]!;

            return new Game
            {
                Id = (string)data["id"]!,
                Name = (string)data["name"]!,
                BoxArtUrl = (string)data["box_art_url"]!,
                Popularity = 0
            };
        }
        catch (Exception e)
        {
            throw new EntityNotFoundException<Game>(nameof(id), id);
        }
    }

    public async Task<Game> GetGameFromName(string name)
    {
        try
        {
            var param = new Dictionary<string, string>() { { nameof(name), name } };
            var url = new Uri(QueryHelpers.AddQueryString(GAMES_URL, param));

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Authorization", AUTH_HEADER);
            request.Headers.Add("Client-Id", CLIENT_ID);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            JsonNode root = JsonNode.Parse(await response.Content.ReadAsStringAsync())!;
            JsonNode data = root["data"]!.AsArray()[0]!;

            return new Game
            {
                Id = (string)data["id"]!,
                Name = (string)data["name"]!,
                BoxArtUrl = (string)data["box_art_url"]!,
                Popularity = 0
            };
        }
        catch (Exception e)
        {
            throw new EntityNotFoundException<Game>(nameof(name), name);
        }
    }

    public async Task<Broadcaster> GetBroadcasterFromId(string id)
    {
        try
        {
            var param = new Dictionary<string, string> { { nameof(id), id } };
            var url = new Uri(QueryHelpers.AddQueryString(BROADCASTERS_URL, param));

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Authorization", AUTH_HEADER);
            request.Headers.Add("Client-Id", CLIENT_ID);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            JsonNode root = JsonNode.Parse(await response.Content.ReadAsStringAsync())!;
            JsonNode data = root["data"]!.AsArray()[0]!;

            return new Broadcaster
            {
                Id = (string)data["id"]!,
                Name = (string)data["display_name"]!,
                Description = (string)data["description"]!,
                ViewCount = (int)data["view_count"]!,
                ProfileImageUrl = (string)data["profile_image_url"]!,
                OfflineImageUrl = (string)data["offline_image_url"]!,
                CreatedAt = DateTime.Parse((string)data["created_at"]!),
            };
        }
        catch (Exception e)
        {
            throw new EntityNotFoundException<Broadcaster>(nameof(id), id);
        }
    }

    public async Task<Broadcaster> GetBroadcasterFromName(string name)
    {
        try
        {
            var param = new Dictionary<string, string> { { "login", name } };
            var url = new Uri(QueryHelpers.AddQueryString(BROADCASTERS_URL, param));

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Authorization", AUTH_HEADER);
            request.Headers.Add("Client-Id", CLIENT_ID);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            JsonNode root = JsonNode.Parse(await response.Content.ReadAsStringAsync())!;
            JsonNode data = root["data"]!.AsArray()[0]!;

            return new Broadcaster
            {
                Id = (string)data["id"]!,
                Name = (string)data["display_name"]!,
                Description = (string)data["description"]!,
                ViewCount = (int)data["view_count"]!,
                ProfileImageUrl = (string)data["profile_image_url"]!,
                OfflineImageUrl = (string)data["offline_image_url"]!,
                CreatedAt = DateTime.Parse((string)data["created_at"]!),
            };
        }
        catch (Exception e)
        {
            throw new EntityNotFoundException<Broadcaster>(nameof(name), name);
        }
    }

    public async Task<Clip> GetClipFromId(string id)
    {
        try
        {
            var param = new Dictionary<string, string> { { "id", id } };
            var url = new Uri(QueryHelpers.AddQueryString(CLIPS_URL, param));

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Authorization", AUTH_HEADER);
            request.Headers.Add("Client-Id", CLIENT_ID);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            JsonNode root = JsonNode.Parse(await response.Content.ReadAsStringAsync())!;
            JsonNode data = root["data"]!.AsArray()[0]!;

            return new Clip
            {
                Id = (string)data["id"]!,
                Url = (string)data["url"]!,
                EmbedUrl = (string)data["embed_url"]!,
                Broadcaster = new Broadcaster
                {
                    Id = (string)data["broadcaster_id"]!
                },
                Game = new Game
                {
                    Id = (string)data["game_id"]!
                },
                Title = (string)data["title"]!,
                ViewCount = (int)data["view_count"]!,
                CreatedAt = DateTime.Parse((string)data["created_at"]!),
                ThumbnailUrl = (string)data["thumbnail_url"]!,
                Duration = (double)data["duration"]!
            };
        }
        catch (Exception e)
        {
            throw new EntityNotFoundException<Clip>(nameof(id), id);
        }
    }
}