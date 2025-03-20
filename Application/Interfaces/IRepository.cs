using System;
using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces
{
	public interface IRepository<T> where T: class
	{
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}

