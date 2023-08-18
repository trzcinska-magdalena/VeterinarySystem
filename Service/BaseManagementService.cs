using Serilog;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Service
{
    public class BaseManagementService : IBaseManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;

        public BaseManagementService(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<BaseManagementViewModel> ConstructBaseManagementVMAsync()
        {
            _logger.SetLogInfo("Starting ConstructBaseManagementVMAsync method.");

            var baseManagementViewModel = new BaseManagementViewModel
            {
                AllBreeds = await _unitOfWork.Breeds.GetAllAsync(tracking: false)
            };
            return baseManagementViewModel;
        }

        public async Task<bool> AddNewBreedAsync(Breed breed)
        {
            _logger.SetLogInfo("Starting AddNewBreedAsync method.");

            if (await _unitOfWork.Breeds.IsExistsAsync(e => e.Name == breed.Name))
            {
                _logger.SetLogWarning($"The breed named {breed.Name} already exists. The breed not added.");
                return false;
            }

            _unitOfWork.Breeds.Add(breed);
            await _unitOfWork.SaveAsync();

            _logger.SetLogInfo($"The breed named {breed.Name} has been successfully added.");
            return true;
        }

        public async Task<(bool, string)> UpdateBreedAsync(Breed breed)
        {
            _logger.SetLogInfo("Starting UpdateBreedAsync method.");

            if (await _unitOfWork.Breeds.IsExistsAsync(e => e.Name == breed.Name))
            {
                _logger.SetLogWarning($"The breed named {breed.Name} already exists. The breed not updated.");
                return (false, "This breed exists!");
            }

            var breedToUpdate = await _unitOfWork.Breeds.GetAsync(e => e.Id == breed.Id && e.Name != breed.Name);
            if (breedToUpdate == null)
            {
                _logger.SetLogWarning($"The breed named {breed.Name} does not exists. The breed not updated.");
                return (false, "This breed does not exist!"); ;
            }

            breedToUpdate.Name = breed.Name;

            _unitOfWork.Breeds.Update(breedToUpdate);
            await _unitOfWork.SaveAsync();

            _logger.SetLogInfo($"The breed named {breed.Name} has been successfully updated.");

            return (true, "");
        }
    }
}
