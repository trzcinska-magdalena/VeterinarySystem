using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRepository<Animal> Animals { get; }
        IRepository<Client> Clients { get; }
        IRepository<Breed> Breeds { get; }
        IRepository<Weight> Weights { get; }
        IRepository<TypeOfVaccine> TypeOfVaccines { get; }
        IRepository<Vaccination> Vaccinations { get; }
        IRepository<Medicine> Medicines { get; }
        IRepository<Surgery> Surgeries { get; }
        IAppointmentRepository Appointments { get; }
        IRepository<Vet> Vets { get; }
        IRepository<Specialisation> Specialisations { get; }
        IRepository<VetSpecialisation> VetSpecialisations { get; }
        IRepository<AppointmentVet> AppointmentVets { get; }

        Task SaveAsync();
    }
}
