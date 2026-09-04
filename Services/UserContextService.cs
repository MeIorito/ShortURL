using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ShortURL.Exceptions;

namespace ShortURL.Services;

public class UserContextService
{
    private readonly IHttpContextAccessor _context;

    public UserContextService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public async Task<Guid> GetCurrentUserSub()
    {
        // TODO create custom context exceptions
        var user = (_context.HttpContext?.User) ?? throw new InvalidCredentialsException();

        if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
        {
            throw new InvalidCredentialsException();
        }
        
        return userId;
    }
}