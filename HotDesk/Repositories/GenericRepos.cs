using HotDesk.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Repositories
{
    public class GenericRepos<T> where T : class
    {
        private readonly HotDeskContext context;
        DbSet<T> _DbSet;

        public GenericRepos(HotDeskContext _context)
        {
            context = _context;
            _DbSet = context.Set<T>();
        }

        public async Task<T> FindById(int id)
        {
            var item = await _DbSet.FindAsync(id);
            return item;
        }

        public async Task<bool> Remove(T item)
        {
            _DbSet.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }


    }
}
