using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models.Models
{
    public class AnimalCreateViewModel
    {
        public Animal Animal { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> Clients { get; set; } = null!;
        [ValidateNever]
        public IEnumerable<SelectListItem> Breeds { get; set; } = null!;
    }
}
