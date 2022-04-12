using Codeup.Assesment.DTOs;
using Codeup.Assesment.Services;
using Codeup.Assesment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Codeup.Assesment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        private readonly IOrderManagementService _orderManagementService;

        public OrderManagementController(IOrderManagementService orderManagementService)
        {
            this._orderManagementService = orderManagementService;
        }
        // GET: api/<OrderManagement>/orders
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            try
            {
                return Ok(await _orderManagementService.GetAllOrders());
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<OrderManagement>/5
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            try
            {
                return Ok(await _orderManagementService.GetOrderById(orderId));
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST api/<OrderManagement>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderDto order)
        {
            try
            {
                GetOrderDto created = await _orderManagementService.CreateOrder(order);
                return Created(created.Id.ToString(), created);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<OrderManagement>/5
        [HttpPut("{orderId}")]
        public async Task<IActionResult> Put(int orderId, [FromBody] UpdateOrderDto order)
        {
            try
            {
                await _orderManagementService.UpdateOrder(orderId, order);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<OrderManagement>/5
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(int orderId)
        {
            try
            {
                await _orderManagementService.RemoveOrder(orderId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
