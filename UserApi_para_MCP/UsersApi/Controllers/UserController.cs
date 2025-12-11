using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;

namespace UsersApi.Controllers;

public class UserController(
        IUserServices userServices
    ) : ControllerBase
{
    private readonly IUserServices _userServices = userServices;

    [HttpGet("getUsers")]
    public async Task<IActionResult> GetByProperty([FromQuery] string id)
    {
        try
        {
            var users = await _userServices.GetUsers(id);
            return Ok(users);
        }
        catch 
        {
            return BadRequest();
        }        
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserModel user)
    {
        try
        {
            await _userServices.AddUser(user);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }        
    }

    [HttpPut("updateUser")]   
    public async Task<IActionResult> Edit([FromBody] UserModel user)
    {
        try
        {
            await _userServices.UpdateUser(user);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("deleteUser")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        try
        {
            await _userServices.DeleteUser(id);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
