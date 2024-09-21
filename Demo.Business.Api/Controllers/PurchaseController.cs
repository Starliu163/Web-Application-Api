using Demo.Business.Api.Services;
using Demo.Entity.Class;
using Demo.Toll.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics.Metrics;

namespace Demo.Business.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PurchaseController : ControllerBase
    {

        [HttpPost("AddPurchaseData")]
        [Authorize]
        public async Task<IActionResult> AddPurchaseData([FromBody] CombinedRequest purchaseData)
        {
            PurchaseService purchaseService = new PurchaseService();
            try
            {
                if (await Task.Run(() => purchaseService.AddPurchaseData(purchaseData.purchaseStore, purchaseData.purchaseDatainfo)))
                {
                    return Ok();
                };
                return StatusCode(401);
            }
            catch (Exception)
            {

                return StatusCode(401);
            }
        }

 


        [HttpPost("GetPurchaseInfo")]
        [Authorize]
        public async Task<IActionResult> GetPurchaseInfo([FromBody] long PurchaseID)
        {
            PurchaseService purchaseService = new PurchaseService();
            try
            {

                return Ok(JsonConvert.SerializeObject(await Task.Run(() => purchaseService.GetPurchaseInfo(PurchaseID))));

            }
            catch (Exception)
            {

                return StatusCode(401);
            }
        }
        //直接读取所有信息
        [HttpPost("GetStorePurchaseRecorder")]
        [Authorize]
        public async Task<IActionResult> GetStorePurchaseRecorder()
        {
            PurchaseService purchaseService = new PurchaseService();
            try
            {
                return Ok(JsonConvert.SerializeObject(await Task.Run(() => purchaseService.GetStorePurchaseRecorder())));

            }
            catch (Exception ex)
            {
                return StatusCode(401, $"内部服务器错误: {ex.Message}");

            }

        }

        [HttpPost("GetStorePurchaseRecorderByParm")]
        [Authorize]
        public async Task<IActionResult> GetStorePurchaseRecorderByParm([FromBody] CombinePurchase combinePurchase)
        {
            PurchaseService purchaseService = new PurchaseService();
            try
            {
               string Data=JsonConvert.SerializeObject(await Task.Run(() => purchaseService.GetStorePurchaseRecorderByParm(combinePurchase.StoreName, combinePurchase.DeviceName, combinePurchase.StartDate, combinePurchase.EndDate)));
  
                Console.WriteLine(Data);
                return Ok(Data);
            }          
            catch (Exception ex)
            {
                return StatusCode(401, $"内部服务器错误: {ex.Message}");

            }
        }



        [HttpPost("GetStorePurchaseRecorderListByParm")]
        [Authorize]
        public async Task<IActionResult> GetStorePurchaseRecorderListByParm([FromBody] CombinePurchase combinePurchase)
        {
            PurchaseService purchaseService = new PurchaseService();
            try
            {
                string Data = JsonConvert.SerializeObject(await Task.Run(() => purchaseService.GetStorePurchaseRecorderListByParm(combinePurchase.StoreName, combinePurchase.DeviceName, combinePurchase.StartDate, combinePurchase.EndDate)));

                Console.WriteLine(Data);
                return Ok(Data);
            }
            catch (Exception ex)
            {
                return StatusCode(401, $"内部服务器错误: {ex.Message}");

            }
        }
    }
    
    //}
    public class CombinePurchase
    {
        private string storeName;
        private string deviceName;
        private string startDate;
        private string endDate;


        public string StoreName { get => storeName; set => storeName = value; }
        public string DeviceName { get => deviceName; set => deviceName = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
    }
    public class CombinedRequest
    {
        public Purchase_Store purchaseStore { get; set; }
        public List<Purchase_Store_Info> purchaseDatainfo { get; set; }
    }

}
