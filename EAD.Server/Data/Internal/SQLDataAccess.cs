using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EAD.Server.Data.Internal
{
    public class SQLDataAccess : IDisposable, ISQLDataAccess
    {
        public SQLDataAccess(IConfiguration config, ILogger<SQLDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> LoadDataBySQLTEXT<T, U>(string sqlText, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlText,
                    commandType: CommandType.Text).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {

            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int SaveDataWithOutPut<T>(string storedProcedure, T parameters, string OutputParameterName, string connectionStringName)
        {

            var parametersNew = new DynamicParameters(parameters);
            int outputID;
            parametersNew.Add($"{OutputParameterName}", dbType: DbType.Int32, direction: ParameterDirection.Output);


            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parametersNew,
                    commandType: CommandType.StoredProcedure);

                outputID = parametersNew.Get<int>($"{OutputParameterName}");
            }
            return outputID;
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();

            isClosed = false;
        }
        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    List<T> rows = connection.Query<T>(storedProcedure, parameters,
                        commandType: CommandType.StoredProcedure).ToList();
                    return rows;
                }


                catch (SqlException ex)

                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }





        }
        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

            return rows;
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction);
        }
        public int SaveDataInTransactionWithOutPut<T>(string storedProcedure, T parameters, string OutputParameterName)
        {
            var parametersNew = new DynamicParameters(parameters);
            int outputID;

            parametersNew.Add($"{OutputParameterName}", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _connection.Execute(storedProcedure, parametersNew,
                    commandType: CommandType.StoredProcedure, transaction: _transaction);

            outputID = parametersNew.Get<int>($"{OutputParameterName}");

            return outputID;
        }
        private bool isClosed = false;
        private readonly IConfiguration _config;
        private readonly ILogger<SQLDataAccess> _logger;

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

            isClosed = true;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

            isClosed = true;
        }

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Commit transaction failed in the dispose method.");
                }
            }

            _transaction = null;
            _connection = null;
        }

        public void DBConnectionTest(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                }
                catch (SqlException sq)
                {
                    throw;
                }
            }


        }
    }
}
