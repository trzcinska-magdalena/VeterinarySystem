using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Service
{
    public interface ISystemService
    {
        void SetTempData(ITempDataDictionary tempData, string tempType, string tempDataValue);
        void SetLogError(Exception ex);
        void SetLogInfo(string information);
        Task<BaseManagementViewModel> ConstructBaseManagementVMAsync();
        Task SaveBreedInTheBaseAsync(Breed breed);
        Task UpdateBreedInTheBaseAsync(Breed breed);
        Task<Breed?> GetBreedOrNullAsync(Expression<Func<Breed, bool>> filter);
        Task<bool> BreedExistsAsync(Expression<Func<Breed, bool>> filter);
    }

    public class SystemService: ISystemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SystemService(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }

        public void SetTempData(ITempDataDictionary tempData, string tempType, string tempDataValue)
        {
            tempData[tempType] = tempDataValue;
        }

        public void SetLogError(Exception ex)
        {
            //TODO
        }

        public void SetLogInfo(string information)
        {
            // TODO
        }


        // Base Management
        public async Task<BaseManagementViewModel> ConstructBaseManagementVMAsync()
        {
            var baseManagementViewModel = new BaseManagementViewModel
            {
                AllBreeds = await _unitOfWork.Breeds.GetAllAsync(tracking: false)
            };
            return baseManagementViewModel;
        }

        public async Task SaveBreedInTheBaseAsync(Breed breed)
        {
            _unitOfWork.Breeds.Add(breed);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateBreedInTheBaseAsync(Breed breed)
        {
            _unitOfWork.Breeds.Update(breed);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Breed?> GetBreedOrNullAsync(Expression<Func<Breed, bool>> filter)
        {
            return await _unitOfWork.Breeds.GetAsync(filter);
        }

        public async Task<bool> BreedExistsAsync(Expression<Func<Breed, bool>> filter)
        {
            return await _unitOfWork.Breeds.BreedExistsAsync(filter);
        }

    }
}
