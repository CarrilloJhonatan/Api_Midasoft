using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        public async Task Delete(string cedula)
        {
            var entity = await GetById(cedula);

            if (entity == null)
                throw new Exception("The entity is null");

            midasoftContext.Set<TEntity>().Remove(entity);
            await midasoftContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await midasoftContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(string cedula)
        {
            return await midasoftContext.Set<TEntity>().FindAsync(cedula);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            midasoftContext.Set<TEntity>().Add(entity);
            await midasoftContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            //midasoftContext.Entry(entity).State = EntityState.Modified;
            midasoftContext.Set<TEntity>().AddOrUpdate(entity);
            await midasoftContext.SaveChangesAsync();
            return entity;
        }
    }
}
