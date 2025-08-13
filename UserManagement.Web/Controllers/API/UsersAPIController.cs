using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[ApiController]
[Route("api/users")]
public class UsersApiController : Controller
{
    private readonly IUserService _userService;

    public UsersApiController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> List(bool? isActive = null)
    {
        var users = await (isActive is null ? _userService.GetAllAsync() : _userService.FilterByActiveAsync(isActive.Value));

        return users;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<User>> ViewUser(long id)
    {
        if (await _userService.GetByIdAsync(id) is not { } user)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromForm] UserListItemViewModel model)
    {
        if (ModelState.IsValid is false)
            return BadRequest();

        var user = model.ToUser();

        await _userService.CreateAsync(user);

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] UserListItemViewModel model)
    {
        if (await _userService.ExistsAsync(model.Id) is false)
            return NotFound();

        if (ModelState.IsValid is false)
            return BadRequest();

        var user = model.ToUser();

        await _userService.EditAsync(user);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (await _userService.GetByIdAsync(id) is not { } user)
            return NotFound();

        await _userService.DeleteAsync(user);
        return NoContent();
    }

    private RedirectToActionResult RedirectToViewUser(long id) => RedirectToAction("ViewUser", new { id });
}
