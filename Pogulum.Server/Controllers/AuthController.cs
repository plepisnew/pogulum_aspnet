using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Pogulum.Data.Models;
using Pogulum.Data.Models.Dto;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Services;

namespace Pogulum.Server.Controllers;

public class AuthController : Controller
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("login")]
    public ActionResult LoginPage()
    {
        return View("Login");
    }

    [HttpGet]
    [Route("register")]
    public ActionResult RegisterPage()
    {
        return View("Register");
    }

    [HttpGet]
    [Authorize]
    [Route("api/[controller]")]
    public ActionResult<object> Identity()
    {
        var username = User!.Identity!.Name;
        var role = User.FindFirstValue(ClaimTypes.Role);
        return new { username, role };
    }

    [HttpPost]
    [Route("api/[controller]/Register")]
    public async Task<ActionResult> Register([FromForm] UserDto userDto)
    {
        try
        {
            if (userDto.Password != userDto.PasswordRepeated)
            {
                ViewData["Error"] = "Passwords don't match!";
                return View("Register");
            }
            var user = await _service.RegisterUser(userDto.Username, userDto.Password);
            return LocalRedirect("/login");
        }
        catch (OverwriteEntityException<User> e)
        {
            ViewData["Error"] = "Selected username already exists!";
            return View("Register");
        }
        catch (Exception e)
        {
            ViewData["Error"] = "Error registering!";
            return View("Register");
        }
    }

    [HttpPost]
    [Route("api/[controller]/Login")]
    public async Task<ActionResult> Login([FromForm] UserDto userDto)
    {
        try
        {
            var token = await _service.LoginUser(userDto.Username, userDto.Password);

            var cookieOptions = new CookieOptions
            {
                Secure = Request.IsHttps,
                Domain = "localhost"
            };

            Response.Cookies.Append("sid", token, cookieOptions);
            return LocalRedirect("/");
        }
        catch (EntityNotFoundException<User> e)
        {
            ViewData["Error"] = "Invalid username or password!";
            return View("Login");
        }
        catch (UnauthorizedAccessException e)
        {
            ViewData["Error"] = "Invalid username or password!";
            return View("Login");
        }
        catch (Exception e)
        {
            ViewData["Error"] = "Error signing in!";
            return View("Login");
        }
    }

    [HttpGet]
    [Route("api/[controller]/Logout")]
    public ActionResult Logout()
    {
        var jwt = Request.Cookies["sid"];
        if (jwt == null)
        {
            return BadRequest("Cookie `sid` could not be found.");
        }

        Response.Cookies.Delete("sid", new CookieOptions
        {
            Expires = DateTime.UtcNow.AddHours(-1)
        });

        return LocalRedirect("/login");
    }
}