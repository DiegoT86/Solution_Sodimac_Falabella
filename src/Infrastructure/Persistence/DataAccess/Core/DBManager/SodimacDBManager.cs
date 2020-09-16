using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core.DBManager
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
