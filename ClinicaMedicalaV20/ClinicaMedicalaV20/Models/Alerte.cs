using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicaMedicalaV20.Models
{
    public class Alerte
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "ID Pacient must be 13 characters.")]
        public string IDPacient { get; set; } // Acesta nu este cheie primară.
        [Required]
        public string Alarma { get; set; }
        [Required]
        public string Avertisment { get; set; }
        [Required]
        public int FrecventaCardiacaMax { get; set; }

        [Required]
        public float UmiditateMax { get; set; }
        [Required]
        public int TemperaturaMax { get; set; }

        [Required]
        public float ECKMax { get; set; }
    }
}
