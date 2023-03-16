using Microsoft.AspNetCore.Mvc.Rendering;
using Order.WebCore.Models;

namespace Order.Web.Models
{
    public class SaveOrderItemViewModel
    {
        public SelectList OrderItemList { get; set; }
        public OrderItemModel OrderItem { get; set; }
    }
}
