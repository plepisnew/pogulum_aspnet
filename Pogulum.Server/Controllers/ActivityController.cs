using Microsoft.AspNetCore.Mvc;
using Pogulum.Data.Models;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Services;

namespace Pogulum.Server.Controllers;

[ApiController]
[Route("api/activites")]
public class ActivityController : ControllerBase
{
    private readonly ActivityService _service;

    public ActivityController(ActivityService service)
    {
        _service = service;
    }

    public class ActivityDto
    {
        public int UserId { get; set; }
        public string SubjectId { get; set; }
        public ActivityType ActivityType { get; set; }
    }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> Get()
    {
        try
        {
            return Ok(await _service.GetAllActivites());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Activity>> Get([FromRoute] int id)
    {
        try
        {
            return Ok(await _service.GetActivity(id));
        }
        catch (EntityNotFoundException<Activity> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ActivityDto dto)
    {
        try
        {
            var activity = await _service.CreateActivity(dto.UserId, dto.ActivityType, dto.SubjectId);
            return CreatedAtAction(nameof(Get), new { id = activity.Id }, activity);
        }
        catch (EntityNotFoundException<Activity> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _service.DeleteActivity(id);
            return Ok();
        }
        catch (EntityNotFoundException<Activity> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
