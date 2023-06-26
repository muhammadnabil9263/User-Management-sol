using System.ComponentModel.DataAnnotations;

namespace User_Management.Models.DTO
{
    public class UserRegistrationRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name{get; set;}
            [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters, dude!")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public int OrgnizationId { get; set;}

    }
}
