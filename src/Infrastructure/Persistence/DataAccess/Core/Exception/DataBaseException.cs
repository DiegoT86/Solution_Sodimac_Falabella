using System;
using System.Data.Common;

namespace Sodimac.Infrastructure.Persistence.DataAccess.Core.Exception
{
    public class DataBaseException : DbException
    {
        public DateTime TimeStamp { get; protected set; }

        public Guid MessageId { get; protected set; }

        public override string Message { get; }

        public string ServerName { get; protected set; }

        public DataBaseException(string message, string serverName)
        {
            this.TimeStamp = DateTime.UtcNow;
            this.ServerName = serverName;
            this.MessageId = Guid.NewGuid();
            this.Message = message;

        }
    }
}
