using Pogulum.Data.Models;
using Pogulum.Data.Repos.Concrete;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Utils;

namespace Pogulum.Server.Services;

public class BroadcasterService
{
    private readonly BroadcasterRepo _repo;

    private readonly Twitch _twitch;

    public BroadcasterService(BroadcasterRepo repo, Twitch twitch)
    {
        _repo = repo;
        _twitch = twitch;
    }

    public async Task<List<Broadcaster>> GetAllBroadcasters()
    {
        return await _repo.GetAsync();
    }

    public async Task<Broadcaster> GetBroadcasterById(string id)
    {
        var broadcaster = await _repo.GetAsync(id);
        return broadcaster ?? throw new EntityNotFoundException<Broadcaster>(nameof(id), id);
    }

    public async Task<Broadcaster?> GetBroadcasterByIdSafe(string id)
    {
        return await _repo.GetAsync(id);
    }

    public async Task<List<Broadcaster>> GetTopPopular(int count)
    {
        var broadcasters = await _repo.GetAsync();
        broadcasters.Sort((broadcaster1, broadcaster2) =>
        {
            return broadcaster1.Popularity > broadcaster2.Popularity
                ? 1
                : broadcaster1.Popularity < broadcaster2.Popularity
                ? -1
                : 0;
        });

        return broadcasters.Where((_, index) => index < count).ToList();
    }

    public async Task<Broadcaster> CreateBroadcasterFromId(string id)
    {
        var broadcaster = await _twitch.GetBroadcasterFromId(id);
        await _repo.CreateAsync(broadcaster);
        return broadcaster;
    }

    public async Task<Broadcaster> CreateBroadcasterFromName(string name)
    {
        var broadcaster = await _twitch.GetBroadcasterFromName(name);
        await _repo.CreateAsync(broadcaster);
        return broadcaster;
    }

    public async Task IncrementBroadcasterPopularity(string id, int increment)
    {
        var broadcaster = await GetBroadcasterById(id);
        broadcaster.Popularity += increment;
        await _repo.UpdateAsync(broadcaster);
    }

    public async Task UpdateViewCount(string id)
    {
        var user = await GetBroadcasterById(id);
        var twitchUser = await _twitch.GetBroadcasterFromId(id);
        user.ViewCount = twitchUser.ViewCount;
        await _repo.UpdateAsync(user);
    }

    public async Task DeleteBroadcaster(string id)
    {
        await GetBroadcasterById(id);
        await _repo.DeleteAsync(id);
    }
}