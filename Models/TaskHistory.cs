using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class TaskHistory
    {
        [Key]
        public int Id { get; set; }
    

        [Required]
        public TaskAction Action { get; set; }

     
        public TaskState? OldStatus { get; set; }

      
        public TaskState? NewStatus { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; } = DateTime.Now;  

        [Required]
        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual TaskEntity Task { get; set; }  

    
        public int  UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User  User { get; set; }  
    }

    public enum TaskAction
    {
        Add,
        Update,
        Delete
    }
}
