using System.Security.Claims;

namespace LunaGames.Auth;

public class Token: IToken
{
    private readonly ClaimsPrincipal _claims;

    public Token(IHttpContextAccessor httpContextAccessor)
    {
        _claims = httpContextAccessor.HttpContext.User;
    }

    public int GetId()
    {
        return Convert.ToInt32(
            _claims.Claims.Single(c => c.Type == "NameIdentifier").Value
        );
    }

    public string GetRole()
    {
        return _claims.Claims.Single(c => c.Type == "Role").Value;
    }
}