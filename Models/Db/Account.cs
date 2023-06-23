namespace VeterinarySystem.Models.Db
{
    public class Account
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int Admin { get; set; }

        public int Id { get; set; }

        public virtual ICollection<Vet> Vets { get; set; } = new List<Vet>();
    }
}
