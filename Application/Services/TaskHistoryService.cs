using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Application.Services
{
    public class TaskHistoryService : ITaskHistoryService
    {
        public TaskHistoryService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        //very important method

        public async Task<List<TaskHistory>> GetTaskHistoryByTaskIdAsync(int taskId)
        {
      
            var taskHistoriesList = await UnitOfWork.TaskHistories.GetAllAsync(th => th.Task,th => th.User); 

                taskHistoriesList = taskHistoriesList
                .Where(th => th.TaskId == taskId)
                .ToList();
  
            if (taskHistoriesList == null)
                return new List<TaskHistory>();
            return taskHistoriesList;

        }



        public async Task AddTaskHistoryAsync(TaskHistory history)
        {
            await UnitOfWork.TaskHistories.AddAsync(history);
        }

        public async Task<List<TaskHistory>> GetAllTaskHistoriesAsync()
        {
          return   await UnitOfWork.TaskHistories.GetAllAsync(th => th.Task, th => th.User);
                                                             
        }

        public async Task<TaskHistory> GetTaskHistoryByIdAsync(int id)
        {
            return await UnitOfWork.TaskHistories.GetByIdAsync(id);
        }



        public async Task<List<TaskHistory>> GetTaskHistoryByUserIdAsync(int userId)
        {
            return await UnitOfWork.TaskHistories.GetAllAsync(th => th.UserId == userId , th => th.User);
        }

        public Task<List<TaskHistory>> GetTaskHistoryInRangeAsync(DateTime startDate, DateTime endDate)
        {
            return UnitOfWork.TaskHistories.GetAllAsync(th => th.Task.CreatedAt >= startDate && th.Task.CreatedAt <= endDate);
        }

        public async Task LogAsync(TaskAction action, int taskId, int userId, TaskState? oldStatus = null, TaskState? newStatus = null)
        {
            var history = new TaskHistory
            {
                Action = action,
                TaskId = taskId,
                UserId = userId,
                ChangedAt = DateTime.UtcNow,
                OldStatus =  oldStatus,    
                NewStatus = newStatus
            }; 

            await UnitOfWork.TaskHistories.AddAsync(history); //TaskHistories.Add(history);
            await UnitOfWork.CompleteAsync();
        }


    }
}

