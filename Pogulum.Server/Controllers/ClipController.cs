using Microsoft.AspNetCore.Mvc;
using Pogulum.Data.Models;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Services;

namespace Pogulum.Server.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class ClipController : ControllerBase
{
    private readonly ClipService _service;

    public ClipController(ClipService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Clip>>> Get()
    {
        try
        {
            return Ok(await _service.GetAllClips());
        }
        catch (EntityNotFoundException<Clip> e)
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
    public async Task<ActionResult<Clip>> Get([FromRoute] string id)
    {
        try
        {
            return Ok(await _service.GetClipById(id));
        }
        catch (EntityNotFoundException<Clip> e)
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
            var clip = await _service.CreateClipFromId(id);
            return CreatedAtAction(nameof(Get), new { id = clip.Id }, clip);
        }
        catch (EntityNotFoundException<Clip> e)
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
        catch (EntityNotFoundException<Clip> e)
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
            await _service.DeleteClip(id);
            return Ok();
        }
        catch (EntityNotFoundException<Clip> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}