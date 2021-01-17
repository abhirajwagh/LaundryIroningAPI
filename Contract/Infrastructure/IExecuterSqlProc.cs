using LaundryIroningHelper;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LaundryIroningContract.Infrastructure
{
    /// <summary>
    /// Use this interface to execute stored procedures for complex data type
    /// </summary>
    /// <typeparam name="QueryEntity"></typeparam>
    public interface IExecuterSqlProc<ComplexEntity> where ComplexEntity : class
    {
        /// <summary>
        /// Execute stored procedures for complex data type
        /// </summary>
        /// <param name="query">Procedure Name</param>
        /// <param name="sqlParam"></param>
        /// <returns></returns>
        Task<List<ComplexEntity>> ExecuteProcedureAsync(string procName, List<Parameters> param = null);
        /// <summary>
        /// Execute procedure using reader on databse to get record
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <returns></returns>
        //Task<List<ComplexEntity>> ExecuteAsync(string procName);
        //Task<List<ComplexEntity>> ExecuteAsync(string procName, params object[] parameters);

        Task<int> ExceuteNonQueryAsync(string procName, IEnumerable<Parameters> Parameters);

    }
}
