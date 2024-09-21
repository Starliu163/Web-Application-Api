using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Entity.Class
{
    public class Store
    {
        private int StoreId;
        private string StoreName;
        private DateTime CreateTime;
        private string Note;

        public int StoreId1 { get => StoreId; set => StoreId = value; }
        public string StoreName1 { get => StoreName; set => StoreName = value; }
        public DateTime CreateTime1 { get => CreateTime; set => CreateTime = value; }
        public string Note1 { get => Note; set => Note = value; }
    }
}
