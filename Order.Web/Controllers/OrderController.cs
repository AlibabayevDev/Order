﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                Orders = orderModels
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
            if (id == 0)
                return PartialView();

            var orderModel = service.OrderService.Get(id);

            return PartialView(orderModel);
        }

        [HttpPost]
        public IActionResult Save(OrderModel model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                    var errorMessage = errors.Aggregate((message, value) =>
                    {
                        if (message.Length == 0)
                            return value;

                        return message + ", " + value;
                    });

                    TempData["Message"] = errorMessage;
                    return RedirectToAction("Index");
                }

                service.OrderService.Save(model);

                TempData["Message"] = "Operation successfully";
            }
            catch (Exception exc)
            {

                TempData["Message"] = "Operation unsuccessfully";
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
    }
}
