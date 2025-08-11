using System.Linq;
using System.Threading.Tasks;
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
        _userService    = userService;
        _userLogService = userLogService;
    }

    [HttpGet]
    public async Task<ViewResult> List(bool? isActive = null)
    {
        var users = await (isActive is null ? _userService.GetAllAsync() : _userService.FilterByActiveAsync(isActive.Value));

        var items = users.Select(UserListItemViewModel.FromUser);

        var model = new UserListViewModel { Items = items.ToList() };

        return View(model);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> ViewUser(long id)
    {
        if (await _userService.GetByIdAsync(id) is not { } user)
            return NotFound();

        var model = UserViewModel.FromUser(user);
        model.Logs = await _userLogService.GetForUserAsync(id);
        return View(model);
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View(new UserListItemViewModel());
    }

    [HttpPost("add")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddPost([FromForm] UserListItemViewModel model)
    {
        if (ModelState.IsValid is false)
            return BadRequest();

        var user = model.ToUser();

        await _userService.CreateAsync(user);

        return RedirectToViewUser(user.Id);
    }

    [HttpGet("{id:long}/edit")]
    public async Task<IActionResult> Edit(long id)
    {
        if (await _userService.GetByIdAsync(id) is not { } user)
            return NotFound();

        return View(UserListItemViewModel.FromUser(user));
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost([FromForm] UserListItemViewModel model)
    {
        if (await _userService.ExistsAsync(model.Id) is false)
            return NotFound();

        if (ModelState.IsValid is false)
            return BadRequest();

        var user = model.ToUser();

        await _userService.EditAsync(user);

        return RedirectToViewUser(model.Id);
    }

    [HttpGet("{id:long}/delete")]
    public async Task<IActionResult> Delete(long id)
    {
        if (await _userService.GetByIdAsync(id) is not { } user)
            return NotFound();

        return View(UserListItemViewModel.FromUser(user));
    }

    [HttpPost("{id:long}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(long id)
    {
        if (await _userService.GetByIdAsync(id) is not { } user)
            return NotFound();

        await _userService.DeleteAsync(user);
        return RedirectToAction("List");
    }

    private RedirectToActionResult RedirectToViewUser(long id) => RedirectToAction("ViewUser", new { id });
}
