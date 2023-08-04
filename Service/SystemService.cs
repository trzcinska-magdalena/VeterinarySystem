using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;
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
                AllBreeds = breeds.Select(e =>
                new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }),

                AllClients = clients.Select(e =>
                new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.FirstName} {e.LastMame}"
                }),
            };
            return animalCreateViewModel;
        }

        public async Task<AnimalDetailViewModel> ConstructAnimalDetailVMAsync(Animal animal, string activeTab)
        {
            var typeOfVaccines = await _unitOfWork.TypeOfVaccines.GetAllAsync(tracking: false);
            var vaccinations = await _unitOfWork.Vaccinations.GetAllAsync(tracking: false, e => e.TypeOfVaccine);
            var medicines = await _unitOfWork.Medicines.GetAllAsync(tracking: false);
            var surgeries = await _unitOfWork.Surgeries.GetAllAsync(tracking: false);
            var vets = await _unitOfWork.Vets.GetAllAsync(tracking: false);

            AnimalDetailViewModel animalDetailViewModel = new AnimalDetailViewModel()
            {
                TypeOfVaccines = typeOfVaccines.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }),

                Medicines = medicines.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }),

                Surgeries = surgeries.Select(e =>
                new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }),

                Vets = vets.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.FirstName} {e.LastName}"
                }),

                Animal = animal,
                Appointments = await _unitOfWork.Appointments.GetAppointmentsWithAllData(animal.Id),
                Vaccinations = vaccinations.Where(e => e.AnimalId == animal.Id).GroupBy(e => e.TypeOfVaccine.Name).ToDictionary(e => e.Key, e => e.ToList()),
                ActiveTab = activeTab
            };
            return animalDetailViewModel;
        }

        public async Task<bool> AddNewAnimalAsync(Animal animal)
        {
            _unitOfWork.Animals.Add(animal);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Animal?> GetAnimalWithDetails(int? id)
        {
            if (id == null)
            {
                return null; // throw exception ?? TODO
            }

            var animal = await _unitOfWork.Animals.GetAsync(e => e.Id == id, false, e => e.Breed, e => e.Client, e => e.Weights);
            return animal;
        }

        public bool IsValidateWeightWithDate(Weight weight)
        {
            return !(weight.Value <= 0 || weight.Date == default);
        }

        public async Task<bool> SetWeightToAnimal(Weight weight, Animal animal)
        {
            weight.Animal = animal;
            _unitOfWork.Weights.Add(weight);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Weight> GetWeight(int id, DateTime date)
        {
            return await _unitOfWork.Weights.GetAsync(filter: e => e.AnimalId == id && e.Date == date);
        }

        public bool IsValidateVaccination(Vaccination vaccination)
        {
            return !(vaccination.TypeOfVaccineId == 0 || vaccination.Date == default || vaccination.ExpiryDate == default);
        }
    }
}
