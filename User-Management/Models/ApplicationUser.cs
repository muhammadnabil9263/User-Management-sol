using Microsoft.AspNetCore.Identity;

namespace User_Management.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        //public Orgnization orgnization { get; set; }

    }
}
