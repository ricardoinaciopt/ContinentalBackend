using System.ComponentModel.DataAnnotations;

namespace ContinentalBackend.Models
{
    public class Alerta
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string FuncionarioId { get; set; }

        [Required]
        public long LinhaId { get; set; }

        [Required]
        [RegularExpression("Avaria|Material", ErrorMessage = "Tipo Invalido")]
        public string Tipo { get; set; }

        [Required]
        [RegularExpression("1|2|3", ErrorMessage = "Valor Invalido")]
        public string Prioridade { get; set; }

        [Required]
        public bool Estado { get; set; } = true;

        [Required]
        public DateTime Criacao { get; set; } = DateTime.Now;
    }
}


