using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Services;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public TasksController(ITaskService taskService, IWebHostEnvironment env)
        {
            TaskService = taskService;
            Env = env;
        }

        public ITaskService TaskService { get; }
        public IWebHostEnvironment Env { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {

            return Ok(await TaskService.GetAllTasksAsync());

        }
        [HttpPost]
        public async Task<IActionResult> AddTask([FromForm] TaskDto taskDto)
        {
            if (taskDto == null) { return BadRequest(); }
            await TaskService.AddTaskAsync(taskDto, Env);

            return Ok(new { message = "Task created successfully!" });


        }
        [HttpPut("{id}")]
        public  async Task< IActionResult> UpdateTask( [FromRoute]int id ,[FromBody] TaskDto taskDto)
        {
            if (taskDto == null) return BadRequest();
            //2 =>is a number for testing 
            await TaskService.UpdateTaskAsync( id ,2,taskDto);
            return Ok(await TaskService.GetTaskByIdAsync(id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {

            await TaskService.DeleteTaskAsync(id);
            return Ok(new { message = "Task deleted successfully!" });

        }


    }
}
