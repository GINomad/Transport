using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Transport.Infrastructure
{
    public interface IRepository<T> : IDisposable
    {
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
