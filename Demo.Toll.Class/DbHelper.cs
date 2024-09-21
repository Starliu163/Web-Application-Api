using System.Data;
using System.Data.SqlClient;

namespace Demo.Toll.Class
{
    public class DbHelper
    {
        #region Member
        private string _connStr = string.Empty;
        private SqlConnection _sqlCon = null;
        #endregion

        /// <summary>
        /// Instantiating Sqlconnection object
        /// </summary>
        public string ConnStr
        {
            get
            {
                return _connStr;
            }
            set
            {
                _connStr = value;
                _sqlCon = new SqlConnection(value);
            }
        }

        /// <summary>
        /// Read-only this._sqlCon
        /// </summary>
        public SqlConnection SqlCon
        {
            get
            {
                return _sqlCon;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbHelper"/> class.
        /// </summary>
        /// <param name="connStr">Connection databse string</param>
        public DbHelper(string connStr)
        {
            _connStr = connStr;
            _sqlCon = new SqlConnection(connStr);
        }

        /// <summary>
        /// Open DataBase Connection
        /// </summary>
        public void OpenDbConnection()
        {
            //If the connection object is null
            if (_sqlCon == null)
            {
                _sqlCon = new SqlConnection(_connStr);
                _sqlCon.Open();
            }
            //If the connectionSate is close
            else if (_sqlCon.State == ConnectionState.Closed)
            {
                _sqlCon.Open();
            }
            //If the connectionSate is broken
            else if (_sqlCon.State == ConnectionState.Broken)
            {
                _sqlCon.Close();
                _sqlCon.Open();
            }
        }

        /// <summary>
        /// Close DataBase Connection
        /// </summary>
        public void CloseDbConnection()
        {
            if (_sqlCon != null)
            {
                if (_sqlCon.State != ConnectionState.Closed)
                {
                    _sqlCon.Close();
                }
            }

        }

        /// <summary>
        /// Save, modify, delete data
        /// </summary>
        /// <param name="sql">Sql command statement</param>
        /// <param name="sqlParameterList">Provide arguments for sqlcommand</param>
        /// <returns>Returns true if the opreate was successful, false otherwise</returns>
        public bool SaveModifyDeleteData(string sql, List<SqlParameter> sqlParameterList)
        {
            this._sqlCon.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, _sqlCon);
            sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

            try
            {   // Return true or false

               return  sqlCommand.ExecuteNonQuery() !=0;
              
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                this._sqlCon.Close();
            }
        }

        /// <summary>
        /// Use transactions to save, modify, and delete data
        /// </summary>
        /// <param name="sql">Sql command statement</param>
        /// <param name="sqlParameterList">Provide arguments for sqlcommand</param>
        /// <param name="sqlTransaction">Used to manipulate database transactions</param>
        /// <param name="sqlCommand">An object that operates on the database</param>
        /// <returns></returns>
        public bool SaveModifyDeleteData(string sql, List<SqlParameter> sqlParameterList, SqlTransaction sqlTransaction, SqlCommand sqlCommand)
        {

            sqlCommand.CommandText = sql;
            sqlCommand.Transaction = sqlTransaction;
            sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

            try
            {
                // Return true or false
                return sqlCommand.ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
            }
        }


        /// <summary>
        /// Save file contet data to database
        /// </summary>
        /// <param name="dataTable">This is a datatable of file content transformation</param>
        /// <returns>If the file content data is saved successfully, return true. If it fails, return false</returns>
        /// <exception cref="ReadAndSaveFileException">Throws an exception when the structure of the file does not conform to the rules</exception>
        public bool SaveBulkData(DataTable dataTable, string targetTable)
        {
            SqlBulkCopy sqlBulkCopy = null;

            try
            {
                sqlBulkCopy = new SqlBulkCopy(_connStr, SqlBulkCopyOptions.FireTriggers);

                if (sqlBulkCopy != null && targetTable != string.Empty)
                {
                    //Target data table
                    sqlBulkCopy.DestinationTableName = targetTable.Trim();

                    //Map the corresponding fields in the data table
                    foreach (var column in dataTable.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());
                    }

                    //Writes data to the target table
                    sqlBulkCopy.WriteToServer(dataTable);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                //Close sqlBulkCopy
                if (sqlBulkCopy != null)
                {
                    sqlBulkCopy.Close();
                }
            }
        }

        /// <summary>
        /// Check if the data exists
        /// </summary>
        /// <param name="sql">Sql command statement</param>
        /// <param name="sqlParameter">Provide arguments for sqlcommand</param>
        /// <returns>Returns true if the data is present and false otherwise</returns>
        public string ExecuteScalar(string sql, List<SqlParameter> sqlParameterList)
        {
            this.OpenDbConnection();
            SqlCommand sqlCommand = new SqlCommand(sql, _sqlCon);
            sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

            try
            {
                //Returns the results of the query
                return sqlCommand.ExecuteScalar().ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.CloseDbConnection();
                sqlCommand.Parameters.Clear();
            }


        }

        public DataTable GetDataToDataTable(string sql, List<SqlParameter> sqlParameterList)
        {
            this.OpenDbConnection();
            SqlCommand command = new SqlCommand(sql, _sqlCon);
            command.Parameters.AddRange(sqlParameterList.ToArray());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
         
            try
            {
                //Returns the results of the query
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch
            {
                throw;
            }
            finally
            {
                this.CloseDbConnection();
                command.Parameters.Clear();
            }
        }
        public DataTable GetDataToDataTable(string sql)
        {
            this.OpenDbConnection();
            SqlCommand command = new SqlCommand(sql, _sqlCon);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            try
            {
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch
            {
                throw;
            }
            finally
            {
                this.CloseDbConnection();
                command.Parameters.Clear();
            }


        }
    }
}
