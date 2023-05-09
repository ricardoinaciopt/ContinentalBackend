using System.ComponentModel.DataAnnotations;

namespace ContinentalBackend.Models
{
    public class Linha
    {
        [Key]
        public long Id { get; set; }

        public string Descricao { get; set; }

        [Required]
        public bool Estado { get; set; } = true;
    }
}
