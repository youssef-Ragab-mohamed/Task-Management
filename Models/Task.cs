using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
        
        public string? FilePath { get; set; }

        [Required]
        public TaskState Status { get; set; }

        [Required]
        public TaskPriority Priority { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //[Required]
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }

        public virtual User User { get; set; }

     //   public virtual List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public virtual List<TaskHistory> TaskHistory { get; set; } = new List<TaskHistory>();
    }

    public enum TaskState
    {
        ToDo,
        InProgress,
        Done
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }
}
