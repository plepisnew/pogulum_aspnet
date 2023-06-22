namespace Pogulum.Data.Tests;

public class ClipRepoTests
{
    private readonly BroadcasterRepo _broadcasterRepo;

    private readonly GameRepo _gameRepo;

    private readonly ClipRepo _clipRepo;

    private readonly ITestOutputHelper _output;

    private static int _clipIndex = 0;

    public ClipRepoTests()
    {
        var context = InMemoryContext.GetDbContext();
        _broadcasterRepo = new BroadcasterRepo(context);
        _gameRepo = new GameRepo(context);
        _clipRepo = new ClipRepo(context);
    }

    public async Task<Clip> CreateClip()
    {
        var broadcaster = new Broadcaster
        {
            Id = _clipIndex.ToString(),
            Name = $"jane_doe{_clipIndex}",
            Description = "Hello! I stream games :>",
            ViewCount = Random.Shared.Next(100, 1000),
            CreatedAt = DateTime.Now.AddDays(Random.Shared.Next(-100, -10)),
            ProfileImageUrl = $"https://twitch.tv/assets/{_clipIndex}_pfp.png",
            OfflineImageUrl = $"https://twitch.tv/assets/{_clipIndex}_offline.png"
        };

        await _broadcasterRepo.CreateAsync(broadcaster);

        var game = new Game
        {
            Id = _clipIndex.ToString(),
            Name = $"Counter-Strike: 1.{_clipIndex}",
            BoxArtUrl = "https://localhost/img/cs.png",
        };

        await _gameRepo.CreateAsync(game);

        var clip = new Clip
        {
            Id = _clipIndex.ToString(),
            Url = $"https://twitch.tv/clips/{_clipIndex}",
            EmbedUrl = $"https://twitch.tv/clips_embed/{_clipIndex}",
            Broadcaster = broadcaster,
            Game = game,
            Title = $"Streaming {game.Name}",
            ViewCount = Random.Shared.Next(100, 100),
            CreatedAt = DateTime.Now,
            ThumbnailUrl = $"https://twitch.tv/assets/thumb/{_clipIndex}",
            Duration = Random.Shared.Next(10, 120),
        };

        _clipIndex++;

        await _clipRepo.CreateAsync(clip);

        return clip;
    }

    [Fact]
    public async void CreateClip_StoresClipInDB()
    {
        var clip = await CreateClip();

        var dbClip = await _clipRepo.GetAsync(clip.Id);

        Assert.NotNull(dbClip);
        Assert.Equal(clip.Title, dbClip.Title);
        Assert.NotNull(clip.Game);
        Assert.NotNull(clip.Broadcaster);
    }

    [Fact]
    public async void CreateMultipleClips_CreatesExactAmountInDB()
    {
        int clipCount = 6;
        for (int i = 0; i < clipCount; i++)
        {
            await CreateClip();
        }

        var dbClips = await _clipRepo.GetAsync();

        Assert.Equal(clipCount, dbClips.Count());
    }

    [Fact]
    public async void DeleteClip_RemovesClipFromDB()
    {
        var clip = await CreateClip();

        await _clipRepo.DeleteAsync(clip.Id);

        var dbClip = await _clipRepo.GetAsync(clip.Id);

        Assert.Null(dbClip);
    }

    [Fact]
    public async void UpdateClip_PropagatesChangeInDB()
    {
        var clip = await CreateClip();

        double expectedClipDuration = 50.2;
        string expectedGameName = "StarCraft";
        int expectedBroadcasterPopularity = 5;

        clip.Duration = expectedClipDuration;
        clip.Game.Name = expectedGameName;
        clip.Broadcaster.Popularity = expectedBroadcasterPopularity;

        await _clipRepo.UpdateAsync(clip);

        var dbClip = (await _clipRepo.GetAsync(clip.Id))!;

        Assert.Equal(dbClip.Game.Name, expectedGameName);
        Assert.Equal(dbClip.Broadcaster.Popularity, expectedBroadcasterPopularity);
        Assert.Equal(dbClip.Duration, expectedClipDuration, 0.1);
    }
}