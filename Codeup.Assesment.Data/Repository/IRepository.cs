using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeup.Assesment.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string includes = "");
        T GetById(object id);
        T Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        IQueryable<T> GetAllQueryable(string includes = "");
        Task<T> InsertAsync(T obj);
        Task<IEnumerable<T>> GetAllAsync(string includes = "");
        Task<T> GetByIdAsync(object id);
    }
}
