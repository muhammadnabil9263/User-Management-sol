using System.ComponentModel.DataAnnotations;

namespace User_Management.Models.DTO
{
    public class UserEditRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public int OrgnizationId { get; set; }
    
    }
}
