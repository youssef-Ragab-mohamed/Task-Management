using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        public TaskService(IUnitOfWork unitOfWork,ITaskHistoryService taskHistoryService)
        {
            UnitOfWork = unitOfWork;
            TaskHistoryService = taskHistoryService;
        }

        public IUnitOfWork UnitOfWork { get; }
        public ITaskHistoryService TaskHistoryService { get; }

        public async Task AddTaskAsync(TaskDto taskDto, IWebHostEnvironment env)
        {
            var task = new TaskEntity
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                DueDate = taskDto.DueDate,
                CreatedAt = DateTime.Now
            };

            // Check if a file was uploaded
            if (taskDto.File != null)
            {
                if (env.WebRootPath == null)
                {
                    throw new InvalidOperationException("WebRootPath is null. Make sure web root path is set properly.");
                }
                string uploadsFolder = Path.Combine(env.WebRootPath, "uploads");

                // Ensure directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, taskDto.File.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await taskDto.File.CopyToAsync(stream);
                }

                // Store the relative file path in the database
                task.FilePath = Path.Combine("uploads", taskDto.File.FileName);
            }

          await  UnitOfWork.Tasks.AddAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await UnitOfWork.Tasks.DeleteAsync(id);
        }

        public async Task<List<TaskDto>> GetAllTasksAsync()
        {
            List<TaskEntity> taskEntity = await UnitOfWork.Tasks.GetAllAsync(); //(t => t.User //,  t => t.Attachments);
         

            var taskDtoList= new List<TaskDto>();
            foreach (var task in taskEntity)
            {
                var taskDto=new TaskDto { Id = task.Id ,Title=task.Title,Description=task.Description, Status=task.Status, Priority=task.Priority,DueDate=task.DueDate};
                taskDtoList.Add(taskDto);
            }
            return taskDtoList;


        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
  
        var task= await UnitOfWork.Tasks.GetByIdAsync(id);
    
         var taskDto = new TaskDto { Id = task.Id, Title = task.Title, Description = task.Description, Status = task.Status,  Priority = task.Priority, DueDate = task.DueDate };
             
          
            return taskDto;
        }

        public async Task<List<TaskDto>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await UnitOfWork.Tasks.GetAllAsync(t => t.UserId == userId);
            var tasksDto = new List<TaskDto>();

            foreach (var task in tasks)
            {

                var taskDto = new TaskDto { Id = task.Id, Title = task.Title, Description = task.Description, Status = task.Status , Priority = task.Priority, DueDate = task.DueDate };
                tasksDto.Add(taskDto);
            }
            return tasksDto;
        }

        public async Task UpdateTaskAsync(int id,int userId,TaskDto taskDto)
        {
           
            
            var task = await UnitOfWork.Tasks.GetByIdAsync(id );
            if (task == null)
                throw new Exception("Task not found");
            var oldStatus =task.Status;


            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.Status = taskDto.Status;
            task.Priority = taskDto.Priority;
            task.DueDate = taskDto.DueDate;

            await UnitOfWork.Tasks.UpdateAsync(task);
            await  TaskHistoryService.LogAsync(TaskAction.Update, id, userId, oldStatus,  taskDto.Status);
        }

    
    }
}
