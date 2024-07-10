using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        DbSet<T> GetDbSet();
        Task Insert(T entity);
        Task Update(T entityToUpdate);
        Task Delete(T entityToDelete);
    }
}
