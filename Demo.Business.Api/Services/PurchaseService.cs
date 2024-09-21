

using Demo.Entity.Class;
using Demo.Toll.Class;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Demo.Business.Api.Services
{
    public class PurchaseService
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DeviceManagementDB;User ID=sa;Password=a1234567";


        public bool AddPurchaseData(Purchase_Store store, List<Purchase_Store_Info> info)
        {
            //声明DataTable，并添加能够接收list相应数据类型的列
            DataTable purchaseDt = new DataTable();
            purchaseDt.Columns.Add("DeviceName", typeof(string));

            purchaseDt.Columns.Add("UnitPrice", typeof(decimal));
            purchaseDt.Columns.Add("Quantity", typeof(int));
            purchaseDt.Columns.Add("Note", typeof(string));
            //遍历List 并将数据添加给DataTable
            foreach (var item in info)
            {
                var row = purchaseDt.NewRow();
                row["DeviceName"] = item.DeviceName;
                row["Quantity"] = item.Quantity;
                row["UnitPrice"] = item.UniTPrice;
                row["Note"] = item.Note;
                purchaseDt.Rows.Add(row);
            }

            string sql = "EXEC AddPurchaseRecords_Store @StoreName,@SupplierName,@PurchaseInfoTable";

            // 创建参数列表
            List<SqlParameter> sqlParameter = new List<SqlParameter>()
            {

                new SqlParameter("@StoreName",store.StoreName),
                new SqlParameter("@SupplierName",store.SupplierName),
                new SqlParameter
                {
                    ParameterName = "@PurchaseInfoTable",
                    Value = purchaseDt,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.PurchaseRecords_Store_Info_Type"
                }
            };

            DbHelper dbHelper = new DbHelper(_connectionString);
            //返回操作结果
            return dbHelper.SaveModifyDeleteData(sql, sqlParameter);
            //dbHelper.SaveModifyDeleteData(sql);
        }
        public DataTable GetPurchaseInfo(long PurchaseID)
        {
            string sql = "SELECT d.DeviceName,d.UnitPrice,p.Quantity,p.Note FROM PurchaseRecords_Store_Info p JOIN DeviceType d ON d.DeviceID=p.DeviceID WHERE p.PurchaseID = @PurchaseID GROUP BY d.DeviceName,d.DeviceID,d.UnitPrice,p.Quantity,p.Note,p.PurchaseID ORDER BY p.PurchaseID;";
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter("@PurchaseID", PurchaseID)
            };

            DbHelper dbHelper = new DbHelper(_connectionString);
            return dbHelper.GetDataToDataTable(sql, list);


        }
        public DataTable GetStorePurchaseRecorder()
        {
            string sql = "SELECT p.PurchaseID,s.StoreName,sp.SupplierName,p.TotalPrice,p.Note,CONVERT(varchar, CONVERT(date,p.PurchaseDate), 23) AS PurchaseDate FROM PurchaseRecords_Store p JOIN  Store s ON p.StoreID = s.StoreID JOIN  Supplier sp ON p.SupplierID = sp.SupplierID JOIN PurchaseRecords_Store_Info pi ON p.PurchaseID = pi.PurchaseID JOIN  DeviceType d ON pi.DeviceID = d.DeviceID GROUP BY p.TotalPrice, p.PurchaseID, s.StoreName, sp.SupplierName,p.PurchaseDate,p.Note ORDER BY p.PurchaseID;";
            DbHelper dbHelper = new DbHelper(_connectionString);
            //返回查询数据
            return dbHelper.GetDataToDataTable(sql);

        }
        public DataTable GetStorePurchaseRecorderByParm(string StoreName, string DeviceName, string StartDate, string EndDate)
        {

            StringBuilder sql = new StringBuilder();
            bool firstAppendpParmResult = false;
            sql.Append("EXEC GetPurchaseRecordsByParm ");
            DbHelper dbHelperdb = new DbHelper(_connectionString);
            List<SqlParameter> sqlParameters = new List<SqlParameter>() {};
            if (!string.IsNullOrEmpty(StoreName))
            {
                sqlParameters.Add(new SqlParameter("@StoreName", StoreName));
                sql.Append("@StoreName=@StoreName");
                firstAppendpParmResult = true;
            }
            if (!string.IsNullOrEmpty(DeviceName))
            {
                sqlParameters.Add(new SqlParameter("@DeviceName", DeviceName));

                if (firstAppendpParmResult)

                {
                    sql.Append(",@DeviceName=@DeviceName");
                }
                else
                {
                    sql.Append("@DeviceName=@DeviceName");
                    firstAppendpParmResult = true;
                }
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
              if (firstAppendpParmResult)
              {
                  sql.Append(",@StartDate =@StartDate,@EndDate=@EndDate");
              }
              else
              {

                  sql.Append("@StartDate =@StartDate,@EndDate=@EndDate");
              }
                sqlParameters.Add(new SqlParameter("@StartDate", StartDate));
                sqlParameters.Add(new SqlParameter("@EndDate", EndDate));
            }
            return dbHelperdb.GetDataToDataTable(sql.ToString(), sqlParameters);
        }

        public DataTable GetStorePurchaseRecorderListByParm(string StoreName, string DeviceName, string StartDate, string EndDate)
        {

            StringBuilder sql = new StringBuilder();
            bool firstAppendpParmResult = false;
            sql.Append("EXEC GetStoreRecoderListByParm ");
            DbHelper dbHelperdb = new DbHelper(_connectionString);
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { };
            if (!string.IsNullOrEmpty(StoreName))
            {
                sqlParameters.Add(new SqlParameter("@StoreName", StoreName));
                sql.Append("@StoreName=@StoreName");
                firstAppendpParmResult = true;
            }
            if (!string.IsNullOrEmpty(DeviceName))
            {
                sqlParameters.Add(new SqlParameter("@DeviceName", DeviceName));

                if (firstAppendpParmResult)

                {
                    sql.Append(",@DeviceName=@DeviceName");
                }
                else
                {
                    sql.Append("@DeviceName=@DeviceName");
                    firstAppendpParmResult = true;
                }
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                if (firstAppendpParmResult)
                {
                    sql.Append(",@StartDate =@StartDate,@EndDate=@EndDate");
                }
                else
                {

                    sql.Append("@StartDate =@StartDate,@EndDate=@EndDate");
                }
                sqlParameters.Add(new SqlParameter("@StartDate", StartDate));
                sqlParameters.Add(new SqlParameter("@EndDate", EndDate));
            }
            return dbHelperdb.GetDataToDataTable(sql.ToString(), sqlParameters);
        }
    }
}





