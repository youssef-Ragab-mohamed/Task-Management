using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Infrastructure.Repositries
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        public AppDbContext Context { get; }
        public GenericRepositry(AppDbContext _Context)
        {
            Context = _Context;
        }

       

        public async  Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            var entity=  await Context.Set<T>().FindAsync(id);
            if (entity == null) return;
             
               Context.Set<T>().Remove(entity);
                await Context.SaveChangesAsync();
          
           
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var include in includes)
            {
                query=query.Include(include);
            }
            return await query.ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query=   Context.Set<T>() ;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        }

        public async Task<T> GetByNameAsync(string name, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Context.Set<T>();
 
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
        }


        public async Task UpdateAsync(T entity)
        {
            var existingEntity = await Context.Set<T>().FindAsync(Context.Entry(entity).Property("Id").CurrentValue);
            if (existingEntity == null) return;

            Context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await Context.SaveChangesAsync();
        }
    }
}
