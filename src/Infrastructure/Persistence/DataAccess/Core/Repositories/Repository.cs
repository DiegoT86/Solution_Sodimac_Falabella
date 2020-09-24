using Dapper;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.Exception;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sodimac.Infrastructure.Persistence.DataAccess.Core.Repositories
{
    public class Repository
    {
        private readonly IDBManager dbManager;
        protected virtual string InsertStoreProcedure => "spInsert";
        protected virtual string UpdateStoreProcedure => "spUpdate";

        public Repository(IDBManager dbManager)
        {
            this.dbManager = dbManager;
        }

        protected T ExecuteScalar<T>(string storedProcedureName, DynamicParameters parameters)
        {
            CommandDefinition command = dbManager.GetStoredProcedureCommand(storedProcedureName, parameters, 600);
            return dbManager.ExecuteScalar<T>(command);
        }

        protected IEnumerable<T> Get<T>(string storedProcedureName, DynamicParameters parameters)
        {
            CommandDefinition command = dbManager.GetStoredProcedureCommand(storedProcedureName, parameters, 600);
            return dbManager.Get<T>(command);
        }

        protected T GetOne<T>(string storedProcedureName, DynamicParameters parameters)
        {
            CommandDefinition command = dbManager.GetStoredProcedureCommand(storedProcedureName, parameters, 600);
            return dbManager.GetOne<T>(command);
        }

        protected bool Exist(string storedProcedureName, DynamicParameters parameters)
        {
            int count = ExecuteScalar<int>(storedProcedureName, parameters);
            return (count != 0);
        }

        public void Insert(DynamicParameters parameters)
        {
            try
            {
                CommandDefinition command = dbManager.GetStoredProcedureCommand(InsertStoreProcedure, parameters, 600);
                dbManager.ExecuteNonQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DataBaseException(ex.Message, ex.Server);
            }

        }

        protected void Update(DynamicParameters parameters)
        {
            try
            {
                CommandDefinition command = dbManager.GetStoredProcedureCommand(UpdateStoreProcedure, parameters, 600);
                dbManager.ExecuteNonQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DataBaseException(ex.Message, ex.Server);
            }
        }

        protected void ExecuteNonQuery(string storedProcedureName, DynamicParameters parameters)
        {
            try
            {
                CommandDefinition command = dbManager.GetStoredProcedureCommand(storedProcedureName, parameters, 600);
                dbManager.ExecuteNonQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DataBaseException(ex.Message, ex.Server);
            }
        }

    }
}
