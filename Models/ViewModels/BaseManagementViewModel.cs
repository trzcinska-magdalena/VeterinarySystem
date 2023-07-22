using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models.ViewModels
{
    public class BaseManagementViewModel
    { 
        [ValidateNever]
        public IEnumerable<Vet> Vets { get; set; } = new List<Vet>();
        public Vet NewVet { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<Breed> Breeds { get; set; } = new List<Breed>();
        public Breed NewBreed { get; set; } = null!;
    }
}
