using Microsoft.AspNetCore.Mvc.Rendering;
using Order.WebCore.Models;

namespace Order.Web.Models
{
    public class SaveOrderViewModel
    {
        public SelectList OrderList { get; set; }
        public OrderModel Order { get; set; }
    }
}
