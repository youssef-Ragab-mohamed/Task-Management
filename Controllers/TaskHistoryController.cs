using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Services;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskHistoryController : ControllerBase
    {
        public TaskHistoryController( ITaskHistoryService taskHistoryService ,IMapper mapper)
        {
            TaskHistoryService = taskHistoryService;
            Mapper = mapper;
        }

        public ITaskHistoryService TaskHistoryService { get;  }
        public IMapper Mapper { get; }

        [HttpGet]
        public async Task< IActionResult> GetAll()
        {
           List <TaskHistory> taskHistoreis = await TaskHistoryService.GetAllTaskHistoriesAsync();
            List<TaskHistoryDto> taskHistoriesDtoList = new();

            if (taskHistoreis != null)
            {
                foreach (var taskHistory in taskHistoreis)
                {              
                    TaskHistoryDto taskHistoryDto =Mapper.Map<TaskHistoryDto>(taskHistory);
                    taskHistoriesDtoList.Add(taskHistoryDto);
                }
            }
 
            return Ok(taskHistoriesDtoList);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskHistory( int id)
        {
            var taskHistory= await TaskHistoryService.GetTaskHistoryByIdAsync(id);

            if (taskHistory != null)
            {
                TaskHistoryDto taskHistoryDto = Mapper.Map<TaskHistoryDto>(taskHistory);
                return Ok(taskHistoryDto);
            }
            return NotFound($" no task exist by id : {id}");

        }   
        [HttpGet("task/{Taskid}")]
        public async Task<IActionResult> GetTaskHistoryByTaskId( [FromRoute] int Taskid)
        {
            var taskHistory= await TaskHistoryService.GetTaskHistoryByTaskIdAsync(Taskid);

            if (taskHistory != null)
            {
                var taskHistoryDtos = Mapper.Map<List<TaskHistoryDto>>(taskHistory);
                return Ok(taskHistoryDtos);
            }
            return NotFound($" already no task by id : {Taskid}");

        }



    }




}
