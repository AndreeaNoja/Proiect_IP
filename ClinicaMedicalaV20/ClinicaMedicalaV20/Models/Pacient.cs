namespace ClinicaMedicalaV20.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Pacient
    {
        [Key]
        public int Id { get; set; }
        public int IDMedicApartinator { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "CNP must be 13 characters long.")]
        public string CNP { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nume { get; set; }

        [Required]
        [MaxLength(30)]
        public string Prenume { get; set; }

        [Required]
        public int Varsta { get; set; }

        [Required]
        [MaxLength(30)]
        public string Judet { get; set; }

        [Required]
        [MaxLength(30)]
        public string Oras { get; set; }

        [Required]
        [MaxLength(30)]
        public string Strada { get; set; }

        [Required]
        [MaxLength(5)]
        public string Numar { get; set; }

        [MaxLength(3)]
        public string Bloc { get; set; }

        [MaxLength(3)]
        public string Apartament { get; set; }

        [MaxLength(15)]
        public string NumarTel { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(40)]
        public string Profesie { get; set; }

        [MaxLength(40)]
        public string LocMunca { get; set; }
    }

}
