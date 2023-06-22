using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pogulum.Data.Models;
using Pogulum.Data.Repos.Concrete;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Utils;

namespace Pogulum.Server.Services;

public class AuthService
{
    private readonly UserRepo _userRepo;

    private readonly IConfiguration _configuration;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserRepo userRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _userRepo = userRepo;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User> RegisterUser(string username, string password)
    {
        bool userExists = (await _userRepo.GetAsync()).Any(u => u.Username == username);

        if (userExists)
            throw new OverwriteEntityException<User>(nameof(username), username);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Username = username,
            PasswordHash = hashedPassword,
        };

        await _userRepo.CreateAsync(user);

        return user;
    }

    public async Task<string> LoginUser(string username, string password)
    {
        var user = (await _userRepo.GetAsync()).Find(u => u.Username == username);

        if (user == null)
            throw new EntityNotFoundException<User>(nameof(username), username);

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid Username or Password!");

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("Jwt:TokenSeed").Value!
        ));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetName()
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new UnauthorizedAccessException("Unable to verify claims!");

        return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }

    public string GetRole()
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new UnauthorizedAccessException("Unable to verify claims!");

        return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
    }
}