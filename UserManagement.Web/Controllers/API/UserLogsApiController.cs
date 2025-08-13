using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.WebMS.Controllers;

[ApiController]
[Route("api/logs/user")]
public class UserLogsApiController(IUserLogService userLogService) : Controller
{
    private readonly IUserLogService _userLogService = userLogService;

    [HttpGet]
    public Task<IEnumerable<UserLog>> List()
        => _userLogService.GetAllAsync();

    [HttpGet("{userId:long}")]
    public Task<IEnumerable<UserLog>> GetByUserIdAsync(long userId)
        => _userLogService.GetForUserAsync(userId);
}
