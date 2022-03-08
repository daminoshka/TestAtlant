using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        T AddWithReturn(T entity);
        T Update(T entity, int key);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        void DeleteRow(T entity);
        T GetById(decimal id);
        T Single(Expression<Func<T, bool>> match);
        T First(Expression<Func<T, bool>> match);
        bool Any(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> where);
        void UpdateVoid(T entity, int key);
        void UpdateRange(IEnumerable<T> entities);
        void SetDetect(bool param);

        IQueryable<T> GetAllWithPaging<TKey>(int pageSize, int page, out int total, Expression<Func<T, TKey>> orderBy,
            bool isOrderAsc, Expression<Func<T, bool>> filter = null, Expression<Func<T, bool>> additionalFilter = null);
    }
}
