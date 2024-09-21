using Demo.Business.Api.Services;
using Demo.Entity.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Demo.Business.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        /// <summary>
        /// 获取Device
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpPost("GetDevice")]
        [Authorize]
        public async Task<IActionResult> GetDevice([FromBody] string DeviceName)

        {
            DeviceService device = new DeviceService();
            try
            {

                var deviceStr = await Task.Run(() => device.GetDeviceByName(DeviceName));
                string jsonString = JsonConvert.SerializeObject(deviceStr);
                //Console.WriteLine("JSON String: " + jsonString);
                return Ok(jsonString);
            }
            catch (Exception)
            {
                return StatusCode(401);
            }


        }


        public class DeviceRequest
        {

            public string DeviceName { get; set; }

        }
    }
}
