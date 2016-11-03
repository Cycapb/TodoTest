using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TodoDAL.Abstract;
using TodoDAL.Models;

namespace TodoDAL.Concrete
{
    public class EntityRepository<T> : IRepository<T> where T:class
    {
        private readonly DbSet<T> _dbSet;
        private readonly TodoContext _context;

        public EntityRepository()
        {
            _context = new TodoContext();
            _dbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T item)
        {
            _dbSet.Add(item);
            await Task.Run(() => _context.SaveChanges());
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDel = await _dbSet.FindAsync(id);
            if (itemToDel != null)
            {
                await Task.Run((() => _dbSet.Remove(itemToDel)));
            }
        }

        public async Task<T> GetItemAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<T> GetList()
        {
            return _dbSet.AsEnumerable();
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await Task.Run(() =>_dbSet.AsEnumerable());
        }

        public Task UpdateAsync(T item)
        {
            return Task.Run(() => _context.Entry(item).State = EntityState.Modified);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }

}