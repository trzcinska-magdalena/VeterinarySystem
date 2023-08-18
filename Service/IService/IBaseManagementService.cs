using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;

namespace VeterinarySystem.Service.IService
{
    public interface IBaseManagementService
    {
        Task<BaseManagementViewModel> ConstructBaseManagementVMAsync();
        Task<bool> AddNewBreedAsync(Breed breed);
        Task<(bool, string)> UpdateBreedAsync(Breed breed);
    }
}
