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

        public T Get(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
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
