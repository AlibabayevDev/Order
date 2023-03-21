using Microsoft.AspNetCore.Mvc;
using Order.WebCore.Services.Contracts;
using System.Linq;
using System;
using Order.Web.Models;
using Order.WebCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Order.WebCore.Mappers;
using System.Reflection;

namespace Order.Web.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IUnitOfWorkService service;
        public OrderItemController(IUnitOfWorkService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orderItemModels = service.OrderItemService.GetAll();

            OrderItemViewModel viewModel = new OrderItemViewModel()
            {
                OrderItems = orderItemModels
            };

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Save(int id)
        {
            SaveOrderItemViewModel viewModel = new SaveOrderItemViewModel();

            var orderItems = service.OrderItemService.GetAll();

            viewModel.OrderItemList = new SelectList(orderItems, "Id", "Name");

            if (id != 0)
            {
                var orderItem = service.OrderItemService.Get(id);
                viewModel.OrderItem = orderItem;
                var order = service.OrderService.Get(orderItem.OrderId);

                viewModel.Order = order;
            }
            

            return PartialView(viewModel);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            SaveOrderItemViewModel viewModel = new SaveOrderItemViewModel();

            var orderItems = service.OrderService.GetAll();

            viewModel.OrderItemList = new SelectList(orderItems, "Id", "Name");
            var order = service.OrderService.Get(id);
            
            viewModel.Order = new OrderModel()
            {
                Id = order.Id,
                Number=order.Number,
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult Save(SaveOrderItemViewModel model)
        {
            model.OrderItem.OrderId = model.Order.Id;
            try
            {
                //if (ModelState.IsValid == false)
                //{
                //    var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                //    var errorMessage = errors.Aggregate((message, value) =>
                //    {
                //        if (message.Length == 0)
                //            return value;

                //        return message + ", " + value;
                //    });

                //    TempData["Message"] = errorMessage;
                //    return RedirectToAction("Index");
                //}

                service.OrderItemService.Save(model.OrderItem);

                TempData["Message"] = "Operation successfully";
            }
            catch (Exception exc)
            {

                TempData["Message"] = "Operation unsuccessfully";
            }

            return RedirectToAction("OrderItems", new { orderId = model.Order.Id });
        }

        [HttpPost]
        public IActionResult Delete(OrderItemViewModel viewModel)
        {
            var deletedId = viewModel.Deleted.Id;

            var orderItem=service.OrderItemService.Get(deletedId);

            service.OrderItemService.Delete(deletedId);
            TempData["Message"] = "Operation successfully";

            return RedirectToAction("OrderItems", new { orderId = orderItem.OrderId });
        }


        public IActionResult OrderItems(int orderId)
        {
            var orderItems = service.OrderItemService.GetById(orderId);
            var order=service.OrderService.Get(orderId);
            var viewModel = new OrderItemViewModel()
            {
                OrderItems = orderItems,
                Order=order
            };

            return View("Index", viewModel);
        }
    }
}
