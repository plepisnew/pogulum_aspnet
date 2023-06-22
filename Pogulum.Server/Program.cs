using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pogulum.Data.Repos.Concrete;
using Pogulum.Server.Ffmpeg;
using Pogulum.Server.Hubs;
using Pogulum.Server.Services;
using Pogulum.Server.Utils;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("Pages/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("Pages/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("Pages/Shared/{0}" + RazorViewEngine.ViewExtension);
});
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PogulumDbContext>(options =>
{
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("Default"), builder => builder.EnableRetryOnFailure());
});

builder.Services.AddScoped<UserRepo>().AddScoped<UserService>();
builder.Services.AddScoped<GameRepo>().AddScoped<GameService>();
builder.Services.AddScoped<BroadcasterRepo>().AddScoped<BroadcasterService>();
builder.Services.AddScoped<ClipRepo>().AddScoped<ClipService>();
builder.Services.AddScoped<SavedClipRepo>().AddScoped<SavedClipService>();
builder.Services.AddScoped<ActivityRepo>().AddScoped<ActivityService>();

builder.Services.AddScoped<VideoService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddSingleton<Twitch>();
builder.Services.AddSingleton<UrlFormatParser>();
builder.Services.AddSingleton<FfmpegWrapper>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("Jwt:TokenSeed").Value!
        ))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler("/Error");
    // app.UseHsts();
}


app.UseCors(policyBuilder =>
{
    policyBuilder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000", "https://localhost:3000");
});

app.MapHub<ChatHub>("/ws/chat");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<AuthMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.UseRouting();

app.Run();
