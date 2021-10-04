using HotDesk.Data;
using HotDesk.Models.AdminModels;
using HotDesk.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HotDesk.Repositories
{

    public class Repository : IRepository
    {
        private readonly HotDeskContext context;

        public Repository(HotDeskContext _context)
        {
            context = _context;
        }


        public T Get<T>(Func<T, bool> predicate) where T : class
        {
            return context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public async Task<IQueryable<T>> GetAll<T>() where T : class
        {
            var result = await context.Set<T>().ToListAsync();
            return result.AsQueryable();
        }

        public IQueryable<T> GetAll<T>(Func<T, bool> predicate) where T : class
        {
            var result = context.Set<T>().Where(predicate).AsQueryable();
            return result;
        }


        public T GetById<T>(int id) where T : class
        {
            var result = context.Set<T>().Find(id);
            return result;
        }

        public async Task<bool> Add<T>(T item) where T : class
        {
            context.Set<T>().Add(item);
            return true;
        }

        public void Remove<T>(T item) where T : class
        {
            context.Set<T>().Remove(item);
            context.SaveChanges();
        }


        public T Check<T>(Func<T, bool> predicate) where T:class
        {
            var result = context.Set<T>().FirstOrDefault(predicate);
            return result;
        }

        public async Task<bool> Update<T> (T item) where T:class
        {
            context.Set<T>().Update(item);
            return true;
        }

        public async Task<bool> SaveChanges() {
            await context.SaveChangesAsync();
            return true;
        }



        public async Task<int> AddReservation(Reservation reservation)
        {
            context.Reservation.Add(reservation);
            await context.SaveChangesAsync();
            return reservation.Id;
        }

    }
}
