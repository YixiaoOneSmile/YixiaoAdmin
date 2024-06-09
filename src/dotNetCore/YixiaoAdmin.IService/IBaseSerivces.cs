using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YixiaoAdmin.IServices
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        Task<TEntity> QueryById(object id);
        Task<bool> Add(TEntity model);
        Task<bool> RemoveById(object id);
        Task<bool> Remove(TEntity model);
        Task<bool> Update(TEntity model);
        Task<IList<TEntity>> Query();
        Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
    }
}
