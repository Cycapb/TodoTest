using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoDAL.Abstract;
using TodoDAL.Models;
using TodoWEB.Abstract;

namespace TodoWEB.Concrete
{
    public class TodoManager:ITodoManager
    {
        private readonly IRepository<Todo> _todoRepository;

        public TodoManager(IRepository<Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public IEnumerable<Todo> GetList(int userId)
        {
            return _todoRepository.GetList().Where(x => x.UserId == userId);
        }

        public async Task<IEnumerable<Todo>> GetListAsync(int userId)
        {
            return (await _todoRepository.GetListAsync()).Where(x => x.UserId == userId);
        }

        public async Task<Todo> GetItemAsync(int id)
        {
            return await _todoRepository.GetItemAsync(id);
        }

        public async Task UpdateAsync(Todo item)
        {
            await _todoRepository.UpdateAsync(item);
            await _todoRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _todoRepository.DeleteAsync(id);
            await _todoRepository.SaveAsync();
        }

        public async Task CreateAsync(Todo item)
        {
            await _todoRepository.CreateAsync(item);
        }

        public async Task CompleteAsync(int id)
        {
            var todo = await _todoRepository.GetItemAsync(id);
            todo.Complete = true;
            await _todoRepository.UpdateAsync(todo);
            await _todoRepository.SaveAsync();
        }
    }
}