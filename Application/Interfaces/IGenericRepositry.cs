using System.Linq.Expressions;

namespace TaskManagement.Application.Interfaces
{
    public interface IGenericRepositry<  T> where T : class
    {
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> GetByNameAsync(string name, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id   );



    }
}
