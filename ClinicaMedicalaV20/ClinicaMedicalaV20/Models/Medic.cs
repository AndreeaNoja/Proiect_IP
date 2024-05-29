using System.ComponentModel.DataAnnotations;

namespace ClinicaMedicalaV20.Models
{
    public class Medic
    {
        [Key]
        public int Id { get; set; } // Aceasta este cheia primară pentru clasa Medic
        public int ID_Medic { get; set; } // Presupunem că acesta este un identificator unic pentru fiecare medic.

        [Required]
        [MaxLength(20)]
        public string Nume { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prenume { get; set; }

        [Required]
        [MaxLength(30)]
        public string Parola { get; set; } // Sper că parola este stocată într-o formă criptată în baza de date reală!
    }
}
