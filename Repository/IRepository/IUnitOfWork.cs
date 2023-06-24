using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAnimalRepository Animals { get; }
        IRepository<Client> Clients { get; }
        IRepository<Account> Accounts { get; }
        IRepository<Breed> Breeds { get; }
        IRepository<Weight> Weights { get; }
        IRepository<TypeOfVaccine> TypeOfVaccines { get; }
        IRepository<Vaccination> Vaccinations { get; }
        //IAppointmentRepository Appointment { get; }

        void Save();
    }
}
