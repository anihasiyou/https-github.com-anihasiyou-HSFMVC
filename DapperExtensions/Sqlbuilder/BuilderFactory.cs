using System;
using System.Data;

namespace DapperExtensions
{
    internal class BuilderFactory
    {
        private static readonly ISqlBuilder Sqlserver = new SqlServerBuilder();
        

        public static ISqlBuilder GetBuilder(IDbConnection conn)
        {
            string dbType = conn.GetType().Name;
            if (dbType.Equals("SqlConnection"))
            {
                return Sqlserver;
            }
            else
            {
                throw new Exception("Unknown DbType:" + dbType);
            }
        }

    }
}
