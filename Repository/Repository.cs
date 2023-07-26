using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VeterinarySystem.Data;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VeterinarySystemContext _db;
        internal DbSet<T> dbSet;

        public Repository(VeterinarySystemContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
