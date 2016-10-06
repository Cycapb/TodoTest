using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoDAL.Abstract
{
    public interface IRepository<T> where T:class 
    {
        Task<IEnumerable<T>> GetListAsync();
        Task<T> GetItemAsync(int id);
        Task CreateAsync(T item);
        Task DeleteAsync(int id);
        Task UpdateAsync(T item);
        Task SaveAsync();
    }
}