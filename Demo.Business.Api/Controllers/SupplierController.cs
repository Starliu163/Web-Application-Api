using Demo.Business.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Demo.Business.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {

        [HttpPost("GetSupplierName")]
        [Authorize]
        public async Task<IActionResult> GetSupplierName()
        {
            try
            {
                SupplierService supplierService = new SupplierService();
                string jsonString = await Task.Run(() => JsonConvert.SerializeObject(supplierService.GetSupplierNames()));

                Console.WriteLine("JSON String: " + jsonString);
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return StatusCode(401, $"内部服务器错误: {ex.Message}");


            }
        }
    }
}
