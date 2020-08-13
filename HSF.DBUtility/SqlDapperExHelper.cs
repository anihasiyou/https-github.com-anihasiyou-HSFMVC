using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DapperExtensions;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace HSF.DBUtility
{
    //https://github.com/znyet/DapperExtensions
    public class SqlDapperExHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public static T GetById<T>(object primaryId) where T : class
        {
            using (var conn = new SqlConnection(connectionString)) //IDbConnection (sqlserver、mysql、oracle、postgresql、sqlite)
            {
                return conn.GetById<T>(primaryId);
            }
        }

        public static int Insert<T>(T model) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Insert<T>(model);
            }
        }
        public static int UpdateById<T>(T model) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Update<T>(model);
            }
        }
        public static int UpdateById<T>(T model, string updateFields) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Update<T>(model, updateFields);
            }
        }
        public static int DeleteById<T>(object Id) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Delete<T>(Id);
            }
        }
        public static int DeleteByIds<T>(object Id) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.DeleteByIds<T>(Id);
            }
        }
        public static int DeleteByWhere<T>(string where,object param) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.DeleteByWhere<T>(where, param);
            }
        }
        public static IEnumerable<T> GetByPage<T>(int pageIndex, int pageSize, out int total, string returnFields, string where, object param
            , string orderBy, IDbTransaction tran, int? commandTimeout) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var page= conn.GetPage<T>(pageIndex, pageSize, where,param,returnFields,orderBy,tran,commandTimeout);
                total = (int)page.Total;
                return page.Data;
            }
        }
        public static IEnumerable<T> GetByPageUnite<T>(string prefix, int pageIndex, int pageSize, out int total, string returnFields = null, string where = null, object param = null, string orderBy = null, IDbTransaction transaction = null, int? commandTimeout = null)
             where T : class
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(1) FROM {0};", where);
            sb.AppendFormat("select top {3} * from(select row_number() over(order by {4}CreateTime desc) as rownumber,{0} from {1}) temp_table where rownumber > (({2}-1)*{3})", returnFields, where, pageIndex, pageSize, prefix);
            using (var conn = new SqlConnection(connectionString))
            {
                using (var reader = conn.QueryMultiple(sb.ToString(), param, transaction, commandTimeout))
                {
                    total = reader.ReadFirst<int>();
                    return reader.Read<T>();
                }
            }
        }
        public static IEnumerable<T> GetAll<T>(string returnFields = null, string orderby = null) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.GetAll<T>(returnFields, orderby);
            }
        }
        public static IEnumerable<T> GetByWhere<T>(string where = null, object param = null, string returnFields = null, string orderby = null) where T : class
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.GetByWhere<T>(where, param, returnFields, orderby);
            }
        }

        //public static IDbConnection GetConn()
        //{
        //    return new SqlConnection(connectionString);
        //    //return new MySqlConnection("server=127.0.0.1;uid=root;pwd=123456;database=test;charset=utf8");
        //    //return new SQLiteConnection(@"Data Source=C:\Users\Administrator\Desktop\1.db;Pooling=true;FailIfMissing=false");
        //    //return new NpgsqlConnection("server=127.0.0.1;uid=postgres;pwd=123456;database=test");
        //    //return new OracleConnection("User ID=test;Password=123456;Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = XE)))");
        //}
    }    
}
