using System.Text.Json;
using Pogulum.Data.Models;
using Pogulum.Data.Repos.Concrete;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Utils;

namespace Pogulum.Server.Services;

public class ClipService
{
    private readonly ClipRepo _repo;
    private readonly GameRepo _gameRepo;
    private readonly BroadcasterRepo _broadcasterRepo;

    private readonly Twitch _twitch;

    public ClipService(ClipRepo repo, GameRepo gameRepo, BroadcasterRepo broadcasterRepo, Twitch twitch)
    {
        _repo = repo;
        _gameRepo = gameRepo;
        _broadcasterRepo = broadcasterRepo;
        _twitch = twitch;
    }

    public async Task<List<Clip>> GetAllClips()
    {
        return await _repo.GetAsync();
    }

    public async Task<Clip> GetClipById(string id)
    {
        var clip = await _repo.GetAsync(id);
        return clip ?? throw new EntityNotFoundException<Clip>(nameof(id), id);
    }

    public async Task<Clip?> GetClipByIdSafe(string id)
    {
        return await _repo.GetAsync(id);
    }

    // TODO: fix whatever this is
    public async Task<Clip> CreateClipFromId(string id)
    {
        var clip = await _twitch.GetClipFromId(id);

        var gameId = clip.Game.Id;
        var game = await _gameRepo.GetAsync(gameId);
        if (game == null)
        {
            game = await _twitch.GetGameFromId(gameId);
            await _gameRepo.CreateAsync(game);
        }

        var broadcasterId = clip.Broadcaster.Id;
        var broadcaster = await _broadcasterRepo.GetAsync(broadcasterId);
        if (broadcaster == null)
        {
            broadcaster = await _twitch.GetBroadcasterFromId(broadcasterId);
            await _broadcasterRepo.CreateAsync(broadcaster);
        }

        clip.Game = game;
        clip.Broadcaster = broadcaster;

        await _repo.CreateAsync(clip);
        return clip;
    }

    public async Task UpdateViewCount(string id)
    {
        var clip = await GetClipById(id);
        var twitchClip = await _twitch.GetClipFromId(id);
        clip.ViewCount = twitchClip.ViewCount;
        await _repo.UpdateAsync(clip);
    }

    public async Task DeleteClip(string id)
    {
        var clip = await GetClipById(id);
        await _repo.DeleteAsync(id);
    }
}