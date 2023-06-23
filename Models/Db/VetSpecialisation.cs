namespace VeterinarySystem.Models.Db
{
    public class VetSpecialisation
    {
        public int SpecialisationId { get; set; }

        public int VetId { get; set; }

        public DateTime DateFrom { get; set; }

        public virtual Specialisation Specialisation { get; set; } = null!;

        public virtual Vet Vet { get; set; } = null!;
    }
}