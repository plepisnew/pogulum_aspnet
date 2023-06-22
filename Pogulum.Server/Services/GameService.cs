using Pogulum.Data.Models;
using Pogulum.Data.Repos.Concrete;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Utils;

namespace Pogulum.Server.Services;

public class GameService
{
    private readonly GameRepo _repo;

    private readonly Twitch _twitch;

    public GameService(GameRepo repo, Twitch twitch)
    {
        _repo = repo;
        _twitch = twitch;
    }

    public async Task<List<Game>> GetAllGames()
    {
        return await _repo.GetAsync();
    }

    public async Task<Game> GetGameById(string id)
    {
        var game = await _repo.GetAsync(id);
        return game ?? throw new EntityNotFoundException<Game>(nameof(id), id);
    }

    public async Task<Game?> GetGameByIdSafe(string id)
    {
        return await _repo.GetAsync(id);
    }

    public async Task<List<Game>> GetTopPopular(int count)
    {
        var games = await _repo.GetAsync();
        games.Sort((game1, game2) =>
        {
            return game1.Popularity > game2.Popularity
                ? -1
                : game1.Popularity < game2.Popularity
                ? 1
                : 0;
        });

        return games.Where((_, index) => index < count).ToList();
    }

    public async Task<Game> CreateGameFromId(string id)
    {
        var game = await _twitch.GetGameFromId(id);
        await _repo.CreateAsync(game);
        return game;
    }

    public async Task<Game> CreateGameFromName(string name)
    {
        var game = await _twitch.GetGameFromName(name);
        await _repo.CreateAsync(game);
        return game;
    }

    public async Task IncrementGamePopularity(string id, int increment)
    {
        var game = await GetGameById(id);
        game.Popularity += increment;
        await _repo.UpdateAsync(game);
    }

    public async Task UpdateGame(Game game)
    {
        await GetGameById(game.Id);
        await _repo.UpdateAsync(game);
    }

    public async Task DeleteGame(string id)
    {
        await GetGameById(id);
        await _repo.DeleteAsync(id);
    }
}