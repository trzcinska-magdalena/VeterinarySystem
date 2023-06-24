using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models
{
    public class AnimalDetailModel
    {
        public Animal? Animal { get; set; }
        public Weight NewWeight { get; set; }
        public Vaccination NewVaccitation { get; set; }
        public List<Weight> Weights { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<SelectListItem> TypeOfVaccines { get; set; }
        public Dictionary<String, List<Vaccination>> Vaccinations { get; set; }
    }
}
