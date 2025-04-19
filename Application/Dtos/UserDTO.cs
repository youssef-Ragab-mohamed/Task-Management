using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs
{
    public class UserDto
    {
        public int? Id { get; set; }  

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Name must be at most 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }  
        


        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(Admin|User)$", ErrorMessage = "Role must be Admin or User.")]
        public string Role { get; set; }
    }
}
