using System.Collections.Generic;
using System.Threading.Tasks;
using DALL.Repositories;

namespace DALL.Services.Implements
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private IGenericRepository<TEntity> genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task Delete(string cedula)
        {
            await genericRepository.Delete(cedula);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await genericRepository.GetAll();
        }

        public async Task<TEntity> GetById(string cedula)
        {
            return await genericRepository.GetById(cedula);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            return await genericRepository.Insert(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            return await genericRepository.Update(entity);
        }
    }
}
