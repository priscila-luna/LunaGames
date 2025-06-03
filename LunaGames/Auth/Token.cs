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
            _claims.Claims.Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value
        );
    }

    public string GetRole()
    {
        return _claims.Claims.Single(c => c.Type == "Role").Value;
    }
}