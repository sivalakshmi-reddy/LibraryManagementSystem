using FluentValidation;
using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        
        if (string.IsNullOrEmpty(command.Email) || string.IsNullOrEmpty(command.Password))
        {
            return BadRequest("Email and password are required");
        }

        if (command.Password.Length < 6)
        {
            return BadRequest("Password must be at least 6 characters");
        }

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}