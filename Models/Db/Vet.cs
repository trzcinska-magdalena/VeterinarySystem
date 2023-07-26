namespace VeterinarySystem.Models.Db
{
    public class Vet
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public byte[] Photo { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<VetSpecialisation> VetSpecialisations { get; set; } = new List<VetSpecialisation>();

        public virtual ICollection<AppointmentVet> AppointmentVets { get; set; } = new List<AppointmentVet>();
    }
}