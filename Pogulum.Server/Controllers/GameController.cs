using Microsoft.AspNetCore.Mvc;
using Pogulum.Data.Models;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Services;

namespace Pogulum.Server.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class GameController : ControllerBase
{
    private readonly GameService _service;

    public GameController(GameService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Game>>> Get()
    {
        return Ok(await _service.GetAllGames());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Game>> Get([FromRoute] string id)
    {
        try
        {
            return Ok(await _service.GetGameById(id));
        }
        catch (EntityNotFoundException<Game> e)
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
            var game = await _service.CreateGameFromId(id);
            return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
        }
        catch (EntityNotFoundException<Game> e)
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
            var game = await _service.CreateGameFromName(name);
            return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
        }
        catch (EntityNotFoundException<Game> e)
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
    public async Task<ActionResult> IncrementPopularity([FromRoute] string id, [FromBody] int increment)
    {
        try
        {
            await _service.IncrementGamePopularity(id, increment);
            return Ok();
        }
        catch (EntityNotFoundException<Game> e)
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
            await _service.DeleteGame(id);
            return Ok();
        }
        catch (EntityNotFoundException<Game> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}