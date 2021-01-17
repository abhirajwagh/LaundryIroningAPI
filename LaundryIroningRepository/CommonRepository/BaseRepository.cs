using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace LaundryIroningRepository.CommonRepository
{
    /// <summary>
    /// Base Repository class is a base class which provides basic Add, Update, Delete functionality to other repositories 
    /// Each CRUD operation should to maintain traice audit and for that User name details are important
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        

        #region Constructor
        public BaseRepository()
        {

        }
        #endregion


        public IUnitOfWork UnitOfWork { get; set; }


        #region public Methods
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await UnitOfWork.DataContext.Set<TEntity>().Where(predicate).CountAsync();
            else
                return await UnitOfWork.DataContext.Set<TEntity>().CountAsync();
        }

        /// <summary>
        /// Add new record synchronously in entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            UnitOfWork.DataContext.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Add new record in entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity)
        {
            await UnitOfWork.DataContext.Set<TEntity>().AddAsync(entity);
        }

        /// <summary>
        /// Synchronously modify single record from entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Update(TEntity entity)
        {
            UnitOfWork.DataContext.Set<TEntity>().Update(entity);
        }

        /// <summary>
        /// Modify single record from entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => UnitOfWork.DataContext.Set<TEntity>().Update(entity));
        }

        /// <summary>
        /// Delete single record from entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => UnitOfWork.DataContext.Set<TEntity>().Remove(entity));
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            UnitOfWork.DataContext.Set<TEntity>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await UnitOfWork.DataContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => UnitOfWork.DataContext.Set<TEntity>().UpdateRange(entities));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="selectPredicate"></param>
        /// <returns></returns>
        public virtual async Task<IList<TResult>> SelectAsyncList<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selectPredicate)
        {
            return await UnitOfWork.DataContext.Set<TEntity>().Where(predicate).Select(selectPredicate).ToListAsync();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IList<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await UnitOfWork.DataContext.Set<TEntity>().AsNoTracking().Where(predicate).ToArrayAsync();

                   return await UnitOfWork.DataContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Returns if there is any record in the repository for the entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await UnitOfWork.DataContext.Set<TEntity>().AsNoTracking().AnyAsync(predicate);

            return await UnitOfWork.DataContext.Set<TEntity>().AsNoTracking().AnyAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return UnitOfWork.DataContext.Set<TEntity>().AsNoTracking().Where(predicate).FirstOrDefault();

            return UnitOfWork.DataContext.Set<TEntity>().AsNoTracking().FirstOrDefault();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await UnitOfWork.DataContext.Set<TEntity>().AsNoTracking().Where(predicate).FirstOrDefaultAsync();

            return await UnitOfWork.DataContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync();
        }

        /// <summary>
        /// Delete single record from entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => UnitOfWork.DataContext.Set<TEntity>().RemoveRange(entities));
        }

    }

    #endregion
}
