using OrdenCompra.API.Domain.Contracts;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.Repositories;

namespace OrdenCompra.API.Infrastructure.Repositories
{
    public class OrdenCompraRepository : Repository, IOrdenCompraRepository
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbManager"></param>
        public OrdenCompraRepository(ISodimacDBManager dbManager) : base(dbManager)
        {

        }

        #endregion
    }
}
