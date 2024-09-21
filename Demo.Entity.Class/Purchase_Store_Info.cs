using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Entity.Class
{
    public class Purchase_Store_Info
    {
        private int ino;
        private int purchaseid;
        private int deviceid;
        private readonly DateOnly createdate = DateOnly.FromDateTime(DateTime.Now);
        private string deviceName;
        private int quantity;
        private decimal unitprice;
        private string note;

        public int Ino { get => ino; set => ino = value; }
        public int PurchaseID { get => purchaseid; set => purchaseid = value; }
        public int DeviceID { get => deviceid; set => deviceid = value; }
        public DateOnly CreateDate { get => createdate; }
        public int Quantity { get => quantity; set => quantity = value; }
        public decimal UniTPrice { get => unitprice; set => unitprice = value; }
        public string? Note  { get => note; set => note = value; }
        public string DeviceName { get => deviceName; set => deviceName = value; }
    }
}
