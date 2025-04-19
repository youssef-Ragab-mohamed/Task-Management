using TaskManagement.Application.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Application.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByNameAsync(string name);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(int id ,UserDto userDto);
        Task DeleteUserAsync(int id);
    }
}
