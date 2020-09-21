namespace Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager
{
    public class SodimacDBManager : DBManager, ISodimacDBManager
    {
        public override string ConnectionStringKey
        {
            get
            {
                return "DBSodimac";
            }
        }
    }
}
