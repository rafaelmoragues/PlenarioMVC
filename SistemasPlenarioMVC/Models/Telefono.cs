using System.ComponentModel.DataAnnotations;

namespace SistemasPlenarioMVC.Models
{
    public class Telefono
    {
        [Key]
        public int TelefonoId { get; set; }
        [Required]
        [Display(Name = "Numero de teléfono")]
        public string TelefonoNumero { get; set; }
        public int PersonaId { get; set; }
        public Persona? Persona { get; set; }
    }
}
