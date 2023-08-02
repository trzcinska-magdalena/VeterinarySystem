using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models.ViewModels
{
    public class AnimalsViewModel
    {

        [ValidateNever]
        public IEnumerable<Animal> AllAnimals { get; set; } = new List<Animal>();
        public string? SearchString { get; set; }
    }
}
