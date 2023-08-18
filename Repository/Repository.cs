using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;
using VeterinarySystem.Data;
using VeterinarySystem.Repository.IRepository;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VeterinarySystemContext _db;
        private readonly ILoggerService _logger;
        internal DbSet<T> dbSet;

        public Repository(VeterinarySystemContext db, ILoggerService logger)
        {
            _db = db;
            dbSet = _db.Set<T>();
            _logger = logger;
        }
        public void Add(T entity)
        {
            _logger.SetLogInfo($"Starting Add method with {entity} entity.");
            dbSet.Add(entity);      
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            _logger.SetLogInfo($"Starting GetAsync method with {filter} filter.");
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
                _logger.SetLogError(ex);
                throw new DbUpdateException("An error occurred regarding the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                _logger.SetLogError(ex);
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                _logger.SetLogError(ex);
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                _logger.SetLogError(ex);
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool tracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            _logger.SetLogInfo("Starting GetAllAsync method.");
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
                _logger.SetLogError(ex);
                throw new DbUpdateException("An error occurred regarding the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                _logger.SetLogError(ex);
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                _logger.SetLogError(ex);
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                _logger.SetLogError(ex);
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> filter)
        {
            _logger.SetLogInfo($"Starting IsExistsAsync method with {filter} filter.");
            try
            {
                return await dbSet.AsNoTracking().AnyAsync(filter);
            }
            catch (DbUpdateException ex)
            {
                _logger.SetLogError(ex);
                throw new DbUpdateException("An error occurred regarding the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                _logger.SetLogError(ex);
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                _logger.SetLogError(ex);           
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                _logger.SetLogError(ex);
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }

        public void Remove(T entity)
        {
            _logger.SetLogInfo($"Starting Remove method with {entity} entity.");
            try
            {
                dbSet.Remove(entity);
            }
            catch (DbUpdateException ex)
            {
                _logger.SetLogError(ex);
                throw new DbUpdateException("An error occurred while removing data to the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                _logger.SetLogError(ex);
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                _logger.SetLogError(ex);
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                _logger.SetLogError(ex);
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }

        public void Update(T entity)
        {
            _logger.SetLogInfo($"Starting Update method with {entity} entity.");
            try
            {
                dbSet.Update(entity);
            }
            catch (DbUpdateException ex)
            {
                _logger.SetLogError(ex);
                throw new DbUpdateException("An error occurred while updating data to the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                _logger.SetLogError(ex);
                throw new InvalidOperationException("An error occurred regarding operations not allowed.", ex);
            }
            catch (ArgumentNullException ex)
            {
                _logger.SetLogError(ex);
                throw new ArgumentNullException("An error occurred regarding null arguments.", ex);
            }
            catch (Exception ex)
            {
                _logger.SetLogError(ex);
                throw new Exception("Unknown error occurred while saving data to the database.", ex);
            }
        }
    }
}
