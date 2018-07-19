using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Data.Context;

namespace RushOrders.Data.Repositories
{

    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected SqlContext _context;

        public RepositoryBase(SqlContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        } 

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}