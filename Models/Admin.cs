using System.ComponentModel.DataAnnotations;

namespace ContinentalBackend.Models
{
    public class Admin : Utilizador
    {
        [Key]
        public long IdAdmin { get; set; }

        [Required]
        public string Nome { get; set; }


    }

}
