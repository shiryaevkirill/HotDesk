using HotDesk.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Repositories
{
    public interface IRepository
    {
        T Get<T>(Func<T, bool> predicate) where T : class;
        Task<IQueryable<T>> GetAll<T>() where T : class;
        IQueryable<T> GetAll<T>(Func<T, bool> predicate) where T : class;
        Task<T> GetById<T>(int id) where T : class;
        void Add<T>(T item) where T : class;
        void Remove<T>(T item) where T : class;

        T Check<T>(Func<T, bool> predicate) where T : class;

        void Update<T>(T item) where T : class;

        void SaveChanges();
        Task<int> AddReservation(Reservation reservation);
    }
}
