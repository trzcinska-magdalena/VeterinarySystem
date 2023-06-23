namespace VeterinarySystem.Models.Db
{
    public class Specialisation
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<VetSpecialisation> VetSpecialisations { get; set; } = new List<VetSpecialisation>();

    }
}
