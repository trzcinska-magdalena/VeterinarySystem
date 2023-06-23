namespace VeterinarySystem.Models.Db
{
    public class TypeOfVaccine
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

    }
}
