using CoMute.Web.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Data
{
    public interface IRepBase<T>
    {
        T GetById(int id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindByConditionAsce(Expression<Func<T, bool>> expression, string sortBy);
        IEnumerable<T> FindByConditionDesc(Expression<Func<T, bool>> expression, string sortBy);
        void Create(T entity);
        void Update(T entity, T oldEntity);
        void Delete(T entity);
        IEnumerable<T> GetWithOptions(QueryOptions<T> options);
    }
}
