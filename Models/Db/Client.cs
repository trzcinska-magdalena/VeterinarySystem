using System.ComponentModel.DataAnnotations;

namespace VeterinarySystem.Models.Db
{
    public class Client
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string FirstName { get; set; } = null!;
        [MinLength(3)]
        public string LastMame { get; set; } = null!;
        [MinLength(3)]
        public string City { get; set; } = null!;
        [StringLength(11)]
        public string PhoneNumber { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }

        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
