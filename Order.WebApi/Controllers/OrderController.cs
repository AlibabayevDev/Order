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
                var bank = OrderService.Get(id);

                if (bank == null)
                    return BadRequest("No such a bank found to delete");

                OrderService.Delete(id);

                return Ok("Successfully deleted");
            }
            catch
            {
                return BadRequest("Failed to delete");
            }
        }
    }

}
