using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models
{
    public class AnimalViewModel
    {
        public List<Animal> Animals = new List<Animal>();
        public string SearchString { get; set; }
    }
}
