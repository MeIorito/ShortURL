namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult GetUser(string id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateUser()
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteUser(string id)
    {
        return Ok();
    }
}