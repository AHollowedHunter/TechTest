using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserLogService _userLogService;

    public UsersController(IUserService userService, IUserLogService userLogService)
    {
        _userService         = userService;
        _userLogService = userLogService;
    }

    [HttpGet]
    public ViewResult List(bool? isActive = null)
    {
        var users = isActive is null ? _userService.GetAll() : _userService.FilterByActive(isActive.Value);

        var items = users.Select(UserListItemViewModel.FromUser);

        var model = new UserListViewModel { Items = items.ToList() };

        return View(model);
    }

    [HttpGet("{id:long}")]
    public IActionResult ViewUser(long id)
    {
        if (_userService.GetById(id) is not { } user)
            return NotFound();

        var model = UserViewModel.FromUser(user);
        model.Logs = _userLogService.GetForUser(id);
        return View(model);

    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View(new UserListItemViewModel());
    }

    [HttpPost("add")]
    [ValidateAntiForgeryToken]
    public IActionResult AddPost([FromForm] UserListItemViewModel model)
    {
        if (ModelState.IsValid is false)
            return BadRequest();

        var user = model.ToUser();

        _userService.Create(user);

        return RedirectToViewUser(user.Id);
    }

    [HttpGet("{id:long}/edit")]
    public IActionResult Edit(long id)
    {
        if (_userService.GetById(id) is not { } user)
            return NotFound();

        return View(UserListItemViewModel.FromUser(user));
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public IActionResult EditPost([FromForm] UserListItemViewModel model)
    {
        if (_userService.Exists(model.Id) is false)
            return NotFound();

        if (ModelState.IsValid is false)
            return BadRequest();

        var user = model.ToUser();

        _userService.Edit(user);

        return RedirectToViewUser(model.Id);
    }

    [HttpGet("{id:long}/delete")]
    public IActionResult Delete(long id)
    {
        if (_userService.GetById(id) is not { } user)
            return NotFound();

        return View(UserListItemViewModel.FromUser(user));
    }

    [HttpPost("{id:long}/delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(long id)
    {
        if (_userService.GetById(id) is not { } user)
            return NotFound();

        _userService.Delete(user);
        return RedirectToAction("List");
    }

    private RedirectToActionResult RedirectToViewUser(long id) => RedirectToAction("ViewUser", new { id });
}
