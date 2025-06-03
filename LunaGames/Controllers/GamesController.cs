using LunaGames.Services;
using LunaGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LunaGames.Auth;

namespace LunaGames.Controllers;

[Authorize(Roles = "user")]
[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly GameService _gameService;
    private readonly IToken _token;

    public GamesController(ILogger<GamesController> logger, GameService gameService, IToken token)
    {
        _logger = logger;
        _gameService = gameService;
        _token = token;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] Game game)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _gameService.Create(game);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu erro no Create {@game}", game);
            return StatusCode(500, "Ocorreu um erro no Create");        
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] Game game, [FromRoute] int id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _gameService.Update(game, id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu erro no Update {@login}", game);
            return StatusCode(500, "Ocorreu um erro no Update");        
        }
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> List()
    {
        try
        {
            var games = await _gameService.List(_token.GetId());
            return Ok(games);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu erro no List");
            return StatusCode(500, "Ocorreu um erro no List");        
        }
    }

    [HttpPost("UserLink")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserLink([FromBody] Game game)
    {
        try
        {
            await _gameService.UserLink(_token.GetId());
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu erro no Create {@game}", game);
            return StatusCode(500, "Ocorreu um erro no Create");        
        }
    }
}

