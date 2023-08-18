using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILoggerService _logger;
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

        public UnitOfWork(VeterinarySystemContext db, ILoggerService logger)
        {
            _logger = logger;
            _db = db;
            Animals = new Repository<Animal>(_db, _logger);
            Clients = new Repository<Client>(_db, _logger);
            Breeds = new Repository<Breed>(_db, _logger);
            Weights = new Repository<Weight>(_db, _logger);
            TypeOfVaccines = new Repository<TypeOfVaccine>(_db, _logger);
            Vaccinations = new Repository<Vaccination>(_db, _logger);
            Surgeries = new Repository<Surgery>(_db, _logger);
            Medicines = new Repository<Medicine>(_db, _logger);
            Appointments = new AppointmentRepository(_db); // TODO
            Vets = new Repository<Vet>(_db, _logger);
            Specialisations = new Repository<Specialisation>(_db, _logger);
            VetSpecialisations = new Repository<VetSpecialisation>(_db, _logger);
            AppointmentVets = new Repository<AppointmentVet>(_db, _logger);
        }

        public async Task SaveAsync()
        {
            _logger.SetLogInfo("Starting SaveAsync method.");
            try
            {
                await _db.SaveChangesAsync();
            }
            catch(DbUpdateException ex) 
            {
                _logger.SetLogError(ex);

                throw new DbUpdateException("An error occurred while saving data to the database.", ex);
            }
            catch(Exception ex) 
            {
                _logger.SetLogError(ex);

                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }
    }
}
