using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.Repositories
{
    public abstract class BaseRepositorio<T>
        : IRepositorio<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> banco;

        protected BaseRepositorio(AppDbContext context)
        {
            _context = context;
            banco = context.Set<T>();
        }

        public virtual async Task<T?> getByIdAsync(string id)
            => await banco.FindAsync(id);

        public virtual async Task<List<T>> getAllAsync()
            => await banco.ToListAsync();

        public virtual async Task addAsync(T entity)
        {
            await banco.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task updateAsync(string id, T entity)
        {
            banco.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task deleteAsync(string id)
        {
            var entity = await getByIdAsync(id);
            if (entity != null)
            {
                banco.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<List<T>> findAsync(Expression<Func<T, bool>> predicate)
            => await banco.Where(predicate).ToListAsync();
    }
    public interface IRepositorio<T>
    {
        Task<T?> getByIdAsync(string id);
        Task<List<T>> getAllAsync();
        Task addAsync(T entity);
        Task updateAsync(string id, T entity);
        Task deleteAsync(string id);
        Task<List<T>> findAsync(Expression<Func<T, bool>> predicate);
    }
}
