using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Order.Web.Models;
using Order.WebCore.Mappers;
using Order.WebCore.Models;
using Order.WebCore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

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
            var orderModels = new List<OrderModel>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://localhost:7018/api/Order/").Result)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        orderModels = JsonConvert.DeserializeObject<List<OrderModel>>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }

            }

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
                var order = new OrderModel();
                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.GetAsync("https://localhost:7018/api/Order/GetById/"+id).Result)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = response.Content.ReadAsStringAsync().Result;
                            order = JsonConvert.DeserializeObject<OrderModel>(apiResponse);
                        }
                        else
                            ViewBag.StatusCode = response.StatusCode;
                    }

                }
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
                var order = new OrderModel();
                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.GetAsync("https://localhost:7018/api/Order/GetById/" + id).Result)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = response.Content.ReadAsStringAsync().Result;
                            order = JsonConvert.DeserializeObject<OrderModel>(apiResponse);
                        }
                        else
                            ViewBag.StatusCode = response.StatusCode;
                    }

                }
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
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://localhost:7018/api/Order/Delete/" + deletedId).Result)
                {
                    TempData["Message"] = "Operation successfully";
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SortDate(OrderViewModel model)
        {
            var orderModels = service.OrderService.GetAll();
            IEnumerable<OrderModel> sortModels = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://localhost:7018/api/Order/SortDate?SortDate1="+model.SortDate1.ToString("yyyy-MM-dd")+"&SortDate2="+model.SortDate2.ToString("yyyy-MM-dd")).Result)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().Result;
                        sortModels = JsonConvert.DeserializeObject<List<OrderModel>>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }

            if(sortModels == null)
            {
                sortModels = new List<OrderModel>();
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
