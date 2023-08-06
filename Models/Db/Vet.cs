using System.ComponentModel.DataAnnotations;

namespace VeterinarySystem.Models.Db
{
    public class Vet
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string FirstName { get; set; } = null!;
        [MinLength(5)]
        public string LastName { get; set; } = null!;

        public byte[]? Photo { get; set; }

        public string? ApplicationUserId { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<VetSpecialisation> VetSpecialisations { get; set; } = new List<VetSpecialisation>();

        public virtual ICollection<AppointmentVet> AppointmentVets { get; set; } = new List<AppointmentVet>();
    }
}