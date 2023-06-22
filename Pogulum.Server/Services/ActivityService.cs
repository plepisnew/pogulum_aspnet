using Pogulum.Data.Models;
using Pogulum.Data.Repos.Concrete;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Utils;

namespace Pogulum.Server.Services;

public enum ActivityType
{
    LikeGame = 0,
    LikeBroadcaster = 1,
    LikeClip = 2
}

public class ActivityService
{
    private readonly ActivityRepo _repo;

    private readonly UserService _userService;
    private readonly BroadcasterService _broadcasterService;
    private readonly GameService _gameService;
    private readonly ClipService _clipService;

    private readonly Twitch _twitch;

    public ActivityService(ActivityRepo repo, UserService userService, BroadcasterService broadcasterService, GameService gameService, ClipService clipService, Twitch twitch)
    {
        _repo = repo;
        _broadcasterService = broadcasterService;
        _userService = userService;
        _gameService = gameService;
        _clipService = clipService;
        _twitch = twitch;
    }

    public async Task<List<Activity>> GetAllActivites()
    {
        var activities = await _repo.GetAsync();
        activities.Sort((a1, a2) =>
        {
            return a1.CreatedAt > a2.CreatedAt
            ? -1
            : a1.CreatedAt < a2.CreatedAt
            ? 1
            : 0;
        });
        return activities;
    }

    public async Task<Activity> GetActivity(int id)
    {
        return await _repo.GetAsync(id) ?? throw new EntityNotFoundException<Activity>(nameof(id), id);
    }

    public async Task<Activity> CreateActivity(int userId, ActivityType activityType, string subjectId)
    {
        var user = await _userService.GetUserById(userId);
        var activity = new Activity { Actor = user };

        switch (activityType)
        {
            case ActivityType.LikeGame:
                var game = await _gameService.GetGameByIdSafe(subjectId)
                    ?? await _gameService.CreateGameFromId(subjectId);

                activity.GameSubject = game;
                user.FavoriteGames.Add(game);
                await _gameService.IncrementGamePopularity(subjectId, 1);
                break;
            case ActivityType.LikeBroadcaster:
                System.Console.WriteLine("Creating broadcaster");
                var broadcaster = await _broadcasterService.GetBroadcasterByIdSafe(subjectId)
                    ?? await _broadcasterService.CreateBroadcasterFromId(subjectId);

                activity.BroadcasterSubject = broadcaster;
                user.FavoriteBroadcasters.Add(broadcaster);
                System.Console.WriteLine("Broadcaster created");

                await _broadcasterService.IncrementBroadcasterPopularity(subjectId, 1);
                break;
            case ActivityType.LikeClip:
                var clip = await _clipService.GetClipByIdSafe(subjectId)
                    ?? await _clipService.CreateClipFromId(subjectId);

                activity.ClipSubject = clip;
                user.FavoriteClips.Add(clip);
                break;
        }
        await _repo.CreateAsync(activity);
        await _userService.UpdateUser(user);
        return activity;
    }

    public async Task DeleteActivity(int id)
    {
        await _repo.DeleteAsync(id);
    }
}