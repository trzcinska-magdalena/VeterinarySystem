using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
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
            try
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
            catch (DbUpdateException ex)
            {
                // TODO - Log the error details to the log file
                throw new DbUpdateException("An error occurred regarding the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // TODO - Log the error details to the log file
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                // TODO - Log the error details to the log file
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                // TODO - Log the error details to the log file
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool tracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            try
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
            catch (DbUpdateException ex)
            {
                // TODO - Log the error details to the log file
                throw new DbUpdateException("An error occurred regarding the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // TODO - Log the error details to the log file
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                // TODO - Log the error details to the log file
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                // TODO - Log the error details to the log file
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }

        public async Task<bool> BreedExistsAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await dbSet.AsNoTracking().AnyAsync(filter);
            }
            catch (DbUpdateException ex)
            {
                // TODO - Log the error details to the log file
                throw new DbUpdateException("An error occurred regarding the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // TODO - Log the error details to the log file
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                // TODO - Log the error details to the log file
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                // TODO - Log the error details to the log file
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }

        public void Remove(T entity)
        {        
            try
            {
                dbSet.Remove(entity);
            }
            catch (DbUpdateException ex)
            {
                // TODO - Log the error details to the log file
                throw new DbUpdateException("An error occurred while removing data to the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // TODO - Log the error details to the log file
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                // TODO - Log the error details to the log file
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                // TODO - Log the error details to the log file
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }

        public void Update(T entity)
        {
            try
            {
                dbSet.Update(entity);
            }
            catch (DbUpdateException ex)
            {
                // TODO - Log the error details to the log file
                throw new DbUpdateException("An error occurred while updating data to the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // TODO - Log the error details to the log file
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                // TODO - Log the error details to the log file
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                // TODO - Log the error details to the log file
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }
    }
}
