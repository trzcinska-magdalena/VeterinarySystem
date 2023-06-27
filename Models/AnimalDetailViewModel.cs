using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models
{
    public class AnimalDetailViewModel
    {
        [ValidateNever]
        public Animal? Animal { get; set; } = null!;
        public Weight NewWeight { get; set; } = null!;
        public Vaccination NewVaccination { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<Appointment> Appointments { get; set; } = null!;
        [ValidateNever]
        public IEnumerable<SelectListItem> TypeOfVaccines { get; set; } = null!;
        [ValidateNever]
        public Dictionary<string, List<Vaccination>> Vaccinations { get; set; } = null!;
    }
}
