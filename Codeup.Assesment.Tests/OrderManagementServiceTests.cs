using Codeup.Assesment.Data;
using Codeup.Assesment.Data.Repository;
using Codeup.Assesment.DTOs;
using Codeup.Assesment.Entities;
using Codeup.Assesment.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Codeup.Assesment.Tests
{
    public class OrderManagementServiceTests
    {
        private readonly OrdersDbContext _dbContext;
        private readonly IRepository<Country> _countriesRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Merchant> _merchantsRepository;
        private readonly IRepository<MerchantPeriod> _merchantPeriodsRepository;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderItem> _orderItemsRepository;

        private readonly OrderManagementService _orderManagementService;

        public OrderManagementServiceTests()
        {
            DbContextOptions<OrdersDbContext> options  = new DbContextOptions<OrdersDbContext>();
            this._dbContext = Substitute.For<OrdersDbContext>(options);
            this._countriesRepository = Substitute.For<IRepository<Country>>();
            this._usersRepository = Substitute.For<IRepository<User>>();
            this._productsRepository = Substitute.For<IRepository<Product>>();
            this._merchantsRepository = Substitute.For<IRepository<Merchant>>();
            this._merchantPeriodsRepository = Substitute.For<IRepository<MerchantPeriod>>();
            this._ordersRepository = Substitute.For<IRepository<Order>>();
            this._orderItemsRepository = Substitute.For<IRepository<OrderItem>>();

            _orderManagementService = new OrderManagementService(
                _dbContext,
                _countriesRepository,
                _usersRepository,
                _productsRepository,
                _merchantsRepository,
                _merchantPeriodsRepository,
                _ordersRepository,
                _orderItemsRepository);
            _ordersRepository.GetByIdAsync(Arg.Any<int>()).Returns(args => Task.FromResult(new Order() { Id = (int)args[0], OrderItems = new List<OrderItem>() { new OrderItem() { ProductId = 1 } } }));
            _ordersRepository.InsertAsync(Arg.Any<Order>()).Returns(Task.FromResult(new Order() { Id = 200 }));
            _ordersRepository.GetAllAsync().Returns(new List<Order>() { new Order() { Id = 1 }, new Order() { Id = 2 } });

        }
        [Fact]
        public async Task GetOrderById_withValidResponse()
        {
            // Arrange

            // Act
            GetOrderDto response = await _orderManagementService.GetOrderById(200);
            // Assert
            GetOrderDto result = Assert.IsType<GetOrderDto>(response);
            Assert.NotNull(result);
            Assert.Equal(200, result.Id);
        }
        [Fact]
        public async Task GetOrder_withValidResponse()
        {
            // Arrange

            // Act
            IEnumerable<GetOrderDto> response = await _orderManagementService.GetAllOrders();
            // Assert
            List<GetOrderDto> result = Assert.IsType<List<GetOrderDto>>(response);
            Assert.NotNull(result);
            Assert.Equal(2, (result as List<GetOrderDto>).Count);
        }
        [Fact]
        public async Task CreateOrder_withValidResponse()
        {
            // Arrange
            CreateOrderDto createOrderDto = new CreateOrderDto();
            createOrderDto.OrderItems = new List<CreateOrderItemDto>();
            createOrderDto.OrderItems.Add(new CreateOrderItemDto() { ProductId = 1, Quantity = 1 });
            // Act
            GetOrderDto response = await _orderManagementService.CreateOrder(createOrderDto);
            // Assert
            GetOrderDto result = Assert.IsType<GetOrderDto>(response);
            Assert.NotNull(result);
            Assert.Equal(200, result.Id);
        }
        [Fact]
        public async Task UpdateOrder_withValidResponse()
        {
            // Arrange
            UpdateOrderDto updateOrderDto = new UpdateOrderDto();
            updateOrderDto.Id = 200;
            updateOrderDto.OrderItems = new List<CreateOrderItemDto>();
            updateOrderDto.OrderItems.Add(new CreateOrderItemDto() { ProductId = 1, Quantity = 1 });
            // Act
            await _orderManagementService.UpdateOrder(200,updateOrderDto);
            // Assert
            int count = _ordersRepository.ReceivedCalls().Count();
            Assert.Equal(2, count);
        }
        [Fact]
        public async Task Delete_withValidResponse()
        {
            // Act
            await _orderManagementService.RemoveOrder(200);
            // Assert
            int count = _ordersRepository.ReceivedCalls().Count();
            Assert.Equal(2, count);
        }
    }
}
