using Demo.Entity.Class;
using Demo.Toll.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Demo.Business.Api.Services
{
    public class StoreService
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DeviceManagementDB;User ID=sa;Password=a1234567";
        //根据关键字
        public DataTable GetStoreName(string queryString)
        {
            string sql = "EXEC SearchStores @SearchName";
            List<SqlParameter> sqlParameter = new List<SqlParameter>()
            {
             new SqlParameter("@SearchName",queryString)
            };
            //_DbHelper.

            DbHelper dbHelper = new DbHelper(_connectionString);
            return dbHelper.GetDataToDataTable(sql, sqlParameter);
        }

        /// <summary>
        /// 新增门店名
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        public bool AddStore(string storeName)
        {
            //将storeName参数化
            SqlParameter sqlParameter = new SqlParameter()
            {
                ParameterName = "StoreName",
                Value = storeName

            };

            return false;
        }
        /// <summary>
        /// 通过name查询是否存在，如果存在，返回ID
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        public int GetStoreIDByName(string storeName)
        {

            string sql = "Select StoreId from Store where StoreName=@StoreName";
            //将storeName参数化
            List<SqlParameter> sqlParameter = new List<SqlParameter>()
            {
              new SqlParameter("StoreName",storeName)
            };

            DbHelper dbHelper = new DbHelper(_connectionString);

            return Convert.ToInt32(dbHelper.ExecuteScalar(sql, sqlParameter));
        }
    }
}
