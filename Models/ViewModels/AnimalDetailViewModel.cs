﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public IEnumerable<Appointment> AllAppointments { get; set; } = null!;
        [ValidateNever]
        public IEnumerable<SelectListItem> AllTypeOfVaccines { get; set; } = null!;
        [ValidateNever]
        public Dictionary<string, List<Vaccination>> AllVaccinations { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> AllMedicines { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> AllSurgeries { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> AllVets { get; set; } = null!;

        [ValidateNever]
        public Appointment Appointment { get; set; } = null!;

        [ValidateNever]
        public string ActiveTab { get; set; } = null!;
    }
}
