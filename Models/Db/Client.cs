namespace VeterinarySystem.Models.Db
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastMame { get; set; } = null!;

        public string City { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
