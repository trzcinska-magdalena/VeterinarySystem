using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models.ViewModels
{
    public class BaseManagementViewModel
    {
        [ValidateNever]
        public IEnumerable<Breed> AllBreeds { get; set; } = new List<Breed>();
        public Breed NewBreed { get; set; } = new Breed();
    }
}
