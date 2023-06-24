using System.ComponentModel.DataAnnotations;

namespace DogVetSystem_Razor.Models.DTOs
{
    public class AnimalPOST
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Weight")]
        public double Weight { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; } = null!;
        [Required]
        [Display(Name = "Breed")]
        public int BreedId { get; set; }
        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
    }
}
