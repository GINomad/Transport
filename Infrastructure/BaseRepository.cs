using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Entity;
using System.Linq.Expressions;


namespace Transport.Infrastructure
{
    public class BaseRepository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        private TransportDBEntities _context;

        public BaseRepository()
        {
            _context = new TransportDBEntities();
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Dispose()
        {
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }

        public void Edit(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
