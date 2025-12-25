using System.Security.Claims;

namespace GymTrackerProject.Services;

public interface IApplicationUserService
{
    public string GetUsersId();
}

public class ApplicationUserService : IApplicationUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public ApplicationUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public string GetUsersId() =>
        _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("User not authenticated.");
}
