﻿using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly UnvcenteralDataBaseContext _context;
        public GenericRepository(UnvcenteralDataBaseContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T Model)
        {
            await _context.Set<T>().AddAsync(Model);
            return Model;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var Data = await _context.Set<T>().AsNoTracking().ToListAsync();
            return Data;
        }
        public async Task<IEnumerable<T>> GetAllIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var data = await query.ToListAsync();
            return data;
        }
        public async Task<T>? GetByIDAsync(int id)
        {
            var Entity = await _context.Set<T>().FindAsync(id);
            return Entity;
        }
        public async Task<T>? GetByNameAsync(Expression<Func<T, bool>> expression)
        {
            var Entity = await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
            return Entity;
        }
        public async Task<T> UpdateAsync(T model, int id)
        {
            T entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(model);
                _context.Entry(entity).State = EntityState.Modified;
                return model;
            }
            return null;
        }
        public void Delete(int id)
        {
            try
            {
                T Entity =  _context.Set<T>().Find(id);
                _context.Set<T>().Remove(Entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot Delete this Entity");
            }
        }

        public async Task<T> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            var Entity = await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
            return Entity;
        }
    }
}
