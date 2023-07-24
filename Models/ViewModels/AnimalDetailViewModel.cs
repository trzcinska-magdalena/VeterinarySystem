using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models.ViewModels
{
    public class AnimalDetailViewModel
    {
        [ValidateNever]
        public Animal Animal { get; set; } = null!;
        public Weight? NewWeight { get; set; }
        public Vaccination? NewVaccination { get; set; }
        public Appointment? NewAppointment { get; set; }

        [ValidateNever]
        public IEnumerable<Appointment> Appointments { get; set; } = null!;
        [ValidateNever]
        public IEnumerable<SelectListItem> TypeOfVaccines { get; set; } = null!;
        [ValidateNever]
        public Dictionary<string, List<Vaccination>> Vaccinations { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> Medicines { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> Surgeries { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> Vets { get; set; } = null!;

        [ValidateNever]
        public Appointment Appointment { get; set; } = null!;
    }
}
