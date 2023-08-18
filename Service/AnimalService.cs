using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Linq.Expressions;
using VeterinarySystem.Models;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Service
{
    public class AnimalService : IAnimalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalService _globalService;
        private readonly ILoggerService _logger;

        public AnimalService(IUnitOfWork unitOfWork, IGlobalService globalService, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _globalService = globalService;
            _logger = logger;
        }
        public async Task<AnimalsViewModel> ConstructAnimalsVWAsync(string? searchString = null)
        {
            _logger.SetLogInfo("Starting ConstructAnimalsVWAsync method.");

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
            _logger.SetLogInfo("Starting ConstructAnimalCreateVMAsync method.");

            var breeds = await _unitOfWork.Breeds.GetAllAsync(tracking: false);
            var clients = await _unitOfWork.Clients.GetAllAsync(tracking: false);

            var animalCreateViewModel = new AnimalCreateViewModel
            {
                AllBreeds = _globalService.GetSelectListItems(breeds, e => e.Id.ToString(), e => e.Name),
                AllClients = _globalService.GetSelectListItems(clients, e => e.Id.ToString(), e => $"{e.FirstName} {e.LastMame}")
            };
            return animalCreateViewModel;
        }

        public async Task<AnimalDetailViewModel> ConstructAnimalDetailVMAsync(Animal animal, string activeTab)
        {
            _logger.SetLogInfo("Starting ConstructAnimalDetailVMAsync method.");

            var typeOfVaccines = await _unitOfWork.TypeOfVaccines.GetAllAsync(tracking: false);
            var vaccinations = await _unitOfWork.Vaccinations.GetAllAsync(tracking: false, e => e.TypeOfVaccine);
            var medicines = await _unitOfWork.Medicines.GetAllAsync(tracking: false);
            var surgeries = await _unitOfWork.Surgeries.GetAllAsync(tracking: false);
            var vets = await _unitOfWork.Vets.GetAllAsync(tracking: false);

            AnimalDetailViewModel animalDetailViewModel = new AnimalDetailViewModel()
            {
                AllTypeOfVaccines = _globalService.GetSelectListItems(typeOfVaccines, e => e.Id.ToString(), e => e.Name),
                AllMedicines = _globalService.GetSelectListItems(medicines, e => e.Id.ToString(), e => e.Name),
                AllSurgeries = _globalService.GetSelectListItems(surgeries, e => e.Id.ToString(), e => e.Name),
                AllVets = _globalService.GetSelectListItems(vets, e => e.Id.ToString(), e => $"{e.FirstName} {e.LastName}"),
                Animal = animal,
                AllAppointments = await _unitOfWork.Appointments.GetAppointmentsWithAllData(animal.Id),
                AllVaccinations = vaccinations.Where(e => e.AnimalId == animal.Id).GroupBy(e => e.TypeOfVaccine.Name).ToDictionary(e => e.Key, e => e.ToList()),
                ActiveTab = activeTab
            };
            return animalDetailViewModel;
        }

        public async Task<bool> AddNewAnimalAsync(Animal animal)
        {
            _logger.SetLogInfo("Starting AddNewAnimalAsync method.");

            _unitOfWork.Animals.Add(animal);
            await _unitOfWork.SaveAsync();

            _logger.SetLogInfo($"The animal named {animal.Name} has been successfully added.");
            return true;
        }

        public async Task<Animal?> GetAnimalWithDetails(int id)
        {
            _logger.SetLogInfo("Starting GetAnimalWithDetails method.");
            var animal = await _unitOfWork.Animals.GetAsync(e => e.Id == id, false, e => e.Breed, e => e.Client, e => e.Weights);
            return animal;
        }

        public bool IsValidTheActiveTab(string activeTab)
        {
            _logger.SetLogInfo("Starting IsValidTheActiveTab method.");

            string[] activeTabs = { "Weight", "Appointment", "Vaccination" };

            if (!activeTabs.Contains(activeTab))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddWeightToAnimal(Weight weight, Animal animal)
        {
            _logger.SetLogInfo("Starting AddWeightToAnimal method.");

            weight.Animal = animal;
            _unitOfWork.Weights.Add(weight);
            await _unitOfWork.SaveAsync();

            _logger.SetLogInfo($"The weight {weight.Value} has been added to animal named {animal.Name}.");
            return true;
        }

        public async Task<Weight> GetWeight(int id, DateTime date)
        {
            _logger.SetLogInfo("Starting GetWeight method.");
            return await _unitOfWork.Weights.GetAsync(filter: e => e.AnimalId == id && e.Date == date);
        }

        public async Task<(bool, string)> UpdateWeight(Weight weight, int value)
        {
            _logger.SetLogInfo("Starting UpdateWeight method.");
            if (value == 0)
            {
                _unitOfWork.Weights.Remove(weight);
                await _unitOfWork.SaveAsync();

                _logger.SetLogInfo($"The weight {weight.Value} has been deleted.");
                return (true, "Weight was deleted successfully");
            }
            else
            {
                weight.Value = value;
                _unitOfWork.Weights.Update(weight);
                await _unitOfWork.SaveAsync();

                _logger.SetLogInfo($"The weight {weight.Value} has been updated.");
                return (true, "Weight was updated successfully");
            }
        }

        public async Task<bool> AddVaccinationToAnimal(Vaccination vaccination, int animalId)
        {
            _logger.SetLogInfo("Starting AddVaccinationToAnimal method.");

            vaccination.AnimalId = animalId;
            _unitOfWork.Vaccinations.Add(vaccination);
            await _unitOfWork.SaveAsync();

            _logger.SetLogInfo($"The vaccination {vaccination.TypeOfVaccine.Name} has been added to animal named {vaccination.Animal.Name}.");
            return true;
        }

        public async Task<IEnumerable<Event>> GetAllAppointmentsAsEvent(int animalId)
        {
            _logger.SetLogInfo("Starting GetAllAppointmentsAsEvent method.");

            var appointmants = await _unitOfWork.Appointments.GetAppointmentsWithAllData(animalId);

            var events = appointmants.Select(e => new Event
            {
                Id = e.Id,
                Title = e.Description,
                Description = e.Description,
                Start = e.Date.ToString("yyyy-MM-dd HH:mm")
            });
            return events;
        }
    }
}
