using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitManager.Repositories
{
    public abstract class BaseRepositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> banco;
        public AppDbContext getContext() => _context;

        protected BaseRepositorio(AppDbContext context)
        {
            _context = context;
            banco = context.Set<T>();
        }

        public virtual async Task<T?> getByIdAsync(int id)
            => await banco.FindAsync(id);

        public virtual async Task<List<T>> getAllAsync()
            => await banco.ToListAsync();

        public virtual async Task addAsync(T entity)
        {
            await banco.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task updateAsync(int id, T entity)
        {
            banco.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task deleteAsync(int id)
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
        Task<T?> getByIdAsync(int id);
        Task<List<T>> getAllAsync();
        Task addAsync(T entity);
        Task updateAsync(int id, T entity);
        Task deleteAsync(int id);
        Task<List<T>> findAsync(Expression<Func<T, bool>> predicate);
    }
}