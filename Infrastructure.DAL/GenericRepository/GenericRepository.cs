using Infrastructure.DAL.DAL_Core.EF;
using Infrastructure.DAL.DAL_Core.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.DAL.GenericRepository
{
    public abstract class GenericRepository<T> : IAsyncEnumerable<T> where T : class
    {

        private EntitiesContext _dataContext;

        private readonly DbSet<T> _dbSet;

        protected IDbFactory DbFactory { get; private set; }

        protected EntitiesContext DbContext
        {
            get { return _dataContext ?? (_dataContext = DbFactory.Init()); }
        }

        protected GenericRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        public virtual void Add(T entity)//TODO Добавление Exception
        {

            DetachEntities();

            _dbSet.Add(entity); // eq  Db.Set<T>().Attach(entity); 
            _dataContext.Entry(entity).State = EntityState.Added;
            _dataContext.SaveChanges();

        }

        public virtual void AddRange(IEnumerable<T> entities)//TODO Добавление Exception
        {
            _dataContext.Set<T>().AddRange(entities);
            _dataContext.SaveChanges();
        }


        public virtual T AddWithReturn(T entity)
        {
            try
            {
                DetachEntities();
                _dbSet.Add(entity); // eq  Db.Set<T>().Attach(entity); 
                _dataContext.Entry(entity).State = EntityState.Added;
                _dataContext.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void DetachEntities()//TODO Добавление Exception
        {
            IEnumerable<EntityEntry> objectStateEntries =
                _dataContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Unchanged);
            foreach (EntityEntry objectStateEntry in objectStateEntries)
            {
                objectStateEntry.State = EntityState.Detached;
            }
        }

        public virtual T Update(T entity, int key)
        {
            try
            {
                T existing = _dataContext.Set<T>().Find(key);
                if (existing != null)
                {
                    _dataContext.Entry(existing).CurrentValues.SetValues(entity);
                    _dataContext.SaveChanges();
                }
                return existing;
            }
            catch (Exception ex)
            {
                throw new Exception($"Возникла ошибка при попытке обновления сущности типа {entity.GetType().ToString()}.\nID = {key}\nСообщение об ошибке: {ex.Message}");
            }
        }


        public virtual void UpdateVoid(T entity, int key)
        {
            try
            {
                T existing = _dataContext.Set<T>().Find(key);
                if (existing != null)
                {
                    _dataContext.Entry(existing).CurrentValues.SetValues(entity);
                    _dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Возникла ошибка при попытке обновления сущности типа {entity.GetType().Name}.\nID = {key}\nСообщение об ошибке: {ex.Message}");
            }


        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            try
            {
                _dataContext.Set<T>().UpdateRange(entities);
                _dataContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception($"Возникла ошибка при попытке обновления массива сущностей.\nСообщение об ошибке: {ex.Message}");
            }


        }

        public void DeleteRow(T entity)
        {
            try
            {
                DetachEntities();

                _dataContext.Set<T>().Attach(entity);

                _dataContext.Entry(entity).State = EntityState.Deleted;

                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Возникла ошибка при попытке удаления строки типа {entity.GetType().Name}.\nСообщение об ошибке: {ex.Message}");
            }

        }


        public virtual void Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Возникла ошибка при попытке объекта типа {entity.GetType().Name}.\nСообщение об ошибке: {ex.Message}");
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)//TODO Добавление Exception
        {
            IEnumerable<T> objects = _dbSet.Where(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public virtual T GetById(decimal id)//TODO Добавление Exception
        {
            return _dbSet.Find(id);
        }

        public T Single(Expression<Func<T, bool>> match)//TODO Добавление Exception
        {
            return _dbSet.SingleOrDefault(match);
        }

        public T First(Expression<Func<T, bool>> match)//TODO Добавление Exception
        {
            return _dbSet.FirstOrDefault(match);
        }

        public bool Any(Expression<Func<T, bool>> filter)//TODO Добавление Exception
        {
            return _dbSet.Any(filter);
        }

        public virtual IQueryable<T> GetAll()//TODO Добавление Exception
        {
            return _dbSet;
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> where)//TODO Добавление Exception
        {
            return _dbSet.Where(where);
        }


        public IQueryable<T> GetAllWithPaging<TKey>(int pageSize, int page, out int total, Expression<Func<T, TKey>> orderBy, bool isOrderAsc, Expression<Func<T, bool>> filter = null, Expression<Func<T, bool>> additionalFilter = null)//TODO Добавление Exception
        {
            IQueryable<T> resultFromDb = _dbSet.Where(filter);

            total = resultFromDb.Count();

            IQueryable<T> resultForPaging = isOrderAsc ? resultFromDb.OrderBy(orderBy).Skip((page - 1) * pageSize).Take(pageSize) : resultFromDb.OrderByDescending(orderBy).Skip((page - 1) * pageSize).Take(pageSize);

            return resultForPaging;
        }


        public void Dispose()
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
                _dataContext = null;
            }
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator()//TODO Добавление Exception
        {
            return new ExpandableAsyncEnumerator<T>(this.GetAll().GetEnumerator());
        }

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAsyncEnumerator();
        }

        public void SetDetect(bool param)
        {
            _dataContext.ChangeTracker.AutoDetectChangesEnabled = param;
        }
    }

    public class ExpandableAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;
        private bool disposed = false;


        public ExpandableAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
            }

            disposed = true;
        }

        public ValueTask DisposeAsync()
        {
            try
            {
                Dispose();
                return default;
            }
            catch (Exception exception)
            {
                return new ValueTask(Task.FromException(exception));
            }
        }
        public System.Threading.Tasks.ValueTask<bool> MoveNextAsync()
        {
            return ValueTask.FromResult(_inner.MoveNext());
        }
        public T Current
        {
            get { return _inner.Current; }
        }
    }
}
