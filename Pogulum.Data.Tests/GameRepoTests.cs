namespace Pogulum.Data.Tests;

public class GameRepoTests
{
    private readonly GameRepo _repo;

    private readonly ITestOutputHelper _output;

    private static int _gameIndex = 0;

    public GameRepoTests(ITestOutputHelper output)
    {
        _output = output;
        _repo = new GameRepo(InMemoryContext.GetDbContext());
    }

    private async Task<Game> CreateGame()
    {
        var dbGames = await _repo.GetAsync();

        var game = new Game
        {
            Id = _gameIndex.ToString(),
            Name = $"Counter-Strike: 1.{_gameIndex}",
            BoxArtUrl = "https://localhost/img/cs.png",
        };

        _gameIndex++;
        await _repo.CreateAsync(game);

        return game;
    }

    [Fact]
    public async void CreateGame_StoresGameInDB()
    {
        var game = await CreateGame();

        var dbGame = await _repo.GetAsync(game.Id);

        Assert.NotNull(dbGame);
        Assert.Equal(game.Name, dbGame.Name);
    }

    [Fact]
    public async void CreateMultipleGames_CreatesExactAmountInDB()
    {
        var gameCount = 3;
        for (int i = 0; i < gameCount; i++)
        {
            await CreateGame();
        }

        var gamesInDb = await _repo.GetAsync();

        Assert.Equal(gameCount, gamesInDb.Count());
    }

    [Fact]
    public async void DeleteGame_RemovesGameFromDB()
    {
        var game = await CreateGame();

        await _repo.DeleteAsync(game.Id);

        var dbGame = await _repo.GetAsync(game.Id);

        Assert.Null(dbGame);
    }

    [Fact]
    public async void UpdateGame_PropagatesChangesInDB()
    {
        var game = await CreateGame();

        var newName = "Corruped Strike: 1.6";
        game.Name = newName;

        await _repo.UpdateAsync(game);

        var dbGame = (await _repo.GetAsync(game.Id))!;

        Assert.Equal(newName, dbGame.Name);
    }
}