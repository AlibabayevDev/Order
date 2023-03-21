using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Models
{
    public class OrderItemViewModel
    {
        public List<OrderItemModel> OrderItems { get; set; }
        public OrderItemModel Deleted { get; set; }
        public OrderModel Order { get; set; }   
    }
}
