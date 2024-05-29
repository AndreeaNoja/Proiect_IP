using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMedicalaV20.Models
{
    public class Date_senzori
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(13, ErrorMessage = "ID Pacient must be 13 characters.")]
        public string IDPacient { get; set; } // Acesta nu este cheie primară.

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        
        public float FrecventaCardiaca { get; set; }

        [Required]
       
        public float Umiditate { get; set; }

        [Required]
      
        public  float TemperaturaMax { get; set; }

        [Required]
        
        public float ECG_pWave { get; set; }
        [Required]
        
        public float ECG_qrsWave { get; set; }
        [Required]
     
        public float ECG_tWave { get; set; }

    }
}
