using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeterinarySystem.Models.Db
{
    public class Animal
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public int BreedId { get; set; }

        public int ClientId { get; set; }

        public string Gender { get; set; } = null!;

        [ValidateNever]
        public virtual Breed Breed { get; set; } = null!;
        [ValidateNever]
        public virtual Client Client { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

        public virtual ICollection<Weight> Weights { get; set; } = new List<Weight>();
    }
}
