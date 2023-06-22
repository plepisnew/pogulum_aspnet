using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pogulum.Data.Models;
using Pogulum.Data.Models.Dto;
using Pogulum.Server.Exceptions;
using Pogulum.Server.Services;
using Pogulum.Server.Utils;

namespace Pogulum.Server.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        return Ok(await _service.GetAllUsers());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<User>> Get([FromRoute] int id)
    {
        try
        {
            return Ok(await _service.GetUserById(id));
        }
        catch (EntityNotFoundException<User> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] User user)
    {
        try
        {
            await _service.UpdateUser(user);
            return Ok();
        }
        catch (EntityNotFoundException<User> e)
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _service.DeleteUser(id);
            return Ok();
        }
        catch (EntityNotFoundException<User> e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}