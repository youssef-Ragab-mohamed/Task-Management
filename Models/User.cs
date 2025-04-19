using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagement.Models
{
    public class User : IdentityUser<int>
    {
 
        // Navigation properties
        [JsonIgnore]
        public virtual List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
  
        public virtual List<TaskHistory> TaskHistory { get; set; } = new List<TaskHistory>();
    }
 
}
