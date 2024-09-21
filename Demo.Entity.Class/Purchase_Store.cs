using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Entity.Class
{
    public class Purchase_Store
    {
        private readonly int purchaseid;
        private readonly DateTime? purchasedate;
        private string supplierName;
        private decimal totalprice;
        private string note;
        private string storeName;

        public int PurchaseID { get => purchaseid;}
        public DateTime ?PurchaseDate { get => purchasedate;}
        public decimal TotalPrice { get => totalprice; set => totalprice = value; }
        public string StoreName { get => storeName; set => storeName = value; }
        public string Note { get => note; set => note = value; }
        public string SupplierName { get => supplierName; set => supplierName = value; }
    }
}
