using Dapper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Sodimac.Infrastructure.Crosscutting.Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager
{
    public abstract class DBManager : IDBManager
    {
        private Database database;
        private IDbTransaction transaction;
        private int transactionsCounter;
        private const string NotFoundConnectionString = "ConnectionStringKey not found for ";

        public abstract string ConnectionStringKey { get; }

        public void OpenTransaction()
        {
            if (transaction == null)
            {
                DbConnection connection = GetDatabase().CreateConnection();
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                transaction = connection.BeginTransaction();
                transactionsCounter = 0;
            }

            transactionsCounter++;
        }

        public void Rollback()
        {
            if (transactionsCounter <= 0)
            {
                throw new System.Exception("There is no transaction");
            }

            if (transaction != null)
            {
                transaction.Rollback();
                transaction.Dispose();
                transaction = null;
            }

            transactionsCounter--;
        }

        public void Commit()
        {
            if (transaction == null || transactionsCounter <= 0)
            {
                throw new System.Exception("There is no transaction");
            }

            if (transaction != null && transactionsCounter == 1)
            {
                transaction.Commit();
                transaction.Dispose();
                transaction = null;
            }

            transactionsCounter--;
        }

        public CommandDefinition GetStoredProcedureCommand(string storedProcedureName, object parameters, int commandTimeout)
        {
            return new CommandDefinition(storedProcedureName, parameters, null, commandTimeout, CommandType.StoredProcedure);
        }

        public void ExecuteNonQuery(CommandDefinition command)
        {
            using (IDbConnection conn = GetDatabase().CreateConnection())
            {
                if (transaction != null)
                {
                    conn.Execute(command.CommandText, command.Parameters, transaction, command.CommandTimeout, command.CommandType);
                }
                else
                {
                    conn.Execute(command);
                }
            }
        }

        public IDataReader ExecuteReader(CommandDefinition command)
        {
            IDbConnection conn = GetDatabase().CreateConnection();
            return conn.ExecuteReader(command);
        }

        public T GetOne<T>(CommandDefinition command)
        {
            using (IDbConnection conn = GetDatabase().CreateConnection())
            {
                return conn.QueryFirstOrDefault<T>(command);
            }
        }

        public async Task<T> GetOneAsync<T>(CommandDefinition command)
        {
            using (IDbConnection conn = GetDatabase().CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<T>(command);
            }
        }

        public async Task<T> GetOneAsync<T>(string sql)
        {
            using (IDbConnection conn = GetDatabase().CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<T>(sql);
            }
        }

        public IEnumerable<T> Get<T>(CommandDefinition command)
        {
            using (IDbConnection conn = GetDatabase().CreateConnection())
            {
                return conn.Query<T>(command);
            }
        }

        public T ExecuteScalar<T>(CommandDefinition command)
        {
            using (IDbConnection conn = GetDatabase().CreateConnection())
            {
                return conn.ExecuteScalar<T>(command);
            }
        }

        public SqlMapper.GridReader QueryMultiple(CommandDefinition command)
        {
            IDbConnection conn = GetDatabase().CreateConnection();
            return conn.QueryMultiple(command);
        }

        public Database GetDatabase()
        {
            if (database != null)
            {
                return database;
            }

            if (database == null)
            {
                string strConnection = GetConnectionString();
                database = new SqlDatabase(strConnection);
            }

            return database;
        }

        private string GetConnectionString()
        {
            string strConnection = ConnectionStringHelper.GetConnectionString(ConnectionStringKey);

            if (!string.IsNullOrEmpty(strConnection))
            {
                return strConnection;
            }
            else
            {
                throw new KeyNotFoundException($"{NotFoundConnectionString} {ConnectionStringKey}");
            }
        }
    }
}