using System.ComponentModel.DataAnnotations;

namespace ContinentalBackend.Models
{
    public class Supervisor : Utilizador
    {
        [Key]
        public long IdSupervisor { get; set; }

        [Required]
        public string Nome { get; set; }


        public string? Role { get; set; }

        public ICollection<Linha>? Linhas { get; set; }
    }
}

