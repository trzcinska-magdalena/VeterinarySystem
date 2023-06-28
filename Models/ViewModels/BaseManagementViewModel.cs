using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models.ViewModels
{
    public class BaseManagementViewModel
    { 
        [ValidateNever]
        public IEnumerable<Account> Accounts { get; set; }
        public Account NewAccount { get; set; }

        [ValidateNever]
        public IEnumerable<Vet> Vets { get; set; }
        public Vet NewVet { get; set; }

        [ValidateNever]
        public IEnumerable<Breed> Breeds { get; set; }
        public Breed NewBreed { get; set; }

        [ValidateNever]
        public IEnumerable<Medicine> Medicines { get; set; }
        public Medicine NewMedicine { get; set; }

        [ValidateNever]
        public IEnumerable<Specialisation> Specialisations { get; set; }
        public Specialisation NewSpecialisation { get; set; }

        [ValidateNever]
        public IEnumerable<Surgery> Surgerys { get; set; }
        public Surgery NewSurgery { get; set; }

        [ValidateNever]
        public IEnumerable<TypeOfVaccine> TypeOfVaccines { get; set; }
        public TypeOfVaccine NewTypeOfVaccine { get; set; }


    }
}
