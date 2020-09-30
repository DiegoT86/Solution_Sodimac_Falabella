using ASNVerify.API.Domain.Contracts;
using Dapper;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.Repositories;

namespace ASNVerify.API.Infrastucture.Repositories
{
    public class ASNVerifyRepository: Repository,  IASNVerifyRepository
    {
        private readonly ISodimacDBManager _dbManager;

        #region Constructor
        /// <summary>
        /// AsnVerifyRepository Constructor
        /// </summary>
        /// <param name="dbManager"></param>
        public ASNVerifyRepository(ISodimacDBManager dbManager) : base(dbManager)
        {
            _dbManager = dbManager;
        }

        #endregion

        public ASNVerify.API.Domain.Entities.ASNVerify GetById(int id)
        {            
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id, System.Data.DbType.Int32);
            
            var cmd = _dbManager.GetStoredProcedureCommand("dbo.ASNVerify_GetById", parameters, 30);

            return _dbManager.GetOne<ASNVerify.API.Domain.Entities.ASNVerify>(cmd);
        }
    }
}
