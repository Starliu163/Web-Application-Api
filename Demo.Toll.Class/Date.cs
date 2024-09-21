using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Toll.Class
{
    public class Date
    {
        public DateOnly GetToday()
        {
        
        return DateOnly.FromDateTime(DateTime.Now);
        }

    }
}
