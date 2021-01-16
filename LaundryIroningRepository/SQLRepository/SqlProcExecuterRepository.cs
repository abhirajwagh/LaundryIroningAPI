using LaundryIroningContract.Infrastructure;
using LaundryIroningEntity.Contract;
using LaundryIroningHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace LaundryIroningRepository.SQLRepository
{
    /// <summary>
    /// Use SqlProcExecuterRepository class to execute stored procedures which return complex types
    /// </summary>
    /// <typeparam name="QueryEntity">This will be a complex type provided by business</typeparam>
    public class SqlProcExecuterRepository : IExecuterStoreProc
    {

        #region Constructor

        public SqlProcExecuterRepository(
            DbContext context,
            IUnitOfWork _uow
        )
        {
            dbContext = context;
            uow = _uow;
        }
       
        #endregion
        public IUnitOfWork uow { get; set; }

        public DbContext dbContext { get; set; }

        #region Public Methods
        /// <summary>
        /// Execute stored procedure on complex data type to get record
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> ExecuteProcedureAsync<TEntity>(string procName, IEnumerable<Parameters> param) where TEntity : class
        {
            if (param != null)
            {
                List<SqlParameter> sqlParam = GetParameter(ref procName, param);
                return await dbContext.Set<TEntity>().FromSqlRaw(procName, sqlParam.ToArray()).ToListAsync();
            }
            return await dbContext.Set<TEntity>().FromSqlRaw(procName).ToListAsync();
        }

        /// <summary>
        /// Execute stored procedure on complex data type to get record
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<TEntity> ExecuteProcedure<TEntity>(string procName, IEnumerable<Parameters> param = null) where TEntity : class
        {
            if (param != null)
            {
                List<SqlParameter> sqlParam = GetParameter(ref procName, param);
                return dbContext.Set<TEntity>().FromSqlRaw(procName, sqlParam.ToArray()).ToList();
            }
            return dbContext.Set<TEntity>().FromSqlRaw(procName).ToList();
        }

        /// <summary>
        /// Execute stored procedure on complex data type to get record without adding complex object in DBContext class
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> ExecuteProcAsync<TEntity>(string procName, IEnumerable<Parameters> param = null) where TEntity : class
        {
            string procedureName = procName;

            //if (param != null)
            //{
            List<SqlParameter> sqlParam = GetParameter(ref procName, param);
            //return await Task.Run(() =>
            //{

            using (var command = uow.DataContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandTimeout = 0;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParam.ToArray());               

                if (command.Connection.State != ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                //await uow.DataContext.Database.OpenConnectionAsync();
                try
                {
                    using (var result = await command.ExecuteReaderAsync())
                    {
                        List<TEntity> list = new List<TEntity>();
                        TEntity obj = default(TEntity);
                        while (await result.ReadAsync())
                        {
                            obj = Activator.CreateInstance<TEntity>();
                            foreach (PropertyInfo prop in obj.GetType().GetProperties())
                            {
                                try
                                {
                                    result.GetOrdinal(prop.Name); // throws error if column is not present in stored procedure
                                    if (!object.Equals(result[prop.Name], DBNull.Value))
                                    {
                                        prop.SetValue(obj, result[prop.Name], null);
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            list.Add(obj);
                        }

                        return list;
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
            //    }
            //);
            //}
            //return await uow.DataContext.Set<TEntity>().FromSql(procName).ToListAsync();
        }

        /// <summary>
        /// Execute procedure using reader on databse to get record
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> ExecuteAsync<TEntity>(string query) where TEntity : class
        {
            //return Task.Run(() =>
            //{
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                await dbContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    List<TEntity> list = new List<TEntity>();
                    TEntity obj = default(TEntity);
                    while (await result.ReadAsync())
                    {
                        obj = Activator.CreateInstance<TEntity>();
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            if (!object.Equals(result[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(obj, result[prop.Name], null);
                            }
                        }
                        list.Add(obj);
                    }
                    return list;
                }
            }
            //}
            // );
        }

        public T ExecuteScalerProcedure<T>(string procname, IEnumerable<Parameters> param = null)
        {
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procname;
                command.CommandType = CommandType.StoredProcedure;
                dbContext.Database.OpenConnection();

                if (param != null)
                {
                    foreach (var Parameter in param)
                    {
                        DbParameter parameter = command.CreateParameter();

                        parameter.ParameterName = "@" + Parameter.ParamKey.ToString();
                        parameter.Value = Parameter.Value;
                        command.Parameters.Add(parameter);

                    }
                }

                var result = command.ExecuteScalar();
                switch ($"{result.GetType().Name}:{typeof(T).Name}")
                {
                    case "Boolean:int":
                    case "Boolean:Int32":
                        return (T)(object)Convert.ToInt32(result);
                    default:
                        throw new NotSupportedException($"Return type {result.GetType().Name}:{typeof(T).Name} is not implemented");
                }
            }

        }

        public async Task<string> ExecuteScalerProcedureAsync(string procName, IEnumerable<Parameters> param = null)
        {
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procName;
                command.CommandType = CommandType.StoredProcedure;
                await dbContext.Database.OpenConnectionAsync();

                if (param != null)
                {
                    foreach (var Parameter in param)
                    {
                        DbParameter parameter = command.CreateParameter();

                        parameter.ParameterName = "@" + Parameter.ParamKey.ToString();
                        parameter.Value = Parameter.Value;
                        command.Parameters.Add(parameter);

                    }
                }

                var result = await command.ExecuteScalarAsync();
                return Convert.ToString(result);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// To get Sql Specific parameters
        /// </summary>
        /// <param name="procName">procName</param>
        /// <param name="param">param</param>
        /// <returns>Returns List of type SqlParameter </returns>
        private List<SqlParameter> GetParameter(ref string procName, IEnumerable<Parameters> param)
        {
            StringBuilder procedureName = new StringBuilder(procName);
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            if (param != null)
            {
                foreach (Parameters p in param)
                {
                    sqlParam.Add(new SqlParameter() { ParameterName = "@" + p.ParamKey, Value = p.Value, Direction = p.Direction == ParamDirection.Output ? ParameterDirection.Output : ParameterDirection.Input });
                    procedureName.Append(" @" + p.ParamKey);
                }
            }
            procName = procedureName.ToString();
            return sqlParam;
        }

        /// <summary>
        /// Execute stored procedure on complex data type to get record to CUD operations
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public int ExceuteNonQuery(string procName, IEnumerable<Parameters> Parameters = null)
        {

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procName;
                command.CommandType = CommandType.StoredProcedure;
                dbContext.Database.OpenConnection();
                if (Parameters != null)
                {
                    foreach (var Parameter in Parameters)
                    {
                        DbParameter parameter = command.CreateParameter();

                        parameter.ParameterName = "@" + Parameter.ParamKey.ToString();
                        parameter.Value = Parameter.Value;
                        command.Parameters.Add(parameter);

                    }
                }
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Execute stored procedure on complex data type to get record to CUD operations
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public async Task<int> ExceuteNonQueryAsync(string procName, IEnumerable<Parameters> Parameters = null)
        {

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procName;
                command.CommandType = CommandType.StoredProcedure;
                await dbContext.Database.OpenConnectionAsync();
                if (Parameters != null)
                {
                    foreach (var Parameter in Parameters)
                    {
                        DbParameter parameter = command.CreateParameter();

                        parameter.ParameterName = "@" + Parameter.ParamKey.ToString();
                        parameter.Value = Parameter.Value;
                        command.Parameters.Add(parameter);

                    }
                }
                return await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Execute Insert update delete  stored procedure and get all out put parameter set in passed entity property.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="procName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public async Task<TEntity> ExceuteNonQueryEntityResponseAsync<TEntity>(string procName, IEnumerable<Parameters> Parameters = null)
        {
            TEntity obj = default(TEntity);
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procName;
                command.CommandType = CommandType.StoredProcedure;
                await dbContext.Database.OpenConnectionAsync();
                if (Parameters != null)
                {
                    List<SqlParameter> sqlParam = GetParameter(ref procName, Parameters);
                    command.Parameters.AddRange(sqlParam.ToArray());
                }
                int retValue = await command.ExecuteNonQueryAsync();
                if (retValue > 0)
                {
                    obj = Activator.CreateInstance<TEntity>();
                    foreach (var Parameter in Parameters.Where(a=>a.Direction.Equals(ParamDirection.Output)))
                    {
                        this.TrySetProperty(obj, Parameter.ParamKey, command.Parameters["@" + Parameter.ParamKey].Value);
                    }
                }
                // To set properties of base respone object - Do it generic later if more properties added to base respone object
                this.TrySetProperty(obj, "Status", retValue > 0);
                return obj;
            }
        }

        #endregion

        private void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                prop.SetValue(obj, value, null);
        }

    }
}
