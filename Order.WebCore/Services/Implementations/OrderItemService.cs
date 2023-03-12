using Order.Core.Domain.Abstract;
using Order.WebCore.Models;
using Order.WebCore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Services.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork db;
        public OrderItemService(IUnitOfWork db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public OrderItemModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderItemModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(OrderItemModel model)
        {
            throw new NotImplementedException();
        }
    }
}
