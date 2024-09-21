using Demo.Entity.Class;
using Demo.Toll.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Demo.Business.Api.Services
{
    public class SupplierService
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DeviceManagementDB;User ID=sa;Password=a1234567";

        public DataTable GetSupplierNames()
        {
            string sql = "SELECT SupplierName FROM Supplier";

            DbHelper dbHelper = new DbHelper(_connectionString);

            return 
                dbHelper.GetDataToDataTable(sql);

        }
    }


}
