using FindoApp.Domain.Interface.Repository;
using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model;
using SQLite;
using System.Linq;

namespace FindoApp.Infra.SQLite
{
    public class GenericRepository<T> : SQLiteConnection, IRepository<T> where T : DBEntity, new()
    {
        public GenericRepository(IDataBaseAccessService dataBaseAccessService) : base(dataBaseAccessService.GetDataBasePath())
        {
            CreateTable<T>();
        }

        public void Add(T entity)
        {
            this.Insert(entity);
        }

        public void Delete(T entity)
        {
            this.Delete(entity);
        }

        public bool Exist(T Entity)
        {
            return (from c in Table<T>() where c.Id == Entity.Id select c).Any();
        }

        public IQueryable<T> Get()
        {
            return (from t in Table<T>() select t).AsQueryable();
        }

        public T Get(int Id)
        {
            return (from c in Table<T>() where c.Id == Id select c).FirstOrDefault();
        }
    }
}
