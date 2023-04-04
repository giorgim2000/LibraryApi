using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Abstraction
{
    public interface IRepository<T>
    {
        Task Create(T input);
        void Update(T input);
        void Delete(T input);
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetCollection(Expression<Func<T, bool>>? expression = null);
        IQueryable<T> GetQuery(Expression<Func<T, bool>>? expression = null);
        Task<bool> SaveChangesAsync();
    }
}
