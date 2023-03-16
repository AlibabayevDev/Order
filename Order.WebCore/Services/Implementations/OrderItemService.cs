using Order.Core.Domain.Abstract;
using Order.WebCore.Mappers;
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
        private readonly OrderItemMapper orderItemMapper;

        public OrderItemService(IUnitOfWork db)
        {
            this.db = db;
            this.orderItemMapper = new OrderItemMapper();
        }

        public void Save(OrderItemModel model)
        {
            if (model == null)
                return;

            var orderItem = orderItemMapper.Map(model);


            if (orderItem.Id != 0)
            {
                db.OrderItemRepository.Update(orderItem);
            }
            else
            {
                db.OrderItemRepository.Add(orderItem);
            }
        }

        public void Delete(int id)
        {
            var orderItem = db.OrderItemRepository.Get(id);

            db.OrderItemRepository.Delete(orderItem.Id);
        }

        public OrderItemModel Get(int id)
        {
            var orderItem = db.OrderItemRepository.Get(id);

            if (orderItem == null)
                return null;

            var orderItemModel = orderItemMapper.Map(orderItem);

            return orderItemModel;
        }

        public List<OrderItemModel> GetAll()
        {
            var orderItems = db.OrderItemRepository.GetAll();

            List<OrderItemModel> orderItemModels = new List<OrderItemModel>();

            for (int i = 0; i < orderItems.Count; i++)
            {
                var orderItem = orderItems[i];
                var orderItemModel = orderItemMapper.Map(orderItem);

                orderItemModel.No = i + 1;
                orderItemModels.Add(orderItemModel);
            }

            return orderItemModels;
        }
    }
}
