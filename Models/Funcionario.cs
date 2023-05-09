using System.ComponentModel.DataAnnotations;

namespace ContinentalBackend.Models
{
    public class Funcionario : Utilizador
    {
        [Key]
        public long IdFuncionario { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public long LinhaId { get; set; }

        public Linha? Linha { get; set; }
    }
}
