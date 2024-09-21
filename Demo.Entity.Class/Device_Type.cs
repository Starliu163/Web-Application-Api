using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Entity.Class
{
    public class Device_Type
    {
        private int deviceId;
        private string deviceName;
        private string deviceDetail;
        private decimal unitPrice;
        private DateOnly createDate;
        private string note;

        public int DeviceId { get => deviceId; set => deviceId = value; }
        public string DeviceName { get => deviceName; set => deviceName = value; }
        public string DeviceDetail { get => deviceDetail; set => deviceDetail = value; }
        public decimal UnitPrice { get => unitPrice; set => unitPrice = value; }
        public DateOnly CreateDate { get => createDate; set => createDate = value; }
        public string Note { get => note; set => note = value; }
    }
}
