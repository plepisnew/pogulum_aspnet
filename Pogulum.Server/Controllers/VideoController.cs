using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pogulum.Server.Services;

namespace Pogulum.Server.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class VideoController : ControllerBase
{
    private readonly VideoService _service;

    public VideoController(VideoService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("{extension}")]
    public async Task<ActionResult> Concatenate([FromBody] List<string> clipIds, [FromRoute] string extension = "mp4")
    {

        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        try
        {
            var videoBytes = await _service.ConcatenateClips(clipIds, extension, Convert.ToBoolean(Request.Query["use_clip_ids"]));
            return File(videoBytes, $"video/{extension}", $"output.{extension}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}