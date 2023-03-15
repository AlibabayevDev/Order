using Order.Core.Domain.Abstract;
using Order.WebCore.Mappers;
using Order.WebCore.Models;
using Order.WebCore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.WebCore.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork db;
        private readonly OrderMapper orderMapper;

        public OrderService(IUnitOfWork db)
        {
            this.db = db;
            orderMapper = new OrderMapper();
        }

        public void Save(OrderModel model)
        {
            if (model == null)
                return;

            var order = orderMapper.Map(model);


            if(order.Id != 0)
            {
                db.OrderRepository.Update(order);
            }
            else
            {
                db.OrderRepository.Add(order);
            }
        }

        public void Delete(int id)
        {
            var order = db.OrderRepository.Get(id);
            
            db.OrderRepository.Update(order);
        }

        public OrderModel Get(int id)
        {
            var order = db.OrderRepository.Get(id);

            if(order == null)
                return null;
            
            var orderModel = orderMapper.Map(order);   

            return orderModel;
        }

        public List<OrderModel> GetAll()
        {
            var orders = db.OrderRepository.GetAll();

            List<OrderModel> orderModels = new List<OrderModel>();

            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                var orderModel = orderMapper.Map(order);

                orderModel.No = i + 1;
                orderModels.Add(orderModel);
            }

            return orderModels;
        }
    }
}