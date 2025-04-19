using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        public IUserService UserService { get; }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await UserService.GetAllUsersAsync();
            if (users == null) return BadRequest("empty");
            return Ok(users);

        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
         
            User user = new User { UserName = userDto.Name, Email = userDto.Email, PasswordHash = userDto.Password };
            await UserService.AddUserAsync(user);
            return Ok(user);
        }
        [HttpPut( "{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, UserDto userDto)
        {
           // var user = await UserService.GetUserByIdAsync(id);
            if (userDto == null) return BadRequest("user not found to update it !");

            await UserService.UpdateUserAsync(id,userDto);
            return Ok (await UserService.GetUserByIdAsync(id));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)

        {
            var user=await UserService.GetUserByIdAsync(id);
            if (user == null) return BadRequest("user already not found!");
            await UserService.DeleteUserAsync(id);
            return Ok(user);

        }


    }

}