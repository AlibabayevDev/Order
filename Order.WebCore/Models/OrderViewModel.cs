using System.Collections.Generic;

namespace Order.WebCore.Models
{
    public class OrderViewModel
    {
        public IEnumerable<OrderModel> Orders { get; set; }
        public OrderModel Deleted { get; set; }
        public DateTime SortDate1 { get; set; }
        public DateTime SortDate2 { get; set; }

    }
}
