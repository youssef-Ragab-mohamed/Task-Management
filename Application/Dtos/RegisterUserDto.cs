using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        public string name { get; set; }
        [Required]

        public string password { get; set; }
        [Required]
        [Compare("password")]

        public string confirmPassword { get; set; }
        [Required]
        public string  email { get; set; }


    }
}
