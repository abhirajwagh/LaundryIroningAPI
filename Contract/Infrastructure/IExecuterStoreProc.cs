using System.Collections.Generic;
using System.Threading.Tasks;
using LaundryIroningEntity.Contract;
using LaundryIroningHelper;
using Microsoft.EntityFrameworkCore;

namespace LaundryIroningContract.Infrastructure
{
    /// <summary>
    /// Use this interface to execute stored procedures for complex data type
    /// </summary>
    /// <typeparam name="QueryEntity"></typeparam>
    public interface IExecuterStoreProc
    {
        IUnitOfWork uow { get; set; }

        DbContext dbContext { get; set; }
        /// <summary>
        /// Execute stored procedures for complex data type
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <param name="Param"></param>
        /// <returns></returns>
        List<TEntity> ExecuteProcedure<TEntity>(string procName, IEnumerable<Parameters> param = null) where TEntity : class;

        /// <summary>
        /// Execute stored procedures Async for complex data type
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <param name="Param"></param>
        /// <returns></returns>
        Task<List<TEntity>> ExecuteProcedureAsync<TEntity>(string procName, IEnumerable<Parameters> param = null) where TEntity : class;


        /// <summary>
        /// Execute stored procedure on complex data type to get record without adding complex object in DBContext class
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<TEntity>> ExecuteProcAsync<TEntity>(string procName, IEnumerable<Parameters> param = null) where TEntity : class;

        /// <summary>
        /// Execute procedure using reader on databse to get record
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <returns></returns>
        Task<List<TEntity>> ExecuteAsync<TEntity>(string procName) where TEntity : class;

        /// <summary>
        /// Execute procedure using perform CUD operations
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <returns></returns>
      
        Task<int> ExceuteNonQueryAsync(string procName, IEnumerable<Parameters> Parameters);

        Task<TEntity> ExceuteNonQueryEntityResponseAsync<TEntity>(string procName, IEnumerable<Parameters> Parameters);
        Task<string> ExecuteScalerProcedureAsync(string procName, IEnumerable<Parameters> param = null);
        int ExceuteNonQuery(string procName, IEnumerable<Parameters> Parameters = null);
        T ExecuteScalerProcedure<T>(string procname, IEnumerable<Parameters> param = null);
    }
}
