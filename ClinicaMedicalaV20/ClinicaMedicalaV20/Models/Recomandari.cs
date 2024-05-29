using System.ComponentModel.DataAnnotations;

namespace ClinicaMedicalaV20.Models
{
    public class Recomandari
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "ID Pacient must be 13 characters.")]
        public string IDPacient { get; set; } // Acesta nu este cheie primară.

        [Required]
        public string TipulRecomandarii { get; set; }

        [Required]
        public DateTime Data_Inceput { get; set; }

        [Required]
        public DateTime Data_Final { get; set; }

        public string AlteIndicatii { get; set; }
    }
}
