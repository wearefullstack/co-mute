using CoMute.Web.Data.DataAccess;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace CoMute.Web.Data
{
    public abstract class RepBase<T> : IRepBase<T> where T : class
    {
        protected AppDbContext _appDbContext;
        public RepBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(T entity)
        {
            
            _appDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> FindAll()
        {
            return _appDbContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _appDbContext.Set<T>().Where(expression);
        }

        public IEnumerable<T> FindByConditionAsce(Expression<Func<T, bool>> expression, string sortBy)
        {
            return _appDbContext.Set<T>().Where(expression)
                .OrderBy(x => x.GetType().GetProperty(sortBy));
        }

        public IEnumerable<T> FindByConditionDesc(Expression<Func<T, bool>> expression, string sortBy)
        {
            return _appDbContext.Set<T>().Where(expression)
                .OrderByDescending(x => x.GetType().GetProperty(sortBy));
        }

        public T GetById(int id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetWithOptions(QueryOptions<T> options)
        {
            IQueryable<T> query = _appDbContext.Set<T>();

            if (options.HasWhere)
                query = query.Where(options.Where);

            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }

            if (options.HasPaging)
            {
                query = query.Skip((options.PageNumber - 1) * options.PageSize)
                             .Take(options.PageSize);
            }
            
            return query.ToList();
        }

        public void Update(T entity, T oldEntity)
        {
            
            _appDbContext.Set<T>().Remove(oldEntity);
            _appDbContext.Set<T>().Add(entity);
        }
    }
}

//entity.GetType().GetProperty("name");