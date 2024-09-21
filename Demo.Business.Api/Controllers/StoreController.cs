using Demo.Business.Api.Services;
using Demo.Entity.Class;
using Demo.Toll.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Demo.Business.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {

        private readonly IConfiguration _configuration;
        [HttpPost("GetToDay")]
        [Authorize]
        public DateOnly GeToDay()
        {
            Date date = new Date();

            DateOnly day = date.GetToday();
            Console.WriteLine(day);
            return day;



        }


        [HttpPost("GetStoreName")]
        [Authorize]
        public IActionResult GetStoreName([FromBody]string StoreName)
        { 
        
           try
            {
                StoreService _storeService = new StoreService() { };
                string jsonString = JsonConvert.SerializeObject(_storeService.GetStoreName(StoreName));

                Console.WriteLine("JSON String: " + jsonString);
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return StatusCode(401, $"内部服务器错误: {ex.Message}");

    }
}
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> StoreExcelDataUpload([FromBody] List<Purchase_Store> data)
        {
            if (data == null || data.Count == 0)
            {
                return BadRequest("无效的数据");
            }

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in data)
                            {
                                string query = @"
                                INSERT INTO PurchaseRecords_Store (PurchaseDate, Supplier,  Note, Store)
                                VALUES (@PurchaseDate, @Supplier, @Note, @Store)";

                                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                                {

                                    command.Parameters.AddWithValue("@PurchaseDate", item.PurchaseDate);
                                    command.Parameters.AddWithValue("@Supplier", item.SupplierName);
                                    //command.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                                    command.Parameters.AddWithValue("@Note", item.Note);
                                    command.Parameters.AddWithValue("@Store", item.StoreName);
                                    await command.ExecuteNonQueryAsync();
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                return Ok(new { message = "数据上传成功" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"内部服务器错误: {ex.Message}");
            }

        }


    }

   
}
