using LunaGames.Services;
using LunaGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LunaGames.Auth;

namespace LunaGames.Controllers;

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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _gameService.Create(game);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] Game game, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _gameService.Update(game, id);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> List()
    {
        var games = await _gameService.List(_token.GetId());
        return Ok(games);
    }

    [HttpPost("UserLink")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserLink([FromBody] Game game)
    {
        await _gameService.UserLink(_token.GetId());
        return Ok();
    }
}

