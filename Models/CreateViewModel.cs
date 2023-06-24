using DogVetSystem_Razor.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models
{
    public class CreateViewModel
    {
        public AnimalPOST Animal { get; set; }
        public List<SelectListItem> Clients { get; set; }
        public List<SelectListItem> Breeds { get; set; }
    }
}
