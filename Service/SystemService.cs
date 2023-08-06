﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using VeterinarySystem.Models;
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

        private IEnumerable<SelectListItem> GetSelectListItems<T>(IEnumerable<T> entities, Func<T, string> textSelector, Func<T, string> valueSelector)
        {
            var selectListItems = entities.Select(e => new SelectListItem
            {
                Value = valueSelector(e),
                Text = textSelector(e)
            });

            return selectListItems;
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
                AllBreeds = GetSelectListItems(breeds, e => e.Id.ToString(), e => e.Name),
                AllClients = GetSelectListItems(clients, e => e.Id.ToString(), e => $"{e.FirstName} {e.LastMame}")
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
                TypeOfVaccines = GetSelectListItems(typeOfVaccines, e => e.Id.ToString(), e => e.Name),
                Medicines = GetSelectListItems(medicines, e => e.Id.ToString(), e => e.Name),
                Surgeries = GetSelectListItems(surgeries, e => e.Id.ToString(), e => e.Name),
                Vets = GetSelectListItems(vets, e => e.Id.ToString(), e => $"{e.FirstName} {e.LastName}"),
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

        public async Task<Animal?> GetAnimalWithDetails(int id)
        {         
            var animal = await _unitOfWork.Animals.GetAsync(e => e.Id == id, false, e => e.Breed, e => e.Client, e => e.Weights);
            return animal;
        }

        public bool IsValidTheActiveTab(string activeTab)
        {
            string[] activeTabs = { "Weight", "Appointment", "Vaccination" };

            if(!activeTabs.Contains(activeTab))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddWeightToAnimal(Weight weight, Animal animal)
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

        public async Task<(bool, string)> UpdateWeight(Weight weight, int value)
        {
            if (value == 0)
            {
                _unitOfWork.Weights.Remove(weight);
                await _unitOfWork.SaveAsync();
                return (true, "Weight deleted successfully");
            }
            else
            {
                weight.Value = value;
                _unitOfWork.Weights.Update(weight);
                await _unitOfWork.SaveAsync();
                return (true, "Weight updated successfully");
            }
        }

        public async Task<bool> AddVaccinationToAnimal(Vaccination vaccination, int animalId)
        {
            vaccination.AnimalId = animalId;
            _unitOfWork.Vaccinations.Add(vaccination);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Event>> GetAllAppointmentsAsEvent(int animalId)
        {
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
