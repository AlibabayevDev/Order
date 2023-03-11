using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Name { get; set; }
        public string VOEN { get; set; }
        public string CorrespondentAccount { get; set; }
        public string SWIFT { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
