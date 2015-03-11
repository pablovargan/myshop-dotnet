using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshopService.DataObjects
{
    public class OrderLine : EntityData
    {
        public virtual Product Product { get; set; }

        public OrderLine()
        {
            Product = new Product();
        }
    }
}
