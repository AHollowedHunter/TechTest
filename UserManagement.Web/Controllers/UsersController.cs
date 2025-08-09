using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List(bool? isActive = null)
    {
        var users = isActive is null ? _userService.GetAll() : _userService.FilterByActive(isActive.Value);

        var items = users.Select(p => new UserListItemViewModel
        {
            Id       = p.Id,
            Forename = p.Forename,
            Surname  = p.Surname,
            Email    = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth,
        });

        var model = new UserListViewModel { Items = items.ToList() };

        return View(model);
    }
}
