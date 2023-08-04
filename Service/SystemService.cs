using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Service
{
    public class SystemService : ISystemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetLogError(Exception ex)
        {
            //TODO
        }

        public void SetLogInfo(string information)
        {
            // TODO
        }


        // Base Management Controller
        public async Task<BaseManagementViewModel> ConstructBaseManagementVMAsync()
        {
            var baseManagementViewModel = new BaseManagementViewModel
            {
                AllBreeds = await _unitOfWork.Breeds.GetAllAsync(tracking: false)
            };
            return baseManagementViewModel;
        }

        public async Task<bool> AddNewBreedAsync(Breed breed)
        {
            if (await _unitOfWork.Breeds.IsExistsAsync(e => e.Name == breed.Name))
            {
                SetLogInfo("");
                return false;
            }

            _unitOfWork.Breeds.Add(breed);
            await _unitOfWork.SaveAsync();

            SetLogInfo("");
            return true;
        }

        public async Task<(bool, string)> UpdateBreedAsync(Breed breed)
        {
            if (await _unitOfWork.Breeds.IsExistsAsync(e => e.Name == breed.Name))
            {
                SetLogInfo("");
                return (false, "This breed exists!");
            }

            var breedToUpdate = await _unitOfWork.Breeds.GetAsync(e => e.Id == breed.Id && e.Name != breed.Name);
            if (breedToUpdate == null)
            {
                SetLogInfo("");
                return (false, "This breed does not exist!"); ;
            }

            breedToUpdate.Name = breed.Name;

            _unitOfWork.Breeds.Update(breedToUpdate);
            await _unitOfWork.SaveAsync();

            SetLogInfo("");

            return (true, "");
        }



        // Animal Controller
        public async Task<AnimalsViewModel> ConstructAnimalsVWAsync(string? searchString = null)
        {
            var animalViewModel = new AnimalsViewModel()
            {
                AllAnimals = await _unitOfWork.Animals.GetAllAsync(tracking: false, e => e.Breed, e => e.Client)
            };

            if (searchString != null)
            {
                animalViewModel.AllAnimals = animalViewModel.AllAnimals.Where(e => e.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return animalViewModel;
        }

        public async Task<AnimalCreateViewModel> ConstructAnimalCreateVMAsync()
        {
            var breeds = await _unitOfWork.Breeds.GetAllAsync(tracking: false);
            var clients = await _unitOfWork.Clients.GetAllAsync(tracking: false);

            var animalCreateViewModel = new AnimalCreateViewModel
            {
                AllBreeds = breeds.Select(breed =>
                new SelectListItem
                {
                    Value = breed.Id.ToString(),
                    Text = breed.Name
                }),

                AllClients = clients.Select(client =>
                new SelectListItem
                {
                    Value = client.Id.ToString(),
                    Text = $"{client.FirstName} {client.LastMame}"
                }),
            };
            return animalCreateViewModel;
        }

        public async Task<AnimalDetailViewModel> ConstructAnimalDetailVMAsync()
        {
            var typeOfVaccines = await _unitOfWork.TypeOfVaccines.GetAllAsync(tracking: false);
            var vaccinations = await _unitOfWork.Vaccinations.GetAllAsync(tracking: false);
            var medicines = await _unitOfWork.Medicines.GetAllAsync(tracking: false);
            var surgeries = await _unitOfWork.Surgeries.GetAllAsync(tracking: false);
            var vets = await _unitOfWork.Vets.GetAllAsync(tracking: false);

            AnimalDetailViewModel animalDetailViewModel = new AnimalDetailViewModel();

            animalDetailViewModel.TypeOfVaccines = typeOfVaccines.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            });

            animalDetailViewModel.Vaccinations = vaccinations.GroupBy(a => a.TypeOfVaccine.Name).ToDictionary(e => e.Key, e => e.ToList());

            animalDetailViewModel.Medicines = medicines.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            });

            animalDetailViewModel.Surgeries = surgeries.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            });

            animalDetailViewModel.Vets = vets.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.LastName}"
                });

            return animalDetailViewModel;
        }

        public void SetAnimalDetailViewModel()
        {

        }

        public bool IsValidateWeightWithDate(Weight weight)
        {
            return !(weight.Value <= 0 || weight.Date == default);
        }

        public bool IsValidateVaccination(Vaccination vaccination)
        {
            return !(vaccination.TypeOfVaccineId == 0 || vaccination.Date == default || vaccination.ExpiryDate == default);
        }
    }
}
