using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContinentalBackend.Models
{
    public class Utilizador
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText]
        [Required]
        public bool Password { get; set; } = true;
    }
}
