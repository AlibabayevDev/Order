using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.WebCore.Models;
using Order.WebCore.Services.Contracts;
using System;

namespace Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService OrderService;
        public OrderController(IOrderService OrderService)
        {
            this.OrderService = OrderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var banks = OrderService.GetAll();

                return Ok(banks);
            }
            catch
            {
                return BadRequest("Unknown error occured");
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var bank = OrderService.Get(id);

                if (bank == null)
                    return BadRequest("No Bank found with given id");

                return Ok(bank);
            }
            catch
            {
                return BadRequest("Unknown error occured");
            }
        }

        [HttpPost]
        public IActionResult Post(OrderModel OrderModel)
        {
            try
            {
                OrderService.Save(OrderModel);

                return Ok("Success!");
            }
            catch
            {
                return BadRequest("Failed to add");
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, OrderModel bankModel)
        {
            try
            {
                if (id != bankModel.Id)
                    return BadRequest("BankModel ID mismatch");

                var bankToUpdate = OrderService.Get(id);

                if (bankToUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");

                OrderService.Save(bankModel);

                return Ok("Successfully Updated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var order = OrderService.Get(id);

                if (order == null)
                    return BadRequest("No such a bank found to delete");

                OrderService.Delete(id);

                return Ok("Successfully deleted");
            }
            catch
            {
                return BadRequest("Failed to delete");
            }
        }

        [HttpPost]
        [Route("SortDate")]
        public IActionResult SortDate(OrderViewModel model)
        {
            try
            {
                var orderModels = OrderService.GetAll();

                if (orderModels == null)
                    return BadRequest("No such a order found");

                IEnumerable<OrderModel> sortModels;
                if (model.SortDate1.ToString() == "01.01.0001 0:00:00" && model.SortDate2.ToString() == "01.01.0001 0:00:00")
                {
                    sortModels = orderModels.Where(x => (x.Date >= DateTime.Now.AddMonths(-1)));
                    return Ok(sortModels);
                }
                else if (model.SortDate1.ToString() == "01.01.0001 0:00:00")
                {
                    sortModels = orderModels.Where(x => (x.Date <= model.SortDate2));
                    return Ok(sortModels);
                }
                else if (model.SortDate2.ToString() == "01.01.0001 0:00:00")
                {
                    sortModels = sortModels = orderModels.Where(x => (x.Date >= model.SortDate1));
                    return Ok(sortModels);
                }
                else
                {
                    sortModels=orderModels.Where(x => (x.Date >= model.SortDate1 && x.Date <= model.SortDate2));
                    return Ok(sortModels);
                }
            }
            catch
            {
                return BadRequest("Failed");
            }
        }
    }

}
