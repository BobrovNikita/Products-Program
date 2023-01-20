using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllByValue(string value);
        T GetModel(Guid id);
        void Create(T model);
        void Update(T model);
        void Delete(T model);
    }
}
