using Microsoft.AspNetCore.Mvc;
using Pogulum.Data.Models;
using Pogulum.Server.Services;

namespace Pogulum.Server.Controllers;

public class HomeController : Controller
{
    private readonly GameService _gameService;

    private readonly BroadcasterService _broadcasterService;

    private readonly ActivityService _activityService;

    public HomeController(GameService gameService, BroadcasterService broadcasterService, ActivityService activityService)
    {
        _gameService = gameService;
        _broadcasterService = broadcasterService;
        _activityService = activityService;
    }

    [HttpGet]
    [Route("/")]
    public async Task<ActionResult> Index()
    {
        var jwt = Request.Cookies["sid"];
        return View(new HomeModel
        {
            TopGames = await _gameService.GetTopPopular(10),
            TopBroadcasters = await _broadcasterService.GetTopPopular(10),
            RecentActivities = await _activityService.GetAllActivites()
        });
    }
}

public class HomeModel
{
    public required List<Game> TopGames { get; set; }

    public required List<Broadcaster> TopBroadcasters { get; set; }

    public required List<Activity> RecentActivities { get; set; }
}