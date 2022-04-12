using Codeup.Assesment.API.Controllers;
using Codeup.Assesment.DTOs;
using Codeup.Assesment.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Codeup.Assesment.Tests
{
    public class OrderManagementControllerTests
    {
        private IOrderManagementService _orderManagementService;
        public OrderManagementControllerTests()
        {
            _orderManagementService = Substitute.For<IOrderManagementService>();
            _orderManagementService.GetAllOrders().Returns(new List<GetOrderDto>() { new GetOrderDto() { Id = 1 }, new GetOrderDto() { Id = 2 } });
            _orderManagementService.GetOrderById(Arg.Any<int>()).Returns(args => Task.FromResult(new GetOrderDto() { Id = (int)args[0] }));
            _orderManagementService.CreateOrder(Arg.Any<CreateOrderDto>()).Returns(args => Task.FromResult(new GetOrderDto() { Id = 200 }));

        }
        [Fact]
        public async Task GetOrder_withValidResponse()
        {
            // Arrange
            OrderManagementController orderController = new OrderManagementController(_orderManagementService);
            // Act
            IActionResult response = await orderController.GetOrder();
            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(2, (result.Value as List<GetOrderDto>).Count);
        }
        [Fact]
        public async Task GetOrderbyId_withValidResponse()
        {
            // Arrange
            int orderId = 1;
            OrderManagementController orderController = new OrderManagementController(_orderManagementService);
            // Act
            IActionResult response = await orderController.GetOrder(orderId);
            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(orderId, (result.Value as GetOrderDto).Id);
        }
        [Fact]
        public async Task CreateOrder_withValidResponse()
        {
            // Arrange
            int orderId = 200;
            CreateOrderDto createOrderDto = new CreateOrderDto() { OrderItems = new List<CreateOrderItemDto>() { new CreateOrderItemDto() } };
            OrderManagementController orderController = new OrderManagementController(_orderManagementService);
            // Act
            IActionResult response = await orderController.CreateOrder(createOrderDto);
            // Assert
            CreatedResult result = Assert.IsType<CreatedResult>(response);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(orderId, (result.Value as GetOrderDto).Id);
        }
        [Fact]
        public async Task UpdateOrder_withValidResponse()
        {
            // Arrange
            int orderId = 200;
            UpdateOrderDto updateOrderDto = new UpdateOrderDto() { OrderItems = new List<CreateOrderItemDto>() { new CreateOrderItemDto() } };
            OrderManagementController orderController = new OrderManagementController(_orderManagementService);
            // Act
            IActionResult response = await orderController.UpdateOrder(orderId, updateOrderDto);
            // Assert
            OkResult result = Assert.IsType<OkResult>(response);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public async Task DeleteOrder_withValidResponse()
        {
            // Arrange
            int orderId = 200;
            OrderManagementController orderController = new OrderManagementController(_orderManagementService);
            // Act
            IActionResult response = await orderController.DeleteOrder(orderId);
            // Assert
            OkResult result = Assert.IsType<OkResult>(response);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}