using System.Collections.Generic;

namespace Order.WebCore.Models
{
    public class OrderViewModel
    {
        public List<OrderModel> Orders { get; set; }
        public OrderModel Deleted { get; set; }
    }
}
