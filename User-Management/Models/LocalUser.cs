using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace User_Management.Models
{
    public class LocalUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        [ForeignKey("Orgnization")]
        public int OrgnizationId { get; set; }

        [JsonIgnore]
        public virtual Orgnization? orgnization { get; set; }
    }

}
