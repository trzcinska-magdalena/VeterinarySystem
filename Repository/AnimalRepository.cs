using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Repository
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private readonly VeterinarySystemContext _context;
        public AnimalRepository(VeterinarySystemContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Animal> GetAllWithData(string? whereString)
        {
            IQueryable<Animal> query = _context.Animals
                .Include(a => a.Breed)
                .Include(a => a.Client);

            if (!string.IsNullOrEmpty(whereString))
            {
                query = query.Where(e => e.Name.Contains(whereString));
            }

            return query.ToList();
        }

        public Animal? GetWithAllData(int id)
        {
            return _context.Animals
                .Include(a => a.Breed)
                .Include(a => a.Client)
                .Include(a => a.Weights)
                .SingleOrDefault(e => e.Id == id);
        }
    }
}
