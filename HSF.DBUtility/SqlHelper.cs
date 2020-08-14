using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HSF.DBUtility
{
    public sealed class SqlHelper
    {
        //数据库连接字符串
        private readonly static string ConnStr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private readonly static string ConnStrCARGOWEB = ConfigurationManager.ConnectionStrings["connStringCARGOWEB"].ConnectionString;


        public static SqlConnection SqlConnection()
        {
            var connection = new SqlConnection(ConnStr);
            connection.Open();
            return connection;
        }
        public static SqlConnection SqlConnectionCARGOWEB()
        {
            var connection = new SqlConnection(ConnStrCARGOWEB);
            connection.Open();
            return connection;
        }
        /// <summary>
        /// 返回所影响的条数
        /// </summary> 
        public static int ExecNonquery(string sql, SqlParameter[] parameter)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        if (parameter != null)
                        {
                            cmd.Parameters.AddRange(parameter);
                        }
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                //TODO：记录错误日志。略
                return -1;
            }
        }


        /// <summary>
        /// 返回首行首列
        /// </summary> 
        public static object ExecScalar(string sql, SqlParameter[] parameter)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        if (parameter != null)
                        {
                            cmd.Parameters.AddRange(parameter);
                        }
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                //TODO：记录错误日志。略
                return null;
            }
        }


        /// <summary>
        /// 返回结果集
        /// </summary> 
        public static DataSet ExecDataSet(string sql, SqlParameter[] parameter)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        if (parameter != null)
                        {
                            cmd.Parameters.AddRange(parameter);
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception e)
            {
                //TODO：记录错误日志。略
                return null;
            }
        }


        /// <summary>
        /// 返回一张表
        /// </summary> 
        public static DataTable ExecDataTable(string sql, SqlParameter[] parameter)
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(ConnStr))
            //    {
            //        conn.Open();
            //        using (SqlCommand cmd = conn.CreateCommand())
            //        {
            //            cmd.CommandText = sql;
            //            if (parameter != null)
            //            {
            //                cmd.Parameters.AddRange(parameter);
            //            }
            //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //            //DataSet ds = new DataSet();
            //            //adapter.Fill(ds);
            //            DataTable dt = new DataTable();
            //            adapter.Fill(dt);
            //            return dt;
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    //TODO：记录错误日志。略
            //    return null;
            //}

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, ConnStr);
                da.SelectCommand.Parameters.AddRange(parameter);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                //TODO：记录错误日志。略
                return null;
            }
        }

        /// <summary>
        /// 3.0 执行存储过程的方法
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="ps">参数数组</param>
        /// <returns></returns>
        public static int ExcuteProc(string procName, params SqlParameter[] parameter)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(procName, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (parameter != null)
                        {
                            command.Parameters.AddRange(parameter);
                        }
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                //TODO：记录错误日志。略
                return -1;
            }
        }


        /// <summary>
        /// 分页存储过程
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="FieldName">字段名</param>
        /// <param name="wheres">where条件</param>
        /// <param name="order">只能是desc or asc</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="TotalCount">总页码</param>
        /// <param name="PageIdORField">指定字段来分页</param>
        /// <param name="OrderField">排序指定的字段</param>
        public static DataSet LinkProce(string TableName, string FieldName, string wheres, string order, string PageIdORField, string OrderField, ref int PageSize, ref int PageIndex)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_PagingTabs";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TableName", TableName);
                    cmd.Parameters.AddWithValue("@FieldName", FieldName);
                    cmd.Parameters.AddWithValue("@where", wheres);
                    cmd.Parameters.AddWithValue("@Order", order);
                    cmd.Parameters.AddWithValue("@OrderField", OrderField);
                    cmd.Parameters.AddWithValue("@PageIdORField", PageIdORField);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.AddWithValue("@PageIndex", PageIndex);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        return ds;
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        ds.Dispose();
                        conn.Close();
                    }
                }
            }
        }
    }
}
