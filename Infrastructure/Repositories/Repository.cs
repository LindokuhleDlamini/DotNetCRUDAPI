using System.Linq.Expressions;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class Repository<T>: IRepository<T> where T: class
	{
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

		public Repository(DbContext context)
		{
            _context = context;
            _dbSet = _context.Set<T>();

        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T? entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            int results = await _context.SaveChangesAsync();

            if (results > 0)
            {
                return true;
            }

            return false; 
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            int results = await _context.SaveChangesAsync();

            if (results > 0)
            {
                return true;
            }

            return false;
        }
    }
}

