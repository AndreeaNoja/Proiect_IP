using System.ComponentModel.DataAnnotations;

namespace ClinicaMedicalaV20.Models
{
    public class Date_medicale
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(13, ErrorMessage = "ID Pacient must be 13 characters.")]
        public string IDPacient { get; set; }
        public string IstoricMedical { get; set; }
        public string Alergii { get; set; }
        public string ConsultatiiCardiologice { get; set; }
    }
}
