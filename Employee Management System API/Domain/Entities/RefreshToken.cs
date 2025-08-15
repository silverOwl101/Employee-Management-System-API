using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.Domain.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }

        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; } = default!;
    }
}
