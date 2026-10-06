using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Security.Dtos;
using Security.Services;

namespace Security.Controller;

public class LoginController : ControllerBase
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService =  userService;
    }
    
    [HttpPost("/login")]
    public IActionResult Login([FromBody] TryLogin tryLogin)
    {
        return Ok(_userService.TryLogin(tryLogin));
    }
    
    [HttpPost("/register")]
    public void Create([FromBody] CreateUserRequest createUserRequest)
    {
        _userService.TryRegister(createUserRequest);
       
        
    }
}