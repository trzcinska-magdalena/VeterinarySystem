using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private VeterinarySystemContext _db;
        public IAnimalRepository Animals { get; private set; }
        public IRepository<Client> Clients { get; private set; }
        public IRepository<Breed> Breeds { get; private set; }
        public IRepository<Account> Accounts { get; private set; }
        public IRepository<Weight> Weights { get; private set; }
        public IRepository<TypeOfVaccine> TypeOfVaccines { get; private set; }
        public IRepository<Vaccination> Vaccinations { get; private set; }
        public IAppointmentRepository Appointment { get; private set; }


        public UnitOfWork(VeterinarySystemContext db)
        {
            _db = db;
            Animals = new AnimalRepository(_db);
            Clients = new Repository<Client>(_db);
            Breeds = new Repository<Breed>(_db);
            Accounts = new Repository<Account>(_db);
            Weights = new Repository<Weight>(_db);
            TypeOfVaccines = new Repository<TypeOfVaccine>(_db);
            Vaccinations = new Repository<Vaccination>(_db);
            Appointment = new AppointmentRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
