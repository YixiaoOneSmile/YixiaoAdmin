using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YixiaoAdmin.Common;

namespace YixiaoAdmin.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        TEntity Find(object id);
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(object id);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(TEntity model);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddList(List<TEntity> model);

        /// <summary>
        /// 添加实体但不保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void AddListWithoutSave(List<TEntity> model);
        /// <summary>
        /// 异步添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity model);
        /// <summary>
        /// 异步添加实体列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddListAsync(List<TEntity> model);
        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        bool RemoveById(object id);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model">要删除的实体</param>
        /// <returns></returns>
        bool Remove(TEntity model);
        /// <summary>
        /// 异步根据主键删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<bool> RemoveByIdAsync(object id);
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="model">要删除的体</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(TEntity model);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        bool Update(TEntity model);
        /// <summary>
        /// 更新实体并不保存
        /// </summary>
        /// <param name="model"></param>
        void UpdatetWithoutSave(TEntity model);
        /// <summary>
        /// 异步更新实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity model);
        /// <summary>
        /// 查询并返回List类型的实体
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> Query();

        /// <summary>
        /// sql语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> Query(string sql);

        /// <summary>
        /// 按表达式搜索
        /// </summary>
        /// <param name="whereExpression">搜索表达式</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 按表达式同步搜索
        /// </summary>
        /// <param name="whereExpression">搜索表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryWithNoAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 排序查询
        /// </summary>
        /// <param name="whereExpression">搜索表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="desc">是否开启倒序，true为开启倒序</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, SortFieldModel[] orderByExpression);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">搜索表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="desc">是否开启倒序，true为开启倒序</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageNumber">每页显示条数</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, SortFieldModel[] orderByExpression,  int currentPage,int pageNumber);

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

    }
}
