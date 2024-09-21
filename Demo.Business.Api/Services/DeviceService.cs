using Demo.Toll.Class;
using System.Data;
using System.Data.SqlClient;

namespace Demo.Business.Api.Services
{

    public class DeviceService
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DeviceManagementDB;User ID=sa;Password=a1234567";
        /// <summary>
        /// 根据关键字获取Device内容
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeviceByName(string DeviceName)
        {
            string sql = "EXEC SearchDevices @SearchName";
            List<SqlParameter> sqlParameter = new List<SqlParameter>() {
            new SqlParameter("SearchName",DeviceName)
            };

            DbHelper dbHelper = new DbHelper(_connectionString);
            return dbHelper.GetDataToDataTable(sql, sqlParameter);


        }
    }
}
