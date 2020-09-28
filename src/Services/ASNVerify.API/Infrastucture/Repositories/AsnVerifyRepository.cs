using ASNVerify.API.Domain.Contracts;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.Repositories;

namespace ASNVerify.API.Infrastucture.Repositories
{
    public class ASNVerifyRepository: Repository,  IASNVerifyRepository
    {

        #region Constructor
        /// <summary>
        /// AsnVerifyRepository Constructor
        /// </summary>
        /// <param name="dbManager"></param>
        public ASNVerifyRepository(ISodimacDBManager dbManager) : base(dbManager)
        {
        }

        #endregion

    }
}
