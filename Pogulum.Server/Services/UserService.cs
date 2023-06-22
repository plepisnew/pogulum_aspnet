using Pogulum.Data.Models;
using Pogulum.Data.Repos.Concrete;
using Pogulum.Server.Exceptions;

namespace Pogulum.Server.Services;

public class UserService
{
    private readonly UserRepo _repo;

    public UserService(UserRepo repo)
    {
        _repo = repo;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _repo.GetAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _repo.GetAsync(id);
        return user ?? throw new EntityNotFoundException<User>(nameof(id), id);
    }

    public async Task UpdateUser(User user)
    {
        await GetUserById(user.Id);
        await _repo.UpdateAsync(user);
    }

    public async Task DeleteUser(int id)
    {
        await GetUserById(id);
        await _repo.DeleteAsync(id);
    }
}