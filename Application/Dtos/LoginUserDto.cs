using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.Dtos
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
