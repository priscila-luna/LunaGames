using System.Security.Claims;
using System.Text;
using LunaGames.Services;
using LunaGames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LunaGames.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration configuration, AuthenticationService authenticationService)
    {
        _logger = logger;
        _configuration = configuration;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        string token = await _authenticationService.Login(login);

        if (token != "")
        {
            return Ok(new { token });
        }
        else
        {
            return Unauthorized();
        }
    }
}

