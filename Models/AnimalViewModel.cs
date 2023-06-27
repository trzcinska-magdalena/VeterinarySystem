using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models
{
    public class AnimalViewModel
    {
        public IEnumerable<Animal> Animals = new List<Animal>();
        public string SearchString { get; set; }
    }
}
