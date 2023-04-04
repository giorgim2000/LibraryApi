using Infrastructure.Data;
using Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }
        public async Task Create(T input)
        {
            await _context.Set<T>().AddAsync(input);
        }

        public void Update(T input)
        {
            _context.Set<T>().Update(input);
        }
        public void Delete(T input)
        {
            _context.Set<T>().Remove(input);
        }

        public async Task<IEnumerable<T>> GetCollection(Expression<Func<T,bool>>? expression = null)
        {
            return expression == null ? await _context.Set<T>().AsNoTracking().ToListAsync() : 
                                        await _context.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>>? expression = null)
        {
            return expression == null ? _context.Set<T>() : _context.Set<T>().Where(expression);
        }
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
