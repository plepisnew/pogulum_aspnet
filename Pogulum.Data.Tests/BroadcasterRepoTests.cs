namespace Pogulum.Data.Tests;

public class BroadcasterRepoTests
{
    private readonly BroadcasterRepo _repo;

    private readonly ITestOutputHelper _output;

    private static int _broadcasterIndex = 0;

    public BroadcasterRepoTests(ITestOutputHelper output)
    {
        var context = InMemoryContext.GetDbContext();
        context.Database.EnsureCreated();
        _repo = new BroadcasterRepo(context);
        _output = output;
    }

    private async Task<Broadcaster> CreateBroadcaster()
    {
        var broadcaster = new Broadcaster
        {
            Id = _broadcasterIndex.ToString(),
            Name = $"jane_doe{_broadcasterIndex}",
            Description = "Hello! I stream games :>",
            ViewCount = Random.Shared.Next(100, 1000),
            CreatedAt = DateTime.Now.AddDays(Random.Shared.Next(-100, -10)),
            ProfileImageUrl = $"https://twitch.tv/assets/{_broadcasterIndex}_pfp.png",
            OfflineImageUrl = $"https://twitch.tv/assets/{_broadcasterIndex}_offline.png"
        };

        _broadcasterIndex++;
        await _repo.CreateAsync(broadcaster);

        return broadcaster;
    }

    [Fact]
    public async void CreateBroadcaster_StoresBroadcasterInDB()
    {
        var broadcaster = await CreateBroadcaster();

        var dbBroadcaster = await _repo.GetAsync(broadcaster.Id);

        Assert.NotNull(dbBroadcaster);
        Assert.Equal(broadcaster.Name, dbBroadcaster.Name);
    }

    [Fact]
    public async void CreateMultipleBroadcasters_CreatesExactAmountInDB()
    {
        var broadcasterCount = 4;
        for (int i = 0; i < broadcasterCount; i++)
        {
            await CreateBroadcaster();
        }

        var broadcastersInDb = await _repo.GetAsync();

        Assert.Equal(broadcasterCount, broadcastersInDb.Count());
    }

    [Fact]
    public async void DeleteBroadcaster_RemovesBroadcasterFromDB()
    {
        var broadcaster = await CreateBroadcaster();

        await _repo.DeleteAsync(broadcaster.Id);

        var dbBroadcaster = await _repo.GetAsync(broadcaster.Id);

        Assert.Null(dbBroadcaster);
    }

    [Fact]
    public async void UpdateBroadcaster_PropagatesChangesInDB()
    {
        var broadcaster = await CreateBroadcaster();

        var newName = "doe_jane";
        broadcaster.Name = newName;

        await _repo.UpdateAsync(broadcaster);

        var dbBroadcaster = (await _repo.GetAsync(broadcaster.Id))!;

        Assert.Equal(newName, dbBroadcaster.Name);
    }
}