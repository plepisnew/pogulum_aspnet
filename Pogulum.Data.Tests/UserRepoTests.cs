namespace Pogulum.Data.Tests;

public class UserRepoTests
{
    private readonly UserRepo _repo;

    private readonly ITestOutputHelper _output;

    private static int _userIndex = 0;

    public UserRepoTests(ITestOutputHelper output)
    {
        _repo = new UserRepo(InMemoryContext.GetDbContext());
        _output = output;
    }

    private async Task<User> CreateUser()
    {
        var user = new User
        {
            Username = $"jane_doe{_userIndex}",
            PasswordHash = "###"
        };

        _userIndex++;
        await _repo.CreateAsync(user);

        return user;
    }

    [Fact]
    public async void CreateUser_StoresUserInDB()
    {
        var user = await CreateUser();

        var dbUser = await _repo.GetAsync(user.Id);

        Assert.NotNull(dbUser);
        Assert.Equal(user.Username, dbUser.Username);
    }

    [Fact]
    public async void CreateMultipleUsers_CreatesExactAmountInDB()
    {
        var userCount = 4;
        for (int i = 0; i < userCount; i++)
        {
            await CreateUser();
        }

        var usersInDb = await _repo.GetAsync();

        Assert.Equal(userCount, usersInDb.Count());
    }

    [Fact]
    public async void DeleteUser_RemovesUserFromDB()
    {
        var user = await CreateUser();

        await _repo.DeleteAsync(user.Id);

        var dbUser = await _repo.GetAsync(user.Id);

        Assert.Null(dbUser);
    }

    [Fact]
    public async void UpdateUser_PropagatesChangesInDB()
    {
        var user = await CreateUser();

        var newUsername = "doe_jane";
        user.Username = newUsername;

        await _repo.UpdateAsync(user);

        var dbUser = (await _repo.GetAsync(user.Id))!;

        Assert.Equal(newUsername, dbUser.Username);
    }
}