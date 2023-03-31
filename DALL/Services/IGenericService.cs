using System.Collections.Generic;
using System.Threading.Tasks;

namespace DALL.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(string cedula);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(string cedula);
    }
}
