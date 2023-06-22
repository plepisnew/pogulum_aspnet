using Microsoft.AspNetCore.Mvc;
using Pogulum.Data.Models;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Services;

namespace Pogulum.Server.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class SavedClipController : ControllerBase
{
    private readonly SavedClipService _service;

    public SavedClipController(SavedClipService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<SavedClip>>> Get()
    {
        try
        {
            return Ok(await _service.GetAllSavedClips());
        }
        catch (EntityNotFoundException<SavedClip> e)
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
    public async Task<ActionResult<SavedClip>> Get(int id)
    {
        try
        {
            return Ok(await _service.GetSavedClipById(id));
        }
        catch (EntityNotFoundException<SavedClip> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(SavedClip savedClip)
    {
        try
        {
            var createdSavedClip = _service.CreateSavedClip(savedClip);
            return CreatedAtAction(nameof(Get), new { id = createdSavedClip.Id }, createdSavedClip);
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