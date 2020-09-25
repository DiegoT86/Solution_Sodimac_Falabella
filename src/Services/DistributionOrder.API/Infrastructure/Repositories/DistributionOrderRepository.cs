using DistributionOrder.API.Domain.Contracts;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.Repositories;

namespace DistributionOrder.API.Infrastructure.Repositories
{
    public class DistributionOrderRepository : Repository, IDistributionOrderRepository
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbManager"></param>
        public DistributionOrderRepository(ISodimacDBManager dbManager) : base(dbManager)
        {

        }

        #endregion
    }
}
