using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models.ViewModels
{
    public class AnimalCreateViewModel
    {
        public Animal Animal { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> AllClients { get; set; } = new List<SelectListItem>();
        [ValidateNever]
        public IEnumerable<SelectListItem> AllBreeds { get; set; } = new List<SelectListItem>();
    }
}
