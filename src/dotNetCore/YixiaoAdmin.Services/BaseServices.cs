/****************************************************
 * Services层基类
****************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YixiaoAdmin.IServices;
using YixiaoAdmin.IRepository;
using System.Linq;
using YixiaoAdmin.Common;
using System.Linq.Expressions;

namespace YixiaoAdmin.Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> baseRepository;

        public async Task<bool> Add(TEntity model)
        {
            InitModel.Init(model, "system", true);
            return await baseRepository.AddAsync(model);
        }

        public async Task<bool> Remove(TEntity model)
        {
            return await baseRepository.RemoveAsync(model);
        }

        public async Task<bool> RemoveById(object id)
        {
            return await baseRepository.RemoveByIdAsync(id);
        }

        public async Task<bool> Update(TEntity model)
        {
            return await baseRepository.UpdateAsync(model);
        }

        public async Task<IList<TEntity>> Query()
        {
            var list = await baseRepository.Query();
            return list.ToList() ;
        }

        public async Task<TEntity> QueryById(object id)
        {
            return await baseRepository.FindAsync(id);
        }

        public async Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseRepository.Query(whereExpression);
        }
    }
}

