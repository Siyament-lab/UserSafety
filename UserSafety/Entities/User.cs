using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSafetyAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(256)]
        [Column(TypeName = "nvarchar(256)")]
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
