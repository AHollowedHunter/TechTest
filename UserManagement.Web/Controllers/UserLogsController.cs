using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.UserLogs;

namespace UserManagement.WebMS.Controllers;

[Route("[controller]")]
public class UserLogsController : Controller
{
    private readonly IUserLogService _userLogService;

    public UserLogsController(IUserLogService userLogService)
    {
        _userLogService = userLogService;
    }

    [HttpGet]
    public ViewResult List()
    {
        var items = _userLogService.GetAll().Select(UserLogListItemViewModel.FromUserLog);

        var model = new UserLogListViewModel() { Items = items.ToList() };

        return View(model);
    }
}
