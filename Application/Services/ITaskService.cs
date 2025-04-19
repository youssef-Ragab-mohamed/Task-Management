using TaskManagement.Application.Dtos;
using TaskManagement.Models;

namespace TaskManagement.Application.Services
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(int id);
        Task<List<TaskDto>> GetTasksByUserIdAsync(int userId);
        Task AddTaskAsync(TaskDto taskDto, IWebHostEnvironment env);
        Task UpdateTaskAsync(int id, int userId, TaskDto taskDto);
        Task DeleteTaskAsync(int id);
    }
}
