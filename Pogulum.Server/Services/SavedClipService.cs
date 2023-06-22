using Pogulum.Data.Models;
using Pogulum.Data.Repos.Concrete;
using Pogulum.Server.Exceptions;

namespace Pogulum.Server.Services;

public class SavedClipService
{
    private readonly SavedClipRepo _repo;

    private readonly ClipRepo _clipRepo;

    private readonly UserRepo _userRepo;

    public SavedClipService(SavedClipRepo repo, ClipRepo clipRepo, UserRepo userRepo)
    {
        _repo = repo;
        _clipRepo = clipRepo;
        _userRepo = userRepo;
    }

    public async Task<List<SavedClip>> GetAllSavedClips()
    {
        return await _repo.GetAsync();
    }

    public async Task<SavedClip> GetSavedClipById(int id)
    {
        var savedClip = await _repo.GetAsync(id);
        return savedClip ?? throw new EntityNotFoundException<SavedClip>(nameof(id), id);
    }

    public async Task CreateSavedClip(SavedClip savedClip)
    {
        string[] durations = savedClip.ClipDurations.Split(",");

        if (durations.Length != savedClip.Clips.Count())
            throw new ArgumentOutOfRangeException("Duration count must match clip count!");

        var clips = await Task.WhenAll(savedClip.Clips.Select(async c =>
        {
            return await _clipRepo.GetAsync(c.Id) ?? throw new EntityNotFoundException<Clip>(nameof(c.Id), c.Id);
        }));

        savedClip.Clips = clips.ToList();

        var userId = savedClip.Creator.Id;
        savedClip.Creator = await _userRepo.GetAsync(userId) ?? throw new EntityNotFoundException<User>(nameof(userId), userId);

        await _repo.CreateAsync(savedClip);
    }

    public async Task UpdateSavedClip(SavedClip savedClip)
    {
        await _repo.UpdateAsync(savedClip);
    }

    public async Task DeleteSavedClip(SavedClip savedClip)
    {
        await _repo.DeleteAsync(savedClip.Id);
    }
}