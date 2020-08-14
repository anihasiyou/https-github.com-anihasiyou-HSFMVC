using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSF.Models;
using HSF.IDAL;
using HSF.DBUtility;
using Dapper;
using DapperExtensions;

namespace HSF.DAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        #region CRUD
        /// <summary>
        /// 根据主键返回实体
        /// </summary>
        public T GetById(object Id)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.GetById<T>(Id);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Insert(T model)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.Insert<T>(model);
            }
        }
        /// <summary>
        /// 根据主键修改数据
        /// </summary>
        public int UpdateById(T model)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.Update<T>(model);
            }
        }
        /// <summary>
        /// 根据主键修改数据 修改指定字段
        /// </summary>
        public int UpdateById(T model, string updateFields)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.Update<T>(model, updateFields);
            }
        }
        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        public int DeleteById(object Id)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.Delete<T>(Id);
            }
        }
        /// <summary>
        /// 根据主键批量删除数据
        /// </summary>
        public int DeleteByIds(object Ids)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.DeleteByIds<T>(Ids);
            }
        }
        /// <summary>
        /// 根据条件删除
        /// </summary>
        public int DeleteByWhere(string where, object param)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.DeleteByWhere<T>(where, param);
            }
        }
        #endregion
        /// <summary>
        /// 获取分页数据
        /// </summary>
        public IEnumerable<T> GetByPage(SearchFilter filter, out int total)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var page= conn.GetPage<T>(filter.pageIndex, filter.pageSize, filter.where, filter.param, filter.returnFields, filter.orderBy, filter.transaction, filter.commandTimeout);
                total=(int)page.Total;
                return page.Data;
            }
        }
        /// <summary>
        /// 获取分页数据 联合查询
        /// </summary>
        public IEnumerable<T> GetByPageUnite(SearchFilter filter, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(1) FROM {0};", filter.where);
            sb.AppendFormat("select top {3} * from(select row_number() over(order by {4}CreateTime desc) as rownumber,{0} from {1}) temp_table where rownumber > (({2}-1)*{3})"
                , filter.returnFields, filter.where, filter.pageIndex, filter.pageSize, filter.prefix);
            using (var conn = SqlHelper.SqlConnection())
            {
                using (var reader = conn.QueryMultiple(sb.ToString(), filter.param, filter.transaction, filter.commandTimeout))
                {
                    total = reader.ReadFirst<int>();
                    return reader.Read<T>();
                }
            }
        }
        /// <summary>
        /// 返回整张表数据
        /// returnFields需要返回的列，用逗号隔开。默认null，返回所有列
        /// </summary>
        public IEnumerable<T> GetAll(string returnFields = null, string orderby = null)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.GetAll<T>(returnFields, orderby);
            }
        }
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        public IEnumerable<T> GetByWhere(string where = null, object param = null, string returnFields = null, string orderby = null)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.GetByWhere<T>(where, param, returnFields, orderby);
            }
        }
    }
}
