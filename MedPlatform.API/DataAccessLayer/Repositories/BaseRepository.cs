using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext dataContext;
        private readonly DbSet<T> dbSet;

         public DataContext GetDataContext(IServiceProvider services)
         {
            var dataContext = services
                .GetRequiredService<DataContext>();

            return dataContext;
         }

        public BaseRepository(IServiceProvider serviceProvider)
        {
            this.dataContext = GetDataContext(serviceProvider);
            dbSet = dataContext.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await Task.FromResult(dbSet.ToList());
        }

        public DbSet<T> GetDbSet()
        {
            return dbSet;
        }

        public async Task Insert(T entity)
        {
            await dbSet.AddAsync(entity);

            await dataContext.SaveChangesAsync();
        }

        public async Task Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dataContext.Entry(entityToUpdate).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }

        public async Task Delete(T entityToDelete)
        {
            if (dataContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

            await dataContext.SaveChangesAsync();
        }
    }
}
