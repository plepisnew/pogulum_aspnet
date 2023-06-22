using Microsoft.AspNetCore.Mvc;
using Pogulum.Data.Models;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Services;

namespace Pogulum.Server.Controllers;

[Route("api/[controller]s")]
public class BroadcasterController : ControllerBase
{
    private readonly BroadcasterService _service;

    public BroadcasterController(BroadcasterService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Broadcaster>>> Get()
    {
        try
        {
            return Ok(await _service.GetAllBroadcasters());
        }
        catch (EntityNotFoundException<Broadcaster> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Broadcaster>> Get([FromRoute] string id)
    {
        try
        {
            return Ok(await _service.GetBroadcasterById(id));
        }
        catch (EntityNotFoundException<Broadcaster> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("WithId")]
    public async Task<ActionResult> CreateFromId([FromBody] string id)
    {
        try
        {
            var broadcaster = await _service.CreateBroadcasterFromId(id);
            return CreatedAtAction(nameof(Get), new { id = broadcaster.Id }, broadcaster);
        }
        catch (EntityNotFoundException<Broadcaster> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("WithName")]
    public async Task<ActionResult> CreateFromName([FromBody] string name)
    {
        try
        {
            var broadcaster = await _service.CreateBroadcasterFromName(name);
            return CreatedAtAction(nameof(Get), new { id = broadcaster.Id }, broadcaster);
        }
        catch (EntityNotFoundException<Broadcaster> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> Put([FromRoute] string id)
    {
        try
        {
            await _service.UpdateViewCount(id);
            return Ok();
        }
        catch (EntityNotFoundException<Broadcaster> e)
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
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        try
        {
            await _service.DeleteBroadcaster(id);
            return Ok();
        }
        catch (EntityNotFoundException<Broadcaster> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}