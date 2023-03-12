using System.ComponentModel.DataAnnotations;

namespace Order.WebCore.Models
{
    public class OrderModel : BaseModel
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}
