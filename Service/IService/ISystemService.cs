using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;

namespace VeterinarySystem.Service.IService
{
    public interface ISystemService
    {
        void SetLogError(Exception ex);
        void SetLogInfo(string information);

        // Base Management Controller
        Task<BaseManagementViewModel> ConstructBaseManagementVMAsync();
        Task<bool> AddNewBreedAsync(Breed breed);
        Task<(bool, string)> UpdateBreedAsync(Breed breed);

        // Animal Controller
        Task<AnimalsViewModel> ConstructAnimalsVWAsync(string? searchString = null);
        Task<AnimalCreateViewModel> ConstructAnimalCreateVMAsync();
        Task<AnimalDetailViewModel> ConstructAnimalDetailVMAsync();
        void SetAnimalDetailViewModel();
        bool IsValidateWeightWithDate(Weight newWeight);
        bool IsValidateVaccination(Vaccination vaccination);
    }
}
