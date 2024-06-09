using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YixiaoAdmin.Common;
using YixiaoAdmin.EntityFrameworkCore;
using YixiaoAdmin.IRepository;
namespace YixiaoAdmin.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected YixiaoAdminContext db;

        public BaseRepository(YixiaoAdminContext yixiaoAdminContext)
        {
            db = yixiaoAdminContext;
        }

        public TEntity Find(object id)
        {
            return db.Set<TEntity>().Find(id);
        }
        public bool Add(TEntity model)
        {
            db.Set<TEntity>().Add(model);
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public bool AddList(List<TEntity> model)
        {
            db.Set<TEntity>().AddRange(model);
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public void AddListWithoutSave(List<TEntity> model)
        {
            db.Set<TEntity>().AddRange(model);

        }
        public bool Update(TEntity model)
        {
            var set = db.Set<TEntity>();
            set.Attach(model);
            foreach (System.Reflection.PropertyInfo p in model.GetType().GetProperties())
            {
                if (p.GetValue(model) != null && p.Name != "Id" )
                {
                    db.Entry<TEntity>(model).Property(p.Name).IsModified = true;
                }
            }
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public void UpdatetWithoutSave(TEntity model)
        {
            var set = db.Set<TEntity>();
            set.Attach(model);
            foreach (System.Reflection.PropertyInfo p in model.GetType().GetProperties())
            {
                if (p.GetValue(model) != null && p.Name != "Id")
                {
                    db.Entry<TEntity>(model).Property(p.Name).IsModified = true;
                }
            }
        }

        public bool Remove(TEntity model)
        {
            db.Set<TEntity>().Remove(model);
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public bool RemoveById(object id)
        {
            var entity = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(entity);
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<TEntity> FindAsync(object id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }
        public async Task<bool> AddAsync(TEntity model)
        {
            await db.Set<TEntity>().AddAsync(model);
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> AddListAsync(List<TEntity> model)
        {
            await db.Set<TEntity>().AddRangeAsync(model);
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateAsync(TEntity model)
        {
            await Task.Run(() => {
                var set = db.Set<TEntity>();
                set.Attach(model);
                foreach (System.Reflection.PropertyInfo p in model.GetType().GetProperties())
                {
                    if (p.PropertyType == typeof(DateTime))
                    {
                        continue;
                    }
                    if (p.GetValue(model) != null && p.Name != "Id" )
                    {
                        db.Entry<TEntity>(model).Property(p.Name).IsModified = true;
                    }
                }
            });
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> RemoveAsync(TEntity model)
        {
            var a = await Task.Run(() => db.Set<TEntity>().Remove(model));
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> RemoveByIdAsync(object id)
        {
            var entity = await Task.Run(() => db.Set<TEntity>().Find(id));
            await Task.Run(() => db.Set<TEntity>().Remove(entity));
            var result = db.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<IQueryable<TEntity>> Query()
        {
            return await Task.Run(() => db.Set<TEntity>());
        }
        public async Task<IQueryable<TEntity>> Query(string sql)
        {
            return await Task.Run(() => db.Set<TEntity>().FromSqlRaw(sql));
        }
        public async Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() => db.Set<TEntity>().Where(expression));
        }
        public  IQueryable<TEntity> QueryWithNoAsync(Expression<Func<TEntity, bool>> expression)
        {
            return db.Set<TEntity>().Where(expression);
        }
        public async Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, SortFieldModel[] orderByExpression)
        {
            var query = await Task.Run(() =>
            {
                //条件过滤
                var query = db.Set<TEntity>().Where(whereExpression);

                //创建表达式变量参数
                var parameter = Expression.Parameter(typeof(TEntity), "p");

                if (orderByExpression != null && orderByExpression.Length > 0)
                {
                    for (int i = 0; i < orderByExpression.Length; i++)
                    {
                        //根据属性名获取属性
                        var property = typeof(TEntity).GetProperty(orderByExpression[i].SortField);
                        //创建一个访问属性的表达式
                        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                        var orderByExp = Expression.Lambda(propertyAccess, parameter);

                        string OrderName = "";
                        if (i > 0)
                            OrderName = orderByExpression[i].IsDesc ? "ThenByDescending" : "ThenBy";
                        else
                            OrderName = orderByExpression[i].IsDesc ? "OrderByDescending" : "OrderBy";


                        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(TEntity), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));

                        query = query.Provider.CreateQuery<TEntity>(resultExp);
                    }
                }
                return query;
            });

            return query;
        }
        public async Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, SortFieldModel[] orderByExpression, int currentPage, int pageNumber)
        {

            var query = await Task.Run(() =>
            {
                //条件过滤
                var query = db.Set<TEntity>().Where(whereExpression);

                //创建表达式变量参数
                var parameter = Expression.Parameter(typeof(TEntity), "p");

                if (orderByExpression != null && orderByExpression.Length > 0)
                {
                    for (int i = 0; i < orderByExpression.Length; i++)
                    {
                        string[] sortFieldList = orderByExpression[i].SortField.Split(".");

                        //根据属性名获取属性
                        var property = typeof(TEntity).GetProperty(sortFieldList[0]);

                        ////创建一个访问属性的表达式
                        MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);



                        if (sortFieldList.Length > 1)
                        {
                            for (int j = 1; j < sortFieldList.Length; j++)
                            {
                                property = property.PropertyType.GetProperty(sortFieldList[j]);
                                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                            }
                        }

                        var orderByExp = Expression.Lambda(propertyAccess, parameter);

                        string OrderName = "";
                        if (i > 0)
                            OrderName = orderByExpression[i].IsDesc ? "ThenByDescending" : "ThenBy";
                        else
                            OrderName = orderByExpression[i].IsDesc ? "OrderByDescending" : "OrderBy";


                        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(TEntity), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));

                        query = query.Provider.CreateQuery<TEntity>(resultExp);
                    }
                }
                return query.Skip(currentPage * pageNumber).Take(pageNumber);
            });

            return query;



        }

        public IDbContextTransaction BeginTransaction()
        {
            return db.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}
