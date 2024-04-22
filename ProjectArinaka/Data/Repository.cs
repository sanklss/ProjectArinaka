using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArinaka.Data
{
    using ProjectArinaka.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Repository<T> where T : class
    {
        private readonly Ww2Context _context;
        private DbSet<T> _dbSet;

        public Repository(Ww2Context context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

       
        public async Task<Ww2table> GetByInventoryNumberAsync(long inventoryNumber)
        {
            return await _context.Ww2tables
                .FirstOrDefaultAsync(e => e.ИнвентарныйНомер == inventoryNumber);
        }

        public async Task<List<Ww2table>> GetByEmployeeNameAsync(string employeeName)
        {
            return await _context.Ww2tables
                .Where(e => e.Фио == employeeName)
                .ToListAsync();
        }
    }

}
