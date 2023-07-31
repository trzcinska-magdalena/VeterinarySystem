using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet.Where(filter);
       
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            if (!tracking)
            {
                return await query.AsNoTracking().FirstOrDefaultAsync();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool tracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            if (!tracking)
            {
                return await query.AsNoTracking().ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<bool> BreedExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.AsNoTracking().AnyAsync(filter);          
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
