namespace VeterinarySystem.Models.Db
{
    public class Breed
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
