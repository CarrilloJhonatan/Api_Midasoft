using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DALL.Data;

namespace DALL.Repositories.Implements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly MidasoftContext midasoftContext;

        public GenericRepository(MidasoftContext midasoftContext)
        {
            this.midasoftContext = midasoftContext;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                throw new Exception("The entity is null");

            midasoftContext.Set<TEntity>().Remove(entity);
            await midasoftContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await midasoftContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await midasoftContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            midasoftContext.Set<TEntity>().Add(entity);
            await midasoftContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            midasoftContext.Entry(entity).State = EntityState.Modified;
            //universityContext.Set<TEntity>().AddOrUpdate(entity);
            await midasoftContext.SaveChangesAsync();
            return entity;
        }
    }
}
