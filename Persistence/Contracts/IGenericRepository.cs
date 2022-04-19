using System.Threading.Tasks;
using System.Collections.Generic;

namespace Persistence.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    T Update(T entity);
    T Delete(T entity);
    Task<bool> ExistsAsync(int id);
}