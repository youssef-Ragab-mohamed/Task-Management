using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.Dtos
{
    public class AttachmentDto
    {
        public int? Id { get; set; }

 
        public IFormFile? File { get; set; } 
 
    }
}
