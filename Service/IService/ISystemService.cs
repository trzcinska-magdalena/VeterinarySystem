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
        Task<AnimalDetailViewModel> ConstructAnimalDetailVMAsync(Animal animal, string activeTab);
        Task<bool> AddNewAnimalAsync(Animal animal);
        Task<Animal?> GetAnimalWithDetails(int? id);
        bool IsValidateWeightWithDate(Weight newWeight);
        Task<bool> SetWeightToAnimal(Weight weight, Animal animal);
        Task<Weight> GetWeight(int id, DateTime date);
        bool IsValidateVaccination(Vaccination vaccination);
    }
}
