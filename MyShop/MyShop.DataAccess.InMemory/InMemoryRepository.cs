using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> list;
        string className;

        public InMemoryRepository() {
            className = typeof(T).Name;
            list = cache[className] as List<T>;
            if (list == null) {
                list = new List<T>();
                Commit();
            }
        }

        public void Commit() {
            cache[className] = list;
        }

        public void Insert(T t) {
            list.Add(t);
        }

        public void Update(T t) {
            T tToUpdate = list.Find(p => p.Id == t.Id);
            if (tToUpdate != null)
                tToUpdate = t;
            else
                throw new Exception("Product not found");
        }

        public T Find(string Id) {
            T t = list.Find(p => p.Id == Id);
            if (t != null)
                return t;
            else
                throw new Exception( className + " not found");
        }

        public void Delete(string Id) {
            T tToDelete = list.Find(p => p.Id == Id);
            if (tToDelete != null)
                list.Remove(tToDelete);
            else
                throw new Exception(className + " not found");
        }

        public IQueryable<T> Collection() {
            return list.AsQueryable();
        }



    }
}
