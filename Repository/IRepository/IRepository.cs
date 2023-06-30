using System.Linq.Expressions;

namespace VeterinarySystem.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
