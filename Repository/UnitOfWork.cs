using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        private VeterinarySystemContext _db;
        public IRepository<Animal> Animals { get; private set; }
        public IRepository<Client> Clients { get; private set; }
        public IRepository<Breed> Breeds { get; private set; }
        public IRepository<Weight> Weights { get; private set; }
        public IRepository<TypeOfVaccine> TypeOfVaccines { get; private set; }
        public IRepository<Vaccination> Vaccinations { get; private set; }
        public IRepository<Vet> Vets { get; private set; }
        public IRepository<Specialisation> Specialisations { get; private set; }
        public IRepository<Medicine> Medicines { get; private set; }
        public IRepository<Surgery> Surgeries { get; private set; }
        public IRepository<VetSpecialisation> VetSpecialisations { get; private set; }
        public IRepository<AppointmentVet> AppointmentVets { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }

        public UnitOfWork(VeterinarySystemContext db, ILogger<UnitOfWork> logger)
        {
            _db = db;
            Animals = new Repository<Animal>(_db);
            Clients = new Repository<Client>(_db);
            Breeds = new Repository<Breed>(_db);
            Weights = new Repository<Weight>(_db);
            TypeOfVaccines = new Repository<TypeOfVaccine>(_db);
            Vaccinations = new Repository<Vaccination>(_db);
            Surgeries = new Repository<Surgery>(_db);
            Medicines = new Repository<Medicine>(_db);
            Appointments = new AppointmentRepository(_db);
            Vets = new Repository<Vet>(_db);
            Specialisations = new Repository<Specialisation>(_db);
            VetSpecialisations = new Repository<VetSpecialisation>(_db);
            AppointmentVets = new Repository<AppointmentVet>(_db);
            _logger = logger;
        }

        public async Task SaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch(DbUpdateException ex) 
            {
                _logger.LogError(ex, "An error occurred in SaveAsync.");

                throw new DbUpdateException("An error occurred while saving data to the database.", ex);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "An error occurred in SaveAsync.");

                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }
    }
}
