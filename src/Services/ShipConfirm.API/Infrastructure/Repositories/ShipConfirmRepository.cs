using ShipConfirm.API.Domain.Contracts;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.Repositories;

namespace ShipConfirm.API.Infrastructure.Repositories
{
    public class ShipConfirmRepository : Repository, IShipConfirmRepository
    {
        #region Contructor 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbManager"></param>
        public ShipConfirmRepository(ISodimacDBManager dbManager) : base(dbManager)
        {

        }

        #endregion
    }
}
