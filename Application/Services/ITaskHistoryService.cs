using TaskManagement.Models;

namespace TaskManagement.Application.Services
{
    public interface ITaskHistoryService
    {
     Task LogAsync(TaskAction action, int taskId, int userId, TaskState? oldStatus = null, TaskState? newStatus = null);
   
    Task<List<TaskHistory>> GetAllTaskHistoriesAsync();
        Task<TaskHistory> GetTaskHistoryByIdAsync(int id);
        Task<List<TaskHistory>> GetTaskHistoryByTaskIdAsync(int taskId);
        Task<List<TaskHistory>> GetTaskHistoryByUserIdAsync(int userId);
        Task<List<TaskHistory>> GetTaskHistoryInRangeAsync(DateTime startDate, DateTime endDate);
        Task AddTaskHistoryAsync(TaskHistory history);
    }
}
