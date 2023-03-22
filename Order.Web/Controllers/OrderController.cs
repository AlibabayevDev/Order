using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Order.Web.Models;
using Order.WebCore.Mappers;
using Order.WebCore.Models;
using Order.WebCore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWorkService service;
        public OrderController(IUnitOfWorkService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orderModels = service.OrderService.GetAll();

            OrderViewModel viewModel = new OrderViewModel()
            {
                Orders = orderModels.Where(x => (x.Date >= DateTime.Today.AddMonths(-1))),
            };

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            
            SaveOrderViewModel viewModel = new SaveOrderViewModel();

            var providers = service.ProviderService.GetAll();

            viewModel.ProviderList = new SelectList(providers, "Id", "Name");

            if (id != 0)
            {
                var order = service.OrderService.Get(id);
                viewModel.Order = order;
            }

            return PartialView(viewModel);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            SaveOrderViewModel viewModel = new SaveOrderViewModel();

            var providers = service.ProviderService.GetAll();

            viewModel.ProviderList = new SelectList(providers, "Id", "Name");

            if (id != 0)
            {
                var order = service.OrderService.Get(id);
                viewModel.Order = order;
            }

            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult Add(SaveOrderViewModel model)
        {
            try
            {
                var temp = service.OrderService.Check(model.Order);
                if(temp==true)
                {
                    TempData["Message"] = "Заказ от этого поставщика существует";
                }
                else
                {
                    service.OrderService.Save(model.Order);
                    TempData["Message"] = "Операция успешно";
                }
            }
            catch (Exception exc)
            {
                TempData["Message"] = "Операция неудачна";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(SaveOrderViewModel model)
        {
            try
            {
                var temp=service.OrderService.CheckId(model.Order);
                var temp1 = service.OrderService.Check(model.Order);
                if(temp==true)
                {
                    service.OrderService.Save(model.Order);
                    TempData["Message"] = "Операция успешно";
                }
                else if(temp1==false)
                {
                    service.OrderService.Save(model.Order);
                    TempData["Message"] = "Операция успешно";
                }
                else
                {
                    TempData["Message"] = "Заказ от этого поставщика существует";
                }
            }
            catch (Exception exc)
            {
                TempData["Message"] = "Операция неудачна";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(OrderViewModel viewModel)
        {
            var deletedId = viewModel.Deleted.Id;

            service.OrderService.Delete(deletedId);

            TempData["Message"] = "Operation successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SortDate(OrderViewModel model)
        {
            var orderModels = service.OrderService.GetAll();
            IEnumerable<OrderModel> sortModels;
            if (model.SortDate1.ToString() == "01.01.0001 0:00:00" && model.SortDate2.ToString() == "01.01.0001 0:00:00")
            {
                sortModels = orderModels.Where(x => (x.Date >= DateTime.Now.AddMonths(-1)));
            }
            else if (model.SortDate1.ToString() == "01.01.0001 0:00:00")
            {
                sortModels = orderModels.Where(x => (x.Date <= model.SortDate2));
            }
            else if (model.SortDate2.ToString() == "01.01.0001 0:00:00")
            {
                sortModels = orderModels.Where(x => (x.Date >= model.SortDate1));
            }
            else
            {
                sortModels = orderModels.Where(x => (x.Date >= model.SortDate1 && x.Date <= model.SortDate2));
            }


            var viewModel = new OrderViewModel()
            {
                Orders = sortModels,
                SortDate1=model.SortDate1,
                SortDate2=model.SortDate2,
            };
            return View("Index", viewModel);
        }
    }
}
