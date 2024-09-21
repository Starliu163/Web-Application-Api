using Dmo.Entity.Class;
using Demo.Toll.Class;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using BCrypt.Net;

namespace Demo.Business.Api.Services
{
    // 认证服务类，负责处理用户认证相关的业务逻辑
    public class AuthService
    {
        private readonly DbHelper _dbHelper;

        // 构造函数，初始化 DbHelper 和连接字符串
        public AuthService(IConfiguration configuration)
        {
            // 从配置文件中读取连接字符串
            string connectionString = configuration.GetConnectionString("SqlServerConnection");
            _dbHelper = new DbHelper(connectionString);
        }

        // 验证用户的方法
        public bool ValidateUser(User user)
        {
            // SQL语句 通过账号获取哈希密码
            string sql = "SELECT PasswordHash FROM Users WHERE Account = @Account";
            List<SqlParameter> sqlParameterlist = new List<SqlParameter>
            {
                new SqlParameter("@Account", user.Account), // 用户账号
            };

            var storedPasswordHash = _dbHelper.ExecuteScalar(sql, sqlParameterlist);

            // 使用 DbHelper 检查用户是否存在
            if (storedPasswordHash != null)
            {
                //比较密码是否相等
                bool isPasscordValid = BCrypt.Net.BCrypt.Verify(user.Pwd,storedPasswordHash);
                //返回比较结果
                return isPasscordValid;
            } 
            return false;
        }
    }
}
