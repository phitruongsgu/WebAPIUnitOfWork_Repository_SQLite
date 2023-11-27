using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UOWPocketBookAPI.Core.IRepositories;
using UOWPocketBookAPI.Data;

namespace UOWPocketBookAPI.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(ApplicationDbContext context, ILogger logger) {
            _context = context; 
            _logger = logger;
            dbSet = _context.Set<T>();
        }
        public virtual async Task<bool> Add(T entity) // virtual để thằng con như UserRepository nó implement thì nó override lại các hàm ở đây
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync(); 
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
