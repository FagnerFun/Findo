using FindoApp.Domain.Model;
using System.Linq;

namespace FindoApp.Domain.Interface.Repository
{
    public interface IRepository<T> where T : DBEntity
    {
        IQueryable<T> Get();
        void Add(T entity);
        void Delete(T entity);
        bool Exist(T Entity);
        T Get(int Id);
    }
}
