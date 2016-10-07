using System.Collections.Generic;
using System.Threading.Tasks;
using TodoDAL.Models;

namespace TodoWEB.Abstract
{
    public interface ITodoManager
    {
        Task<IEnumerable<Todo>> GetListAsync(int userId);
        Task<Todo> GetItemAsync(int id);
        Task UpdateAsync(Todo item);
        Task DeleteAsync(int id);
        Task CreateAsync(Todo item);
        Task CompleteAsync(int id);
    }
}
