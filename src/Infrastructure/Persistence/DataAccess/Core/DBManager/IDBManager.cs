using Dapper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager
{
    public interface IDBManager
    {
        T ExecuteScalar<T>(CommandDefinition command);

        IDataReader ExecuteReader(CommandDefinition command);

        void ExecuteNonQuery(CommandDefinition command);

        CommandDefinition GetStoredProcedureCommand(string storedProcedureName, object parameters, int commandTimeout);

        T GetOne<T>(CommandDefinition command);

        Task<T> GetOneAsync<T>(CommandDefinition command);

        Task<T> GetOneAsync<T>(string sql);

        IEnumerable<T> Get<T>(CommandDefinition command);

        void OpenTransaction();

        void Commit();

        void Rollback();

        Database GetDatabase();

        SqlMapper.GridReader QueryMultiple(CommandDefinition command);
    }
}
