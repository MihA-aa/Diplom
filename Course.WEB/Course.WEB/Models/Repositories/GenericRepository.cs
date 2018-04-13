using Course.WEB.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Course.WEB.Models.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext db;
        DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            this.db = context;
            this.dbSet = context.Set<T>();
        }

        public void Create(T item)
        {
            dbSet.Add(item);
        }

        public void Delete(int id)
        {
            T item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public int Count()
        {
            return dbSet.Count();
        }
    }
}
