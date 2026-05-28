using FitManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.Business
{
    public abstract class Negocio<T>
    {
        protected readonly IRepositorio<T> repositorio;

        protected Negocio(IRepositorio<T> repository)
        {
            repositorio = repository;
        }

        public virtual async Task<List<T>> getAllAsync() => await repositorio.getAllAsync();

        public virtual async Task<T?> getByIdAsync(string id) => await repositorio.getByIdAsync(id);

        public virtual async Task addAsync(T entity) => await repositorio.addAsync(entity);

        public virtual async Task updateAsync(string id, T entity) => await repositorio.updateAsync(id, entity);

        public virtual async Task deleteAsync(string id) => await repositorio.deleteAsync(id);


    }


    public interface INegocio<T>
    {
        Task<T?> getByIdAsync(string id);
        Task<List<T>> getAllAsync();
        Task addAsync(T entity);
        Task updateAsync(string id, T entity);
        Task deleteAsync(string id);
    }
}
